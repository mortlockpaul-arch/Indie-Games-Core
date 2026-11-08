# Source repair routines synced from x360-game-manager/src/package_window.py
# in the order used by the decompiler's Convert XNA 3 option.
import argparse
import os
import re
import shutil
import tempfile
from pathlib import Path


def fix_xna_storage_container_path(root, log):
    """
    Convert XNA StorageContainer.Path file operations to FNA.

    Example:
        string path = Path.Combine(storageContainer.Path, "GravityDash.sav");
        FileStream fileStream = File.Open(path, FileMode.Create);

    Becomes:
        Stream fileStream = storageContainer.CreateFile("GravityDash.sav");
    """

    root = Path(root)
    total_fixes = 0
    modified_files = 0

    path_pattern = re.compile(
        r"""
        (?P<indent>^[ \t]*)
        (?:string|var)\s+
        (?P<pathvar>[A-Za-z_]\w*)
        \s*=\s*
        (?:System\.IO\.)?Path\.Combine
        \s*\(
            \s*(?P<container>[A-Za-z_]\w*)\.Path
            \s*,\s*
            (?P<filename>"(?:\\.|[^"\\])*")
            \s*
        \)
        \s*;
        """,
        re.MULTILINE | re.VERBOSE,
    )

    for file in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj", ".git"} for part in file.parts):
            continue

        try:
            original = file.read_text(encoding="utf-8-sig")
            updated = original
            fixes = 0

            matches = list(path_pattern.finditer(updated))

            # Process backwards so replacing text doesn't invalidate offsets.
            for match in reversed(matches):
                pathvar = match.group("pathvar")
                container = match.group("container")
                filename = match.group("filename")

                escaped = re.escape(pathvar)

                # Match File.Open(path, FileMode.X)
                open_pattern = re.compile(
                    rf"""
                    (?P<type>FileStream|Stream|var)
                    \s+
                    (?P<stream>[A-Za-z_]\w*)
                    \s*=\s*
                    (?:System\.IO\.)?File\.Open
                    \s*\(
                        \s*{escaped}\s*,
                        \s*FileMode\.(?P<mode>
                            CreateNew|Create|OpenOrCreate|
                            Open|Truncate|Append
                        )
                        (?:\s*,\s*FileAccess\.\w+)?
                        (?:\s*,\s*FileShare\.\w+)?
                        \s*
                    \)
                    \s*;
                    """,
                    re.VERBOSE,
                )

                # Only convert a path when its supported uses are known.
                tail = updated[match.end():]
                open_match = open_pattern.search(tail)

                # Avoid accidentally matching a different variable declaration
                # from an unrelated method.
                if open_match:
                    between = tail[:open_match.start()]

                    if re.search(
                        rf"\b{escaped}\b",
                        between,
                    ):
                        open_match = None

                if open_match:
                    mode = open_match.group("mode")
                    stream = open_match.group("stream")

                    if mode in ("Create", "CreateNew"):
                        replacement = (
                            f"Stream {stream} = "
                            f"{container}.CreateFile({filename});"
                        )
                    else:
                        replacement = (
                            f"Stream {stream} = "
                            f"{container}.OpenFile("
                            f"{filename}, FileMode.{mode});"
                        )

                    start = match.end() + open_match.start()
                    end = match.end() + open_match.end()

                    updated = updated[:start] + replacement + updated[end:]

                    # Remove the original path declaration.
                    updated = (
                        updated[:match.start()]
                        + updated[match.end():]
                    )

                    fixes += 1
                    continue

                # Handle File.Exists(path) when the path variable is used
                # exclusively for this check.
                exists_pattern = re.compile(
                    rf"(?:System\.IO\.)?File\.Exists\s*\(\s*{escaped}\s*\)"
                )

                exists_matches = list(exists_pattern.finditer(tail))

                if len(exists_matches) == 1:
                    remaining = exists_pattern.sub("", tail)

                    if not re.search(rf"\b{escaped}\b", remaining):
                        updated = exists_pattern.sub(
                            f"{container}.FileExists({filename})",
                            updated,
                        )
                        updated = (
                            updated[:match.start()]
                            + updated[match.end():]
                        )
                        fixes += 1

            # Remove unused StorageContainer.Path declarations.
            # Only examine references inside the containing C# method/block.

            unused_path_pattern = re.compile(
                r"""
                ^[ \t]*
                (?:string|var)\s+
                (?P<pathvar>[A-Za-z_]\w*)
                \s*=\s*
                (?:System\.IO\.)?Path\.Combine
                \s*\(
                    \s*[A-Za-z_]\w*\.Path
                    \s*,\s*
                    "(?:\\.|[^"\\])*"
                    \s*
                \)
                \s*;
                [ \t]*(?:\r?\n)?
                """,
                re.MULTILINE | re.VERBOSE,
            )

            for match in reversed(list(unused_path_pattern.finditer(updated))):
                pathvar = match.group("pathvar")

                # Determine the containing block using the declaration's indentation.
                declaration_indent = len(
                    match.group(0)
                ) - len(
                    match.group(0).lstrip()
                )

                # Find the end of the current method/block.
                block_end = len(updated)

                for line_match in re.finditer(
                        r"^[ \t]*\}",
                        updated[match.end():],
                        re.MULTILINE,
                ):
                    line = line_match.group(0)
                    indent = len(line) - len(line.lstrip())

                    if indent < declaration_indent:
                        block_end = match.end() + line_match.start()
                        break

                # Check only the remainder of the containing block.
                block_text = updated[match.end():block_end]

                if not re.search(rf"\b{re.escape(pathvar)}\b", block_text):
                    updated = updated[:match.start()] + updated[match.end():]
                    fixes += 1
                    log(f"Removed unused StorageContainer.Path: {file}")

            if updated != original:
                backup = file.with_suffix(file.suffix + ".bak")

                if not backup.exists():
                    backup.write_text(original, encoding="utf-8")

                file.write_text(updated, encoding="utf-8")

                modified_files += 1
                total_fixes += fixes

                log(f"Fixed {fixes} storage operation(s): {file}")

        except Exception as exc:
            log(f"Storage conversion failed: {file}: {exc}")

    log(
        f"StorageContainer conversion complete: "
        f"{total_fixes} fixes across {modified_files} files"
    )

    return total_fixes


def fix_unused_storage_container_paths(root, log):
    root = Path(root)
    total = 0

    pattern = re.compile(
        r'''
        ^[ \t]*
        (?:string|var)\s+
        (?P<variable>[A-Za-z_]\w*)
        \s*=\s*
        (?:System\.IO\.)?Path\.Combine
        \s*\(
            \s*(?P<container>[A-Za-z_]\w*)\.Path
            \s*,\s*
            (?P<filename>"(?:\\.|[^"\\])*")
            \s*
        \)\s*;
        [ \t]*\r?\n
        (?P<next>[ \t]*(?:if\s*\(\s*!?\s*)?[A-Za-z_]\w*\.FileExists\s*\()
        ''',
        re.MULTILINE | re.VERBOSE,
    )

    for file in root.rglob("*.cs"):
        if any(p.lower() in {"bin", "obj", ".git"} for p in file.parts):
            continue

        try:
            original = file.read_text(encoding="utf-8-sig")

            def replace(match):
                nonlocal total

                # Only remove when the next operation is on the same container.
                next_statement = match.group("next")

                if not re.search(
                    rf'\b{re.escape(match.group("container"))}\.FileExists\s*\(',
                    next_statement,
                ):
                    return match.group(0)

                total += 1
                return next_statement

            updated = pattern.sub(replace, original)

            if updated != original:
                file.write_text(updated, encoding="utf-8")
                log(f"Removed unused storage path: {file}")

        except Exception as exc:
            log(f"Failed: {file}: {exc}")

    log(f"Removed {total} unused storage path declarations")
    return total


def fix_xna_signed_in_gamer_avatar(root: Path, log=None) -> int:
    root = Path(root)
    log = log or (lambda message: None)

    pattern = re.compile(
        r'if\s*\(\s*'
        r'(?P<gamer>\w+)\s*!=\s*null\s*&&\s*'
        r'(?P=gamer)\.Avatar\s*!=\s*null\s*'
        r'\)\s*\{\s*'
        r'return\s+(?P=gamer)\.Avatar\s*;\s*'
        r'\}',
        re.MULTILINE
    )

    total = 0

    for file in root.rglob("*.cs"):
        content = file.read_text(encoding="utf-8-sig")

        updated, count = pattern.subn("", content)

        if count:
            file.write_text(updated, encoding="utf-8")
            total += count

            log(f"Fixed SignedInGamer.Avatar: {file.name}")

    log(f"SignedInGamer.Avatar fixes: {total}")

    return total


def fix_xna_depth_stencil_buffer(
    root: str | Path,
    log_callback=None,
) -> int:
    """
    Convert common XNA 3.x DepthStencilBuffer usage to FNA.

    FNA associates depth/stencil buffers with RenderTarget2D.
    SetRenderTarget() handles depth-buffer switching automatically.

    Returns the number of modified files.
    """

    root = Path(root)
    log_callback = log_callback or (lambda message, color=None: None)

    if not root.is_dir():
        log_callback(f"DepthStencil fix: Directory not found: {root}", "red")
        return 0

    modified_files = 0

    # Remove legacy DepthStencilBuffer variable declarations.
    declaration_pattern = re.compile(
        r"^[ \t]*"
        r"(?:(?:public|private|protected|internal|static|readonly)\s+)*"
        r"DepthStencilBuffer\s+\w+\s*;\s*\r?\n",
        re.MULTILINE,
    )

    # Remove legacy DepthStencilBuffer object creation.
    creation_pattern = re.compile(
        r"^[ \t]*"
        r"(?:(?:DepthStencilBuffer\s+)?\w+\s*=\s*)"
        r"new\s+DepthStencilBuffer\s*\([^;]*\)\s*;\s*\r?\n",
        re.MULTILINE,
    )

    # Remove assignments that save the currently bound depth buffer.
    save_pattern = re.compile(
        r"^[ \t]*"
        r"(?:DepthStencilBuffer\s+)?\w+\s*=\s*"
        r"(?:\w+\.)*GraphicsDevice\.DepthStencilBuffer\s*;\s*\r?\n",
        re.MULTILINE,
    )

    # Remove assignments that switch or restore depth buffers.
    switch_pattern = re.compile(
        r"^[ \t]*"
        r"(?:\w+\.)*GraphicsDevice\.DepthStencilBuffer\s*=\s*"
        r"\w+\s*;\s*\r?\n",
        re.MULTILINE,
    )

    # Upgrade render targets that have DepthFormat.None.
    # Only applies to simple constructor expressions.
    render_target_pattern = re.compile(
        r"(new\s+RenderTarget2D\s*\([^;]*?,\s*)"
        r"DepthFormat\.None"
        r"(\s*\))",
        re.DOTALL,
    )

    for file in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in file.relative_to(root).parts
        ):
            continue

        try:
            original = file.read_text(encoding="utf-8-sig")
        except (OSError, UnicodeError) as exc:
            log_callback(f"Cannot read {file}: {exc}", "red")
            continue

        updated = original
        counts = {}

        for name, pattern in (
            ("declarations", declaration_pattern),
            ("creations", creation_pattern),
            ("saved buffers", save_pattern),
            ("buffer switches", switch_pattern),
        ):
            updated, count = pattern.subn("", updated)
            counts[name] = count

        # Only modify render targets in files where legacy
        # depth/stencil operations were actually removed.
        if sum(counts.values()):
            updated, count = render_target_pattern.subn(
                r"\g<1>DepthFormat.Depth24\g<2>",
                updated,
            )
            counts["render targets"] = count

        if updated == original:
            continue

        try:
            file.write_text(updated, encoding="utf-8")
        except OSError as exc:
            log_callback(f"Cannot write {file}: {exc}", "red")
            continue

        modified_files += 1

        details = ", ".join(
            f"{name}={count}"
            for name, count in counts.items()
            if count
        )

        log_callback(
            f"Fixed XNA DepthStencilBuffer: "
            f"{file.relative_to(root)} ({details})",
            "green",
        )

        if re.search(
            r"\bDepthStencilBuffer\b|\.DepthStencilBuffer\b",
            updated,
        ):
            log_callback(
                f"WARNING: Remaining legacy depth-buffer usage: "
                f"{file.relative_to(root)}",
                "yellow",
            )

    log_callback(
        f"DepthStencil fix complete: "
        f"{modified_files} files modified",
        "green",
    )

    return modified_files


def fix_xna_effect_pool(
    root: str | Path,
    log_callback=None,
) -> int:
    """
    Fix legacy XNA EffectPool usage in SkinnedModelBasicEffect.

    Converts:
        SkinnedModelBasicEffect(GraphicsDevice graphicsDevice, EffectPool effectPool)
            : base(graphicsDevice, SkinnedModelBasicEffectCode.Code,
                   CompilerOptions.None, effectPool)

    To:
        SkinnedModelBasicEffect(GraphicsDevice graphicsDevice)
            : base(graphicsDevice, SkinnedModelBasicEffectCode.Code)

    Also updates:
        new SkinnedModelBasicEffect(graphicsDevice, (EffectPool)null)

    To:
        new SkinnedModelBasicEffect(graphicsDevice)

    Returns the number of modified files.
    """

    root = Path(root)
    log_callback = log_callback or (lambda message, color=None: None)

    if not root.is_dir():
        log_callback(f"EffectPool fix: Directory not found: {root}", "red")
        return 0

    modified_files = 0

    constructor_pattern = re.compile(
        r"""
        (?P<prefix>
            (?:public|protected|internal|private)\s+
            SkinnedModelBasicEffect\s*
        )
        \(
            \s*GraphicsDevice\s+(?P<device>\w+)\s*,
            \s*EffectPool\s+\w+\s*
        \)
        \s*:\s*base\s*
        \(
            \s*(?P=device)\s*,
            \s*SkinnedModelBasicEffectCode\.Code\s*,
            \s*CompilerOptions\.None\s*,
            \s*\w+\s*
        \)
        """,
        re.VERBOSE,
    )

    usage_pattern = re.compile(
        r"""
        new\s+SkinnedModelBasicEffect\s*
        \(
            \s*(?P<device>[^,()]+?)\s*,
            \s*(?:\(EffectPool\)\s*)?null\s*
        \)
        """,
        re.VERBOSE,
    )

    for file in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in file.relative_to(root).parts
        ):
            continue

        try:
            original = file.read_text(encoding="utf-8-sig")
        except (OSError, UnicodeError) as exc:
            log_callback(f"EffectPool fix: Cannot read {file}: {exc}", "red")
            continue

        updated = original

        updated, constructor_count = constructor_pattern.subn(
            lambda match: (
                f"{match.group('prefix')}"
                f"(GraphicsDevice {match.group('device')})\n"
                f"        : base({match.group('device')}, "
                f"SkinnedModelBasicEffectCode.Code)"
            ),
            updated,
        )

        updated, usage_count = usage_pattern.subn(
            lambda match: (
                f"new SkinnedModelBasicEffect({match.group('device').strip()})"
            ),
            updated,
        )

        if updated == original:
            continue

        try:
            file.write_text(updated, encoding="utf-8")
        except OSError as exc:
            log_callback(f"EffectPool fix: Cannot write {file}: {exc}", "red")
            continue

        modified_files += 1

        log_callback(
            f"Fixed XNA EffectPool: {file.relative_to(root)} "
            f"(constructors: {constructor_count}, usages: {usage_count})",
            "green",
        )

    log_callback(
        f"EffectPool fix complete: {modified_files} files modified",
        "green",
    )

    return modified_files


def fix_xna_effect_pass(root, log_callback=None):
    """
    Convert XNA 3.x EffectPass.Begin()/End()
    to MonoGame/FNA EffectPass.Apply().

    Recursively processes C# files.
    """

    root = Path(root)
    log_callback = log_callback or (lambda message, color=None: None)

    total_files = 0
    total_changes = 0

    # Matches effect.CurrentTechnique.Passes[0].Begin()
    # and other common EffectPass expressions.
    pass_expr = (
        r"(?:\b[A-Za-z_]\w*(?:\.[A-Za-z_]\w*|\[[^\]\r\n]+\])*)"
        r"\.CurrentTechnique\.Passes\[[^\]\r\n]+\]"
    )

    begin_pattern = re.compile(
        rf"({pass_expr})\.Begin\s*\(\s*\)"
    )

    end_pattern = re.compile(
        rf"({pass_expr})\.End\s*\(\s*\)\s*;"
    )

    # Also supports variables explicitly named effectPass
    variable_begin = re.compile(
        r"\b(effectPass\w*|pass)\.Begin\s*\(\s*\)"
    )

    variable_end = re.compile(
        r"\b(effectPass\w*|pass)\.End\s*\(\s*\)\s*;"
    )

    for cs_file in root.rglob("*.cs"):
        try:
            original = cs_file.read_text(encoding="utf-8-sig")
            content = original
            changes = 0

            # Replace Begin() with Apply()
            content, count = begin_pattern.subn(
                r"\1.Apply()",
                content
            )
            changes += count

            content, count = variable_begin.subn(
                r"\1.Apply()",
                content
            )
            changes += count

            # Remove End() calls
            content, count = end_pattern.subn(
                "",
                content
            )
            changes += count

            content, count = variable_end.subn(
                "",
                content
            )
            changes += count

            if content != original:
                cs_file.write_text(content, encoding="utf-8")
                total_files += 1
                total_changes += changes

                log_callback(
                    f"Fixed EffectPass: {cs_file.name} "
                    f"({changes} changes)",
                    None
                )

        except (OSError, UnicodeError) as e:
            log_callback(
                f"Failed to fix EffectPass in {cs_file}: {e}",
                None
            )

    log_callback(
        f"EffectPass conversion complete: "
        f"{total_files} files, {total_changes} changes.",
        None
    )

    return total_changes


def fix_xna_vertex_buffer(root, log_callback=None):
    """
    Convert XNA 3.x VertexBuffer constructors to FNA/MonoGame.

    Before:
        new VertexBuffer(
            GraphicsDevice,
            MapVertices.Length * VertexPositionTexture.SizeInBytes,
            BufferUsage.WriteOnly
        )

    After:
        new VertexBuffer(
            GraphicsDevice,
            typeof(VertexPositionTexture),
            MapVertices.Length,
            BufferUsage.WriteOnly
        )
    """

    log_callback = log_callback or (lambda message, color=None: None)
    root = Path(root)

    pattern = re.compile(
        r'new\s+VertexBuffer\s*\(\s*'
        r'(?P<device>[\w.]+)\s*,\s*'
        r'(?P<count>[\w.]+)\s*\*\s*'
        r'(?P<vertex_type>\w+)\.SizeInBytes\s*,\s*'
        r'(?P<usage>BufferUsage\.\w+)\s*'
        r'\)',
        re.MULTILINE
    )

    fixed_files = 0
    replacements = 0

    for cs_file in root.rglob("*.cs"):
        try:
            original = cs_file.read_text(encoding="utf-8-sig")
            file_replacements = 0

            def replace_vertex_buffer(match):
                nonlocal file_replacements

                device = match.group("device")
                count = match.group("count")
                vertex_type = match.group("vertex_type")
                usage = match.group("usage")

                # Convert byte size into vertex count.
                if count.endswith(".Length"):
                    vertex_count = count
                else:
                    vertex_count = count

                file_replacements += 1

                return (
                    f"new VertexBuffer("
                    f"{device}, "
                    f"typeof({vertex_type}), "
                    f"{vertex_count}, "
                    f"{usage})"
                )

            converted = pattern.sub(replace_vertex_buffer, original)

            if converted != original:
                cs_file.write_text(converted, encoding="utf-8-sig")

                fixed_files += 1
                replacements += file_replacements

                log_callback(
                    f"Fixed {file_replacements} XNA VertexBuffer "
                    f"constructor(s): {cs_file.relative_to(root)}",
                    "green"
                )

        except Exception as e:
            log_callback(
                f"Error fixing XNA VertexBuffer in {cs_file}: {e}",
                "red"
            )

    log_callback(
        f"XNA VertexBuffer conversion complete: "
        f"{replacements} replacements in {fixed_files} files.",
        "green"
    )

    return replacements


def fix_xna_color_constructor(root, log_callback=None):
    """
    Convert unsupported XNA Color(Color, alpha) constructors
    to FNA/MonoGame-compatible expressions.
    """

    root = Path(root)

    pattern = re.compile(
        r'new\s+Color\s*\(\s*'
        r'(?P<color>Color\.\w+)\s*,\s*'
        r'\(byte\)\s*',
        re.MULTILINE
    )

    def find_closing_parenthesis(text, start):
        depth = 0

        for i in range(start, len(text)):
            if text[i] == '(':
                depth += 1
            elif text[i] == ')':
                depth -= 1

                if depth == 0:
                    return i

        return -1

    fixed_files = 0
    replacements = 0

    for cs_file in root.rglob("*.cs"):
        try:
            original = cs_file.read_text(encoding="utf-8-sig")
            code = original

            matches = list(pattern.finditer(code))
            file_replacements = 0

            for match in reversed(matches):
                color = match.group("color")
                start = match.start()
                alpha_start = match.end()

                depth = 1
                end = -1

                for i in range(alpha_start, len(code)):
                    if code[i] == '(':
                        depth += 1
                    elif code[i] == ')':
                        depth -= 1

                        if depth == 0:
                            end = i
                            break

                if end == -1:
                    continue

                alpha = code[alpha_start:end].strip()

                while alpha.startswith("(") and alpha.endswith(")"):
                    closing = find_closing_parenthesis(alpha, 0)

                    if closing != len(alpha) - 1:
                        break

                    alpha = alpha[1:-1].strip()

                # Preserve the original byte cast before normalising alpha.
                replacement = (
                    f"{color} * (((byte)({alpha})) / 255f)"
                )

                code = code[:start] + replacement + code[end + 1:]
                file_replacements += 1

            if code != original:
                cs_file.write_text(code, encoding="utf-8-sig")

                fixed_files += 1
                replacements += file_replacements

                log_callback(
                    f"Fixed {file_replacements} XNA Color constructor(s): "
                    f"{cs_file.relative_to(root)}",
                    "green"
                )

        except Exception as e:
            log_callback(
                f"Error fixing XNA Color constructors in {cs_file}: {e}",
                "red"
            )

    log_callback(
        f"XNA Color conversion complete: "
        f"{replacements} replacements in {fixed_files} files.",
        "green"
    )

    return replacements


def fix_xna_draw_primitives(root: str | Path, log_message) -> int:
    """
    Convert legacy XNA 3.x drawing APIs to FNA/MonoGame.

    Fixes:
    - Effect.Begin() / Effect.End()
    - EffectPass.Begin() -> EffectPass.Apply()
    - EffectPass.End() removal
    - GraphicsDevice.Vertices[index].SetSource()
    - GraphicsDevice.VertexDeclaration assignment
    - SamplerState MagFilter/MinFilter/MipFilter
    - Legacy RenderState alpha-test properties

    Supports graphics device variables such as:
    - device
    - GraphicsDevice
    - Global.GraphicsDevice
    - this.GraphicsDevice

    Returns the number of modified files.
    """

    root = Path(root)
    changed = 0

    # --------------------------------------------------
    # Regular expressions
    # --------------------------------------------------

    receiver = r"(?:this\.)?[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*"

    # Effect.Begin() / Effect.End()
    effect_begin_pattern = re.compile(
        r"(?m)^[ \t]*"
        r"(?:basicEffect|effect)\.Begin\s*\(\s*\)\s*;[ \t]*\r?\n?"
    )

    effect_end_pattern = re.compile(
        r"(?m)^[ \t]*"
        r"(?:basicEffect|effect)\.End\s*\(\s*\)\s*;[ \t]*\r?\n?"
    )

    # EffectPass.Begin() -> Apply()
    pass_begin_pattern = re.compile(
        r"\bpass\.Begin\s*\(\s*\)"
    )

    # EffectPass.End() removal
    pass_end_pattern = re.compile(
        r"(?m)^[ \t]*pass\.End\s*\(\s*\)\s*;[ \t]*\r?\n?"
    )

    # Sampler filter assignment
    sampler_assignment = (
        rf"(?P<device>{receiver})"
        r"\.SamplerStates\[(?P<index>\d+)\]"
        r"\.(?P<filter>MagFilter|MinFilter|MipFilter)"
        r"\s*=\s*TextureFilter\.Point\s*;"
    )

    sampler_line_pattern = re.compile(
        rf"(?m)^[ \t]*{sampler_assignment}[ \t]*(?:\r?\n|$)"
    )

    # Legacy alpha-test assignments
    alpha_test_pattern = re.compile(
        rf"(?m)^[ \t]*{receiver}"
        r"\.RenderState\."
        r"(?:AlphaTestEnable|AlphaFunction|ReferenceAlpha)"
        r"\s*=\s*[^;\r\n]+;[ \t]*(?:\r?\n|$)"
    )

    # Legacy vertex source
    vertex_source_pattern = re.compile(
        rf"(?m)^[ \t]*{receiver}"
        r"\.Vertices\[\d+\]\.SetSource\s*"
        r"\([^;\r\n]*\)\s*;[ \t]*(?:\r?\n|$)"
    )

    # Legacy vertex declaration
    vertex_declaration_pattern = re.compile(
        rf"(?m)^[ \t]*{receiver}"
        r"\.VertexDeclaration\s*=\s*[^;\r\n]+;"
        r"[ \t]*(?:\r?\n|$)"
    )

    # --------------------------------------------------
    # Sampler conversion
    # --------------------------------------------------

    def fix_sampler_groups(source: str) -> str:
        """
        Convert three consecutive Point filter assignments
        belonging to the same sampler.

        Preserves other sampler configurations.
        """

        lines = source.splitlines(keepends=True)
        output = []
        i = 0

        while i < len(lines):
            matches = []
            j = i

            while j < len(lines) and len(matches) < 3:
                match = sampler_line_pattern.fullmatch(lines[j])

                if not match:
                    break

                if matches:
                    first = matches[0]

                    if (
                        match.group("device") != first.group("device")
                        or match.group("index") != first.group("index")
                    ):
                        break

                matches.append(match)
                j += 1

            if len(matches) == 3:
                filters = {m.group("filter") for m in matches}

                if filters == {"MagFilter", "MinFilter", "MipFilter"}:
                    first = matches[0]
                    indent = re.match(r"[ \t]*", lines[i]).group()

                    newline = (
                        "\r\n" if lines[i].endswith("\r\n")
                        else "\n"
                    )

                    output.append(
                        f"{indent}{first.group('device')}"
                        f".SamplerStates[{first.group('index')}] "
                        f"= SamplerState.PointClamp;{newline}"
                    )

                    i = j
                    continue

            output.append(lines[i])
            i += 1

        return "".join(output)

    # --------------------------------------------------
    # Process C# files
    # --------------------------------------------------

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.parts
        ):
            continue

        try:
            text = path.read_text(encoding="utf-8-sig")
        except (OSError, UnicodeError) as exc:
            log_message(f"Skipping {path}: {exc}")
            continue

        original = text

        # Convert legacy Effect calls.
        text = effect_begin_pattern.sub("", text)
        text = effect_end_pattern.sub("", text)

        # Convert EffectPass calls.
        text = pass_begin_pattern.sub("pass.Apply()", text)
        text = pass_end_pattern.sub("", text)

        # Convert sampler filters.
        text = fix_sampler_groups(text)

        # Remove obsolete alpha-test properties.
        # WARNING: The original alpha cutoff is not preserved.
        if alpha_test_pattern.search(text):
            log_message(
                f"WARNING: Removing legacy alpha-test settings "
                f"in {path.name}; shader conversion may be required"
            )
            text = alpha_test_pattern.sub("", text)

        # Only remove vertex bindings when the file uses
        # DrawUserPrimitives and does not use buffer draws.
        uses_user_primitives = bool(
            re.search(r"\bDrawUserPrimitives\s*\(", text)
        )

        uses_buffer_primitives = bool(
            re.search(
                r"\bDraw(?:Indexed)?Primitives\s*\(",
                text
            )
        )

        if uses_user_primitives and not uses_buffer_primitives:
            text = vertex_source_pattern.sub("", text)
            text = vertex_declaration_pattern.sub("", text)

        # --------------------------------------------------
        # Save modified file
        # --------------------------------------------------

        if text != original:
            try:
                path.write_text(text, encoding="utf-8")
            except OSError as exc:
                log_message(f"Failed to update {path}: {exc}")
                continue

            changed += 1
            log_message(
                f"Fixed XNA 3.x drawing APIs: {path.name}"
            )

    if changed:
        log_message(
            f"Fixed XNA 3.x drawing APIs in {changed} file(s)"
        )

    return changed


def fix_xna_shader_profile(root: str | Path, log_message) -> int:
    """Remove obsolete XNA 3.x shader profile settings."""

    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^[ \t]*"
        r"(?:this\.)?\w+\."
        r"(?:MinimumVertexShaderProfile|MinimumPixelShaderProfile)"
        r"\s*=\s*"
        r"(?:ShaderProfile\.)?\w+\s*;\s*\n?"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8-sig")
        original = text

        text = pattern.sub("", text)

        if text != original:
            path.write_text(text, encoding="utf-8")
            changed += 1
            log_message(f"Fixed XNA 3.x shader profiles: {path.name}")

    return changed


def fix_xna_graphics_content(root: str | Path, log_message) -> int:
    """
    Convert XNA 3.x graphics content lifecycle methods to FNA/MonoGame.

    Fixes:
    - LoadGraphicsContent(bool) -> LoadContent()
    - UnloadGraphicsContent(bool) -> UnloadContent()
    - base.LoadGraphicsContent(...) -> base.LoadContent()
    - base.UnloadGraphicsContent(...) -> base.UnloadContent()
    - Remove obsolete loadAllContent/unloadAllContent conditions
    - Preserve the contents of conditional blocks
    - Skip conflicting existing modern overrides
    """

    root = Path(root)
    changed = 0

    lifecycle_methods = {
        "LoadGraphicsContent": ("LoadContent", "loadAllContent"),
        "UnloadGraphicsContent": ("UnloadContent", "unloadAllContent"),
    }

    method_pattern = re.compile(
        r"\b(?P<old>LoadGraphicsContent|UnloadGraphicsContent)"
        r"\s*\(\s*bool\s+(?P<param>\w+)\s*\)"
    )

    def find_matching_brace(text: str, opening: int) -> int | None:
        """Find matching closing brace for a C# block."""
        depth = 0

        for i in range(opening, len(text)):
            if text[i] == "{":
                depth += 1
            elif text[i] == "}":
                depth -= 1
                if depth == 0:
                    return i

        return None

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        try:
            text = path.read_text(encoding="utf-8-sig")
        except (OSError, UnicodeError) as exc:
            log_message(f"Skipping {path}: {exc}")
            continue

        original = text

        # Process from the end to avoid invalidating offsets.
        matches = list(method_pattern.finditer(text))

        for match in reversed(matches):
            old_name = match.group("old")
            param_name = match.group("param")
            new_name, _ = lifecycle_methods[old_name]

            # Only convert override declarations.
            declaration_start = text.rfind("\n", 0, match.start()) + 1
            declaration = text[declaration_start:match.start()]

            if not re.search(r"\boverride\b", declaration):
                continue

            # Find method body.
            opening = text.find("{", match.end())

            if opening == -1:
                continue

            closing = find_matching_brace(text, opening)

            if closing is None:
                continue

            # Avoid duplicate modern overrides.
            existing_override = re.search(
                rf"\boverride\s+void\s+{new_name}\s*\(\s*\)",
                text[:declaration_start] + text[closing + 1:]
            )

            if existing_override:
                log_message(
                    f"Skipping {old_name} in {path.name}: "
                    f"{new_name} already exists"
                )
                continue

            body = text[opening + 1:closing]

            # Convert base lifecycle calls.
            body = re.sub(
                rf"\bbase\.{old_name}\s*\(\s*[^)]*\)",
                f"base.{new_name}()",
                body,
            )

            # Remove simple obsolete boolean guards,
            # preserving the guarded statements.
            guard_pattern = re.compile(
                rf"\bif\s*\(\s*{re.escape(param_name)}\s*\)\s*\{{"
            )

            while True:
                guard = guard_pattern.search(body)

                if not guard:
                    break

                guard_open = body.find("{", guard.start())
                guard_close = find_matching_brace(body, guard_open)

                if guard_close is None:
                    break

                inner = body[guard_open + 1:guard_close]

                body = (
                    body[:guard.start()]
                    + inner
                    + body[guard_close + 1:]
                )

            # Replace references to the removed parameter only
            # when all occurrences were eliminated.
            if re.search(rf"\b{re.escape(param_name)}\b", body):
                log_message(
                    f"Skipping {old_name} in {path.name}: "
                    f"remaining reference to {param_name}"
                )
                continue

            new_method = (
                text[declaration_start:match.start()]
                + f"{new_name}()"
                + text[match.end():opening + 1]
                + body
                + text[closing:closing + 1]
            )

            text = (
                text[:declaration_start]
                + new_method
                + text[closing + 1:]
            )

        if text != original:
            try:
                path.write_text(text, encoding="utf-8")
            except OSError as exc:
                log_message(f"Failed to update {path}: {exc}")
                continue

            changed += 1
            log_message(
                f"Fixed XNA 3.x graphics content: {path.name}"
            )

    if changed:
        log_message(
            f"Fixed XNA 3.x graphics content in {changed} file(s)"
        )

    return changed


def fix_xna_sampler_states(root: str | Path, log_message) -> int:
    """Convert XNA 3.x sampler filter assignments to FNA/MonoGame."""

    root = Path(root)
    changed = 0

    # Match three consecutive Point filter assignments.
    pattern = re.compile(
        r"(?P<indent>^[ \t]*)"
        r"(?P<device>[\w.]+GraphicsDevice|GraphicsDevice)"
        r"\.SamplerStates\[(?P<index>\d+)\]"
        r"\.MagFilter\s*=\s*TextureFilter\.Point\s*;\s*"
        r"(?P=device)\.SamplerStates\[(?P=index)\]"
        r"\.MipFilter\s*=\s*TextureFilter\.Point\s*;\s*"
        r"(?P=device)\.SamplerStates\[(?P=index)\]"
        r"\.MinFilter\s*=\s*TextureFilter\.Point\s*;",
        re.MULTILINE,
    )

    def replace_filters(match):
        return (
            f"{match.group('indent')}"
            f"{match.group('device')}.SamplerStates"
            f"[{match.group('index')}] = SamplerState.PointClamp;"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        try:
            text = path.read_text(encoding="utf-8-sig")
        except (OSError, UnicodeError) as exc:
            log_message(f"Skipping {path}: {exc}")
            continue

        original = text
        text = pattern.sub(replace_filters, text)

        if text != original:
            try:
                path.write_text(text, encoding="utf-8")
            except OSError as exc:
                log_message(f"Failed to update {path}: {exc}")
                continue

            changed += 1
            log_message(f"Fixed XNA 3.x sampler states: {path.name}")

    return changed


def fix_xna_bloom_component(root: str | Path, log_message) -> int:
    """Fix XNA 3.x BloomComponent graphics APIs for FNA/MonoGame."""

    root = Path(root)
    changed = 0

    fixes = [
        # ResolveTexture2D -> RenderTarget2D
        (
            re.compile(r"\bResolveTexture2D\b"),
            "RenderTarget2D",
        ),

        # Old RenderTarget2D constructor.
        # new RenderTarget2D(device, width, height, 1, format)
        (
            re.compile(
                r"new\s+RenderTarget2D\s*\(\s*"
                r"(?P<device>[^,()]+)\s*,\s*"
                r"(?P<width>[^,()]+)\s*,\s*"
                r"(?P<height>[^,()]+)\s*,\s*"
                r"1\s*,\s*"
                r"(?P<format>[^,()]+)\s*\)"
            ),
            lambda m: (
                f"new RenderTarget2D("
                f"{m.group('device').strip()}, "
                f"{m.group('width').strip()}, "
                f"{m.group('height').strip()}, "
                f"false, {m.group('format').strip()}, "
                f"DepthFormat.None)"
            ),
        ),

        # renderTarget.GetTexture() -> renderTarget
        (
            re.compile(r"\.GetTexture\s*\(\s*\)"),
            "",
        ),

        # GraphicsDevice.SetRenderTarget(0, target)
        # -> GraphicsDevice.SetRenderTarget(target)
        (
            re.compile(
                r"\bGraphicsDevice\.SetRenderTarget\s*"
                r"\(\s*0\s*,\s*(?P<target>[^()]+?)\s*\)"
            ),
            lambda m: (
                f"GraphicsDevice.SetRenderTarget("
                f"{m.group('target').strip()})"
            ),
        ),

        # ResolveBackBuffer is unavailable.
        # Scene rendering must be redirected separately.
        (
            re.compile(
                r"(?m)^([ \t]*)"
                r"GraphicsDevice\.ResolveBackBuffer\s*"
                r"\(\s*([A-Za-z_]\w*)\s*\)\s*;"
            ),
            lambda m: (
                f"{m.group(1)}// TODO: Render scene into "
                f"{m.group(2)} before bloom processing.\n"
                f"{m.group(1)}// ResolveBackBuffer removed."
            ),
        ),
    ]

    # Replace legacy DrawFullscreenQuad implementation.
    draw_method_pattern = re.compile(
        r"private\s+void\s+DrawFullscreenQuad\s*"
        r"\(\s*Texture2D\s+texture\s*,\s*"
        r"int\s+width\s*,\s*"
        r"int\s+height\s*,\s*"
        r"Effect\s+effect\s*,\s*"
        r"IntermediateBuffer\s+currentBuffer\s*\)"
        r"\s*\{"
    )

    replacement_method = """private void DrawFullscreenQuad(
        Texture2D texture,
        int width,
        int height,
        Effect effect,
        IntermediateBuffer currentBuffer)
    {
        Effect activeEffect =
            showBuffer >= currentBuffer ? effect : null;

        spriteBatch.Begin(
            SpriteSortMode.Immediate,
            BlendState.Opaque,
            SamplerState.LinearClamp,
            DepthStencilState.None,
            RasterizerState.CullNone,
            activeEffect
        );

        spriteBatch.Draw(
            texture,
            new Rectangle(0, 0, width, height),
            Color.White
        );

        spriteBatch.End();
    }"""

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.parts
        ):
            continue

        try:
            text = path.read_text(encoding="utf-8-sig")
        except (OSError, UnicodeError) as exc:
            log_message(f"Skipping {path}: {exc}")
            continue

        original = text

        # Apply regular expression fixes.
        for pattern, replacement in fixes:
            text = pattern.sub(replacement, text)

        # Replace legacy fullscreen drawing method.
        match = draw_method_pattern.search(text)

        if match:
            opening_brace = text.find("{", match.start())
            depth = 0
            closing_brace = None

            for i in range(opening_brace, len(text)):
                if text[i] == "{":
                    depth += 1
                elif text[i] == "}":
                    depth -= 1

                    if depth == 0:
                        closing_brace = i
                        break

            if closing_brace is not None:
                old_method = text[match.start():closing_brace + 1]

                # Only rewrite the legacy XNA implementation.
                if (
                    "effect.Begin()" in old_method
                    and "spriteBatch.Begin(" in old_method
                ):
                    text = (
                        text[:match.start()]
                        + replacement_method
                        + text[closing_brace + 1:]
                    )

        if text != original:
            try:
                path.write_text(text, encoding="utf-8")
            except OSError as exc:
                log_message(f"Failed to update {path}: {exc}")
                continue

            changed += 1
            log_message(f"Fixed XNA 3.x BloomComponent: {path.name}")

    if changed:
        log_message(
            f"Fixed XNA 3.x BloomComponent in {changed} file(s)"
        )

    return changed


def fix_xna_vertex_elements(root: str | Path, log_message, framework: str = "monogame",) -> int:
    root = Path(root)

    if framework.lower() not in {"fna", "monogame"}:
        raise ValueError(f"Unsupported framework: {framework}")

    changed = 0

    # Replace legacy VertexDeclaration constructors.
    constructor_pattern = re.compile(
        r"new\s+VertexDeclaration\s*\(\s*"
        r"(?:[\w.]+\s*,\s*)?"
        r"VertexPositionTexture\.VertexElements\s*\)"
    )

    # Replace standalone VertexElements references.
    elements_pattern = re.compile(
        r"\bVertexPositionTexture\.VertexElements\b"
    )

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.relative_to(root).parts
        ):
            continue

        try:
            raw = path.read_bytes()
            bom = raw.startswith(b"\xef\xbb\xbf")
            text = raw.decode("utf-8-sig")

            updated, constructors = constructor_pattern.subn(
                "VertexPositionTexture.VertexDeclaration",
                text
            )

            updated, elements = elements_pattern.subn(
                "VertexPositionTexture.VertexDeclaration",
                updated
            )
            vertex_count_pattern = re.compile(
                r"\b(?P<buffer>\w+)\.SizeInBytes\s*/\s*"
                r"VertexPositionTexture\.SizeInBytes\b"
            )

            updated, vertex_counts = vertex_count_pattern.subn(
                lambda m: f"{m.group('buffer')}.VertexCount",
                updated
            )
            if updated != text:
                data = updated.encode("utf-8")
                if bom:
                    data = b"\xef\xbb\xbf" + data

                path.write_bytes(data)
                changed += 1

                log_message(
                    f"Fixed VertexElements: {path} "
                    f"(constructors={constructors}, "
                    f"references={elements}), "
                    f"vertex_counts={vertex_counts}"
                )

        except (OSError, UnicodeError) as exc:
            log_message(f"Error processing {path}: {exc}")

    log_message(
        f"Fixed {changed} file(s) "
        f"(fix_xna_vertex_elements)."
    )

    return changed


def fix_xna_index_buffer(root: str | Path, log_message, framework: str = "monogame") -> int:
    root = Path(root)
    changed = 0

    if framework.lower() not in {"fna", "monogame"}:
        raise ValueError(f"Unsupported framework: {framework}")

    pattern = re.compile(
        r"new\s+IndexBuffer\s*\(\s*"
        r"(?P<device>[\w.]+)\s*,\s*"
        r"2\s*\*\s*(?P<array>[\w.]+)\.Length\s*,\s*"
        r"(?P<usage>BufferUsage\.\w+)\s*,\s*"
        r"IndexElementSize\.SixteenBits\s*\)"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.relative_to(root).parts):
            continue

        original = path.read_text(encoding="utf-8-sig")

        updated, count = pattern.subn(
            lambda m: (
                f"new IndexBuffer("
                f"{m.group('device')}, "
                f"IndexElementSize.SixteenBits, "
                f"{m.group('array')}.Length, "
                f"{m.group('usage')})"
            ),
            original
        )

        if count:
            path.write_text(updated, encoding="utf-8")
            changed += count
            log_message(f"Fixed IndexBuffer: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} IndexBuffer constructor(s).")
    return changed


def fix_xna_renderstate(
    root: str | Path,
    log_message,
    framework: str = "monogame",
) -> int:
    root = Path(root)
    framework = framework.lower()

    if framework not in {"fna", "monogame"}:
        raise ValueError(f"Unsupported framework: {framework}")

    if not root.is_dir():
        raise FileNotFoundError(root)

    changed = 0

    device_pattern = (
        r"(?:"
        r"base\.Game"
        r"|this\.Game"
        r"|\(\(GameComponent\)this\)\.Game"
        r"|\(\(Game\)[^)]+\)"
        r"|[\w.]+"
        r")\.GraphicsDevice"
        r"|[\w.]+"
    )

    # Local RenderState declaration and consecutive assignments.
    local_pattern = re.compile(
        rf"(?m)^(?P<indent>[ \t]*)"
        rf"RenderState\s+(?P<var>\w+)\s*=\s*"
        rf"(?P<device>(?:{device_pattern}))\.RenderState\s*;"
        rf"(?P<assignments>(?:[ \t]*\r?\n"
        rf"[ \t]*(?P=var)\.\w+\s*=\s*[^;\r\n]+;)+)"
    )

    assignment_pattern = re.compile(
        r"(?m)^[ \t]*(?P<var>\w+)\."
        r"(?P<property>\w+)\s*=\s*"
        r"(?P<value>[^;\r\n]+);"
    )

    # Direct GraphicsDevice.RenderState assignments.
    direct_pattern = re.compile(
        rf"(?m)^(?P<indent>[ \t]*)"
        rf"(?P<device>(?:{device_pattern}))"
        rf"\.RenderState\.(?P<property>\w+)"
        rf"\s*=\s*(?P<value>[^;\r\n]+);"
    )

    def normalized(value):
        return re.sub(r"\s+", "", value)

    def boolean(value):
        value = normalized(value).lower()
        if value == "true":
            return True
        if value == "false":
            return False
        return None

    def convert_group(device, properties, indent, newline):
        """
        Convert a group of legacy assignments.

        Returns None if conversion is unsafe.
        """
        known = {
            "AlphaBlendEnable",
            "AlphaBlendOperation",
            "SourceBlend",
            "DestinationBlend",
            "DepthBufferEnable",
            "DepthBufferWriteEnable",
            "AlphaTestEnable",
            "CullMode",
        }

        if not set(properties).issubset(known):
            return None

        result = []

        # Depth state
        depth = properties.get("DepthBufferEnable")
        write = properties.get("DepthBufferWriteEnable")

        if depth is not None or write is not None:
            depth_enabled = (
                boolean(depth) if depth is not None else None
            )
            write_enabled = (
                boolean(write) if write is not None else None
            )

            if (
                (depth is not None and depth_enabled is None)
                or (write is not None and write_enabled is None)
            ):
                return None

            if depth_enabled is None or write_enabled is None:
                # A single assignment cannot safely replace
                # the entire DepthStencilState.
                return None

            if depth_enabled and write_enabled:
                state = "DepthStencilState.Default"
            elif not depth_enabled and not write_enabled:
                state = "DepthStencilState.None"
            else:
                return None

            result.append(
                f"{device}.DepthStencilState = {state};"
            )

        # Blend state
        blend_enabled = properties.get("AlphaBlendEnable")
        blend_keys = {
            "AlphaBlendOperation",
            "SourceBlend",
            "DestinationBlend",
        }

        if blend_enabled is not None:
            enabled = boolean(blend_enabled)

            if enabled is None:
                return None

            if not enabled:
                if blend_keys.intersection(properties):
                    return None
                result.append(
                    f"{device}.BlendState = BlendState.Opaque;"
                )
            else:
                operation = properties.get("AlphaBlendOperation")
                source = properties.get("SourceBlend")
                destination = properties.get("DestinationBlend")

                if operation is not None and normalized(operation) != "BlendFunction.Add":
                    return None

                if source is not None and normalized(source) not in {
                    "Blend.SourceAlpha",
                    "Blend.One",
                }:
                    return None

                if destination is not None and normalized(destination) not in {
                    "Blend.InverseSourceAlpha",
                    "Blend.Zero",
                }:
                    return None

                # Only use the preset when explicitly compatible.
                if (
                    source is not None
                    and destination is not None
                    and normalized(source) == "Blend.SourceAlpha"
                    and normalized(destination) == "Blend.InverseSourceAlpha"
                ):
                    result.append(
                        f"{device}.BlendState = BlendState.NonPremultiplied;"
                    )
                elif source is None and destination is None:
                    result.append(
                        f"{device}.BlendState = BlendState.AlphaBlend;"
                    )
                else:
                    return None

        elif blend_keys.intersection(properties):
            return None

        # Cull mode
        if "CullMode" in properties:
            value = normalized(properties["CullMode"])

            states = {
                "CullMode.None": "RasterizerState.CullNone",
                "CullMode.CullClockwiseFace":
                    "RasterizerState.CullClockwise",
                "CullMode.CullCounterClockwiseFace":
                    "RasterizerState.CullCounterClockwise",
                "(CullMode)1": "RasterizerState.CullNone",
                "(CullMode)2": "RasterizerState.CullClockwise",
                "(CullMode)3": "RasterizerState.CullCounterClockwise",
            }

            state = states.get(value)
            if state is None:
                return None

            result.append(
                f"{device}.RasterizerState = {state};"
            )

        # Alpha testing has no equivalent fixed-function state.
        if "AlphaTestEnable" in properties:
            enabled = boolean(properties["AlphaTestEnable"])
            if enabled is None:
                return None
            if enabled:
                result.append(
                    "// TODO: AlphaTestEnable requires an "
                    "alpha-test shader/effect."
                )

        if not result:
            return None

        return newline.join(indent + line for line in result)

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.relative_to(root).parts
        ):
            continue

        try:
            raw = path.read_bytes()
            bom = raw.startswith(b"\xef\xbb\xbf")
            text = raw.decode("utf-8-sig")
            original = text
            newline = "\r\n" if "\r\n" in text else "\n"

            # Convert local RenderState groups only when
            # the variable is not used elsewhere.
            def replace_local(match):
                var = match.group("var")
                device = match.group("device")
                indent = match.group("indent")

                assignments = match.group("assignments")
                properties = {}

                for item in assignment_pattern.finditer(assignments):
                    name = item.group("property")
                    if name in properties:
                        return match.group(0)
                    properties[name] = item.group("value")

                # Avoid removing a variable still used later.
                outside = (
                    text[:match.start()] +
                    text[match.end():]
                )
                if re.search(rf"\b{re.escape(var)}\b", outside):
                    return match.group(0)

                replacement = convert_group(
                    device, properties, indent, newline
                )

                return replacement if replacement else match.group(0)

            text = local_pattern.sub(replace_local, text)

            # Direct assignments are converted conservatively.
            def replace_direct(match):
                prop = match.group("property")
                value = match.group("value")
                device = match.group("device")
                indent = match.group("indent")

                if prop == "AlphaBlendEnable":
                    enabled = boolean(value)
                    if enabled is not None:
                        state = (
                            "BlendState.AlphaBlend"
                            if enabled else "BlendState.Opaque"
                        )
                        return (
                            f"{indent}{device}.BlendState = {state};"
                        )

                if prop == "CullMode":
                    result = convert_group(
                        device, {prop: value}, indent, newline
                    )
                    if result:
                        return result

                return match.group(0)

            text = direct_pattern.sub(replace_direct, text)

            if text != original:
                encoded = text.encode("utf-8")
                if bom:
                    encoded = b"\xef\xbb\xbf" + encoded

                path.write_bytes(encoded)
                changed += 1
                log_message(f"Fixed XNA RenderState: {path}")

        except (OSError, UnicodeError) as exc:
            log_message(f"Error processing {path}: {exc}")

    log_message(f"Fixed {changed} file(s) for {framework}.")
    return changed


def _xna_sprite_code_mask(text):
    """Keep source offsets while excluding comments and C# string literals."""
    pattern = r'//[^\r\n]*|/\*[\s\S]*?\*/|@"(?:""|[^"])*"|"(?:\\.|[^"\\])*"|\x27(?:\\.|[^\x27\\])*\x27'
    return re.sub(pattern, lambda m: re.sub(r'[^\r\n]', ' ', m.group()), text)


def fix_xna_spritebatch_begin(root: str | Path, log_message) -> int:
    """Convert legacy blend-first overloads without changing modern sort-first calls."""
    changed = 0
    blend_names = {'None': 'Opaque', 'AlphaBlend': 'NonPremultiplied', 'Additive': 'Additive'}
    blend_numbers = {'0': 'Opaque', '1': 'NonPremultiplied', '2': 'Additive'}
    sort_numbers = {'0': 'Immediate', '1': 'Deferred', '2': 'Texture', '3': 'BackToFront', '4': 'FrontToBack'}
    for path in Path(root).rglob('*.cs'):
        if any(part.lower() in {'bin', 'obj', '.git'} for part in path.parts):
            continue
        text = path.read_text(encoding='utf-8-sig')
        mask = _xna_sprite_code_mask(text)
        blend_variables = set(re.findall(r'\b(?:SpriteBlendMode|BlendState)\s+(\w+)\b', mask))
        effect_variables = set(re.findall(r'\b(?:Effect|BasicEffect)\s+(\w+)\b', mask))
        edits = []
        for call in re.finditer(r'\.Begin\s*\(', mask):
            start = call.end()
            depth = 0
            pieces = []
            piece_start = start
            end = None
            for position in range(start, len(mask)):
                character = mask[position]
                if character == ')' and depth == 0:
                    pieces.append(text[piece_start:position].strip())
                    end = position
                    break
                if character in '([{':
                    depth += 1
                elif character in ')]}':
                    depth -= 1
                elif character == ',' and depth == 0:
                    pieces.append(text[piece_start:position].strip())
                    piece_start = position + 1
            if end is None or not pieces or not pieces[0]:
                continue
            first = pieces[0]
            legacy = re.fullmatch(r'SpriteBlendMode\.(None|AlphaBlend|Additive)', first)
            numeric = re.fullmatch(r'\(\s*SpriteBlendMode\s*\)\s*([012])', first)
            if legacy:
                blend = 'BlendState.' + blend_names[legacy[1]]
            elif numeric:
                blend = 'BlendState.' + blend_numbers[numeric[1]]
            elif re.fullmatch(r'BlendState\.(Opaque|AlphaBlend|NonPremultiplied|Additive)', first):
                # Earlier converter versions left blend-first calls behind. Only
                # those invalid calls get the legacy AlphaBlend correction.
                blend = first.replace('BlendState.AlphaBlend', 'BlendState.NonPremultiplied')
            elif first in blend_variables or first.rsplit('.', 1)[-1] in blend_variables:
                blend = first
            else:
                continue
            sort = 'SpriteSortMode.Deferred'
            remaining = pieces[1:]
            if remaining:
                sort_arg = remaining.pop(0)
                numeric_sort = re.fullmatch(r'\(\s*SpriteSortMode\s*\)\s*([0-4])', sort_arg)
                if numeric_sort:
                    sort = 'SpriteSortMode.' + sort_numbers[numeric_sort[1]]
                elif re.fullmatch(r'SpriteSortMode\.\w+', sort_arg):
                    sort = sort_arg
                else:
                    continue
            if remaining and re.fullmatch(r'(?:SaveStateMode\.(?:None|SaveState)|\(\s*SaveStateMode\s*\)\s*[01])', remaining[0]):
                saved_state = remaining.pop(0)
                if saved_state.endswith('SaveState') or re.search(r'\)\s*1$', saved_state):
                    log_message(f'{path}: legacy SaveState removed; later drawing must set its graphics states explicitly.')
            effect = None
            transform = None
            if len(remaining) > 1:
                continue
            if remaining:
                argument = remaining[0]
                if argument.startswith('effect:'):
                    effect = argument.split(':', 1)[1].strip()
                elif argument in effect_variables or argument.lower().endswith('effect'):
                    effect = argument
                else:
                    transform = argument.removeprefix('transformMatrix:').strip()
            arguments = [sort, blend]
            if effect is not None or transform is not None:
                arguments += ['null', 'null', 'null', effect or 'null']
                if transform is not None:
                    arguments.append(transform)
            edits.append((start, end, ', '.join(arguments)))
        for start, end, replacement in reversed(edits):
            text = text[:start] + replacement + text[end:]
        if edits:
            path.write_text(text, encoding='utf-8')
            changed += len(edits)
            log_message(f'Fixed SpriteBatch.Begin: {path} ({len(edits)} occurrence(s))')
    return changed


def fix_xna_sprite_blend_mode(root, log):
    """Convert calls before replacing enum declarations, including field variables."""
    fix_xna_spritebatch_begin(root, log)
    total = 0
    tokens = re.compile(r'SpriteBlendMode\.(?:AlphaBlend|Additive|None)|\(\s*SpriteBlendMode\s*\)\s*[012]|\bSpriteBlendMode\b')
    replacements = {'AlphaBlend': 'NonPremultiplied', 'Additive': 'Additive', 'None': 'Opaque',
                    '0': 'Opaque', '1': 'NonPremultiplied', '2': 'Additive'}
    for file in Path(root).rglob('*.cs'):
        if any(part.lower() in {'bin', 'obj', '.git'} for part in file.parts):
            continue
        original = file.read_text(encoding='utf-8-sig')
        content = original
        for match in reversed(list(tokens.finditer(_xna_sprite_code_mask(original)))):
            token = match.group()
            if token == 'SpriteBlendMode':
                replacement = 'BlendState'
            else:
                value = token.rsplit('.', 1)[-1] if '.' in token else token[-1]
                replacement = 'BlendState.' + replacements[value]
            content = content[:match.start()] + replacement + content[match.end():]
        if content != original:
            file.write_text(content, encoding='utf-8')
            total += 1
            log(f'Fixed SpriteBlendMode: {file}', 'green')
    return total





def fix_skinned_model_effect(root: str | Path, log_message) -> int:
    """Fix XNA 3.x SkinnedModelBasicEffect constructors for FNA/MonoGame."""
    root = Path(root)
    changed = 0

    fixes = [
        (
            re.compile(
                r"protected\s+SkinnedModelBasicEffect\(\s*"
                r"GraphicsDevice\s+graphicsDevice,\s*"
                r"SkinnedModelBasicEffect\s+clone\s*\)\s*"
                r":\s*base\(\s*graphicsDevice,\s*"
                r"\(Effect\)\s*\(object\)\s*clone\s*\)"
            ),
            "protected SkinnedModelBasicEffect(GraphicsDevice graphicsDevice, "
            "SkinnedModelBasicEffect clone)\n"
            "        : base(graphicsDevice, clone)"
        ),
        (
            re.compile(
                r"public\s+SkinnedModelBasicEffect\(\s*"
                r"GraphicsDevice\s+graphicsDevice,\s*"
                r"EffectPool\s+effectPool\s*\)\s*"
                r":\s*base\(\s*graphicsDevice,\s*"
                r"SkinnedModelBasicEffectCode\.Code,\s*"
                r"\(CompilerOptions\)0,\s*effectPool\s*\)"
            ),
            "public SkinnedModelBasicEffect(GraphicsDevice graphicsDevice)\n"
            "        : base(graphicsDevice, SkinnedModelBasicEffectCode.Code)"
        ),
        (
            re.compile(
                r"new\s+SkinnedModelBasicEffect\(\s*"
                r"(?P<device>[^,;()]+),\s*"
                r"\(EffectPool\)null\s*\)"
            ),
            lambda m: f"new SkinnedModelBasicEffect({m.group('device').strip()})"
        ),
    ]

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        updated = text
        count = 0

        # Apply constructor and instantiation fixes.
        for pattern, replacement in fixes:
            updated, n = pattern.subn(replacement, updated)
            count += n

        if count:
            path.write_text(updated, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} SkinnedModelBasicEffect occurrence(s).")
    return changed


def fix_xna_render_target(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^[ \t]*DSB\s*=\s*new\s+DepthStencilBuffer\("
        r"\(\(Game\)this\)\.GraphicsDevice,\s*"
        r"Width,\s*Height,\s*"
        r"\(\(Game\)this\)\.GraphicsDevice\.DepthStencilBuffer\.Format"
        r"\);\s*\n"
        r"(?P<indent>[ \t]*)GraphicsDevice\s+graphicsDevice\s*=\s*"
        r"\(\(Game\)this\)\.GraphicsDevice;\s*\n"
        r"(?P=indent)int\s+width\s*=\s*Width;\s*\n"
        r"(?P=indent)int\s+height\s*=\s*Height;\s*\n"
        r"(?P=indent)displayMode\s*=\s*"
        r"\(\(Game\)this\)\.GraphicsDevice\.DisplayMode;\s*\n"
        r"(?P=indent)renderTarget\s*=\s*new\s+RenderTarget2D\("
        r"graphicsDevice,\s*width,\s*height,\s*1,\s*displayMode\.Format"
        r"\);"
    )

    replacement = (
        r"\g<indent>displayMode = ((Game)this).GraphicsDevice.DisplayMode;\n"
        r"\g<indent>renderTarget = new RenderTarget2D(\n"
        r"\g<indent>    ((Game)this).GraphicsDevice,\n"
        r"\g<indent>    Width,\n"
        r"\g<indent>    Height,\n"
        r"\g<indent>    false,\n"
        r"\g<indent>    displayMode.Format,\n"
        r"\g<indent>    DepthFormat.Depth24);"
    )

    # Direct RenderTarget2D constructor.
    direct_pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"renderTarget\s*=\s*new\s+RenderTarget2D\("
        r"(?P<device>"
        r"\(\(Game\)this\)\.GraphicsDevice"
        r"|base\.GraphicsDevice"
        r"),\s*"
        r"(?P<width>Width),\s*"
        r"(?P<height>Height),\s*"
        r"1,\s*"
        r"(?P=device)\.DisplayMode\.Format"
        r"\);"
    )

    def direct_replacement(match: re.Match) -> str:
        indent = match.group("indent")
        device = match.group("device")

        return (
            f"{indent}renderTarget = new RenderTarget2D(\n"
            f"{indent}\t{device},\n"
            f"{indent}\tWidth,\n"
            f"{indent}\tHeight,\n"
            f"{indent}\tfalse,\n"
            f"{indent}\t{device}.DisplayMode.Format,\n"
            f"{indent}\tDepthFormat.Depth24);"
        )

    render_target_pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)if\s*\(multiSample\)\s*\n"
        r"(?P=indent)\{\s*\n"
        r"(?P=indent)[ \t]*oldDSB\s*=\s*"
        r"\(\(Game\)this\)\.GraphicsDevice\.DepthStencilBuffer;\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"SetRenderTarget\(0,\s*renderTarget\);\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"DepthStencilBuffer\s*=\s*DSB;\s*\n"
        r"(?P=indent)\}"
    )

    render_target_replacement = (
        r"\g<indent>if (multiSample)\n"
        r"\g<indent>{\n"
        r"\g<indent>\t((Game)this).GraphicsDevice.SetRenderTarget(renderTarget);\n"
        r"\g<indent>}"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        new_text, direct_count = direct_pattern.subn(
            direct_replacement,
            new_text,
        )

        new_text, render_count = render_target_pattern.subn(
            render_target_replacement,
            new_text,
        )

        total = count + direct_count + render_count

        if total:
            path.write_text(new_text, encoding="utf-8")
            changed += total
            log_message(f"Fixed: {path} ({total} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_render_target_draw(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)if\s*\(multiSample\)\s*\n"
        r"(?P=indent)\{\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"SetRenderTarget\(0,\s*\(RenderTarget2D\)null\);\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"DepthStencilBuffer\s*=\s*oldDSB;\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"Clear\(Color\.Black\);\s*\n"
        r"(?P=indent)[ \t]*SpriteBatch\.Begin\("
        r"\(SpriteBlendMode\)1,\s*\(SpriteSortMode\)0,\s*"
        r"\(SaveStateMode\)0\);\s*\n"
        r"(?P=indent)[ \t]*SpriteBatch\.Draw\("
        r"renderTarget\.GetTexture\(\),\s*"
        r"new Rectangle\(0,\s*0,\s*realWidth,\s*realHeight\),\s*"
        r"Color\.White\);\s*\n"
        r"(?P=indent)[ \t]*SpriteBatch\.End\(\);\s*\n"
        r"(?P=indent)\}\s*\n"
        r"(?P=indent)\(\(Game\)this\)\.Draw\(gameTime\);"
    )

    replacement = (
        r"\g<indent>if (multiSample)\n"
        r"\g<indent>{\n"
        r"\g<indent>\t((Game)this).GraphicsDevice.SetRenderTarget(null);\n"
        r"\g<indent>\t((Game)this).GraphicsDevice.Clear(Color.Black);\n"
        r"\g<indent>\n"
        r"\g<indent>\tSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);\n"
        r"\g<indent>\tSpriteBatch.Draw(renderTarget, new Rectangle(0, 0, realWidth, realHeight), Color.White);\n"
        r"\g<indent>\tSpriteBatch.End();\n"
        r"\g<indent>}\n"
        r"\g<indent>base.Draw(gameTime);"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_sound_effect_play(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^([ \t]*)(?P<var>\w+)\s*=\s*"
        r"(?P<sound>\w+)\.Play\(\s*"
        r"(?:(?P<volume>[\d.]+f?)\s*)?\)\s*;"
    )

    def replacement(match: re.Match) -> str:
        indent = match.group(1)
        var = match.group("var")
        sound = match.group("sound")
        volume = match.group("volume")

        if volume:
            return (
                f"{indent}{var} = {sound}.CreateInstance();\n"
                f"{indent}{var}.Volume = {volume};\n"
                f"{indent}{var}.Play();"
            )

        return (
            f"{indent}{var} = {sound}.CreateInstance();\n"
            f"{indent}{var}.Play();"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_vertex_streams(root: str | Path, log_message, framework: str = "monogame",) -> int:
    root = Path(root)
    changed = 0

    if framework.lower() not in {"fna", "monogame"}:
        raise ValueError(f"Unsupported framework: {framework}")

    pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"(?P<device>[\w.]+)\.VertexDeclaration\s*=\s*"
        r"(?P<declaration>[\w.]+)\s*;[ \t]*\r?\n"
        r"[ \t]*(?P=device)\.Vertices\[0\]\.SetSource\s*\(\s*"
        r"(?P<buffer>[\w.]+)\s*,\s*0\s*,\s*"
        r"(?P<stride>[\w.]+)\s*\)\s*;"
    )

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.relative_to(root).parts
        ):
            continue

        try:
            raw = path.read_bytes()
            bom = raw.startswith(b"\xef\xbb\xbf")
            original = raw.decode("utf-8-sig")

            updated, count = pattern.subn(
                lambda m: (
                    f"{m.group('indent')}"
                    f"{m.group('device')}.SetVertexBuffer("
                    f"{m.group('buffer')});"
                ),
                original
            )

            if count:
                data = updated.encode("utf-8")
                if bom:
                    data = b"\xef\xbb\xbf" + data

                path.write_bytes(data)
                changed += count

                log_message(
                    f"Fixed VertexStreams: {path} "
                    f"({count} occurrence(s))"
                )

        except (OSError, UnicodeError) as exc:
            log_message(f"Error processing {path}: {exc}")

    log_message(
        f"Fixed {changed} "
        f"(fix_xna_vertex_streams) occurrence(s)."
    )

    return changed


def fix_xna_effect_begin_end(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    sprite_pattern = re.compile(
        r"(?m)"
        r"^(?P<indent>[ \t]*)"
        r"(?P<sprite>[\w.]+)\.Begin\("
        r"\(SpriteBlendMode\)1,\s*"
        r"\(SpriteSortMode\)0,\s*"
        r"\(SaveStateMode\)[01]\);\s*\n"
        r"(?P=indent)effect\.Parameters\[\"SetDepth\"\]\.SetValue\(depth\);\s*\n"
        r"(?P=indent)effect\.Parameters\[\"SwayPhase\"\]\.SetValue\(sway\);\s*\n"
        r"(?P=indent)effect\.Parameters\[\"SwayAmount\"\]\.SetValue\(SwayAmount\);\s*\n"
        r"(?P=indent)(?:effect\.Begin\(\);\s*\n)?"
        r"(?P=indent)(?:effect\.CurrentTechnique\.Passes\[0\]\.Begin\(\);\s*\n)?"
        r"(?P=indent)(?:base\.Game\.GraphicsDevice\.)?"
        r"DepthStencilState\s*=\s*DepthStencilState\.Default;\s*\n"
        r"(?P=indent)(?P=sprite)\.Draw\("
        r"(?P<draw>[^;]+)\);\s*\n"
        r"(?P=indent)(?:effect\.CurrentTechnique\.Passes\[0\]\.End\(\);\s*\n)?"
        r"(?P=indent)(?:effect\.End\(\);\s*\n)?"
        r"(?P=indent)(?P=sprite)\.End\(\);"
    )

    floaty_pattern = re.compile(
        r"(?m)"
        r"^(?P<indent>[ \t]*)"
        r"floatyEffect\.Begin\(\);\s*\n"
        r"(?P=indent)floatyEffect\.CurrentTechnique\.Passes\[0\]\.Begin\(\);\s*\n"
        r"(?P=indent)base\.Game\.GraphicsDevice\.DrawUserPrimitives\("
        r"(?P<draw>[^;]+)\);\s*\n"
        r"(?P=indent)floatyEffect\.CurrentTechnique\.Passes\[0\]\.End\(\);\s*\n"
        r"(?P=indent)floatyEffect\.End\(\);"
    )

    def sprite_replacement(match: re.Match) -> str:
        indent = match.group("indent")
        sprite = match.group("sprite")
        draw = match.group("draw")

        return (
            f"{indent}effect.Parameters[\"SetDepth\"].SetValue(depth);\n"
            f"{indent}effect.Parameters[\"SwayPhase\"].SetValue(sway);\n"
            f"{indent}effect.Parameters[\"SwayAmount\"].SetValue(SwayAmount);\n"
            f"{indent}base.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;\n"
            f"{indent}{sprite}.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, effect);\n"
            f"{indent}{sprite}.Draw({draw});\n"
            f"{indent}{sprite}.End();"
        )

    def floaty_replacement(match: re.Match) -> str:
        indent = match.group("indent")
        draw = match.group("draw")

        return (
            f"{indent}floatyEffect.CurrentTechnique.Passes[0].Apply();\n"
            f"{indent}base.Game.GraphicsDevice.DrawUserPrimitives({draw});"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")

        new_text, sprite_count = sprite_pattern.subn(
            sprite_replacement, text
        )

        new_text, floaty_count = floaty_pattern.subn(
            floaty_replacement, new_text
        )

        total = sprite_count + floaty_count

        if total:
            path.write_text(new_text, encoding="utf-8")
            changed += total
            log_message(f"Fixed: {path} ({total} occurrence(s))")

    log_message(f"Fixed {changed} (fix_xna_effect_begin_end) occurrence(s).")
    return changed


def fix_xna_render_target_restore(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)if\s*\(multiSample\)\s*\n"
        r"(?P=indent)\{\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"SetRenderTarget\(0,\s*\(RenderTarget2D\)null\);\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"DepthStencilBuffer\s*=\s*oldDSB;\s*\n"
        r"(?P=indent)[ \t]*\(\(Game\)this\)\.GraphicsDevice\."
        r"Clear\(Color\.Black\);\s*\n"
        r"(?P=indent)[ \t]*SpriteBatch\.Begin\("
        r"SpriteSortMode\.Immediate,\s*BlendState\.AlphaBlend\);\s*\n"
        r"(?P=indent)[ \t]*SpriteBatch\.Draw\("
        r"renderTarget\.GetTexture\(\),\s*"
        r"new Rectangle\(0,\s*0,\s*realWidth,\s*realHeight\),\s*"
        r"Color\.White\);\s*\n"
        r"(?P=indent)[ \t]*SpriteBatch\.End\(\);\s*\n"
        r"(?P=indent)\}\s*\n"
        r"(?P=indent)\(\(Game\)this\)\.Draw\(gameTime\);"
    )

    replacement = (
        r"\g<indent>if (multiSample)\n"
        r"\g<indent>{\n"
        r"\g<indent>\t((Game)this).GraphicsDevice.SetRenderTarget(null);\n"
        r"\g<indent>\t((Game)this).GraphicsDevice.Clear(Color.Black);\n"
        r"\g<indent>\tSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);\n"
        r"\g<indent>\tSpriteBatch.Draw(renderTarget, new Rectangle(0, 0, realWidth, realHeight), Color.White);\n"
        r"\g<indent>\tSpriteBatch.End();\n"
        r"\g<indent>}\n"
        r"\g<indent>base.Draw(gameTime);"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} (fix_xna_render_target_restore) occurrence(s).")
    return changed


def fix_xna_effect_commit_changes(
    root: str | Path,
    log_message,
    framework: str = "monogame",
) -> int:
    root = Path(root)

    if framework.lower() not in {"fna", "monogame"}:
        raise ValueError(f"Unsupported framework: {framework}")

    if not root.is_dir():
        raise FileNotFoundError(root)

    changed = 0

    commit_pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"(?P<effect>[\w.]+)\.CommitChanges\s*\(\s*\)\s*;"
        r"[ \t]*(?:\r?\n)?"
    )

    # Specific shader-instancing method, to avoid modifying
    # unrelated draw calls.
    method_pattern = re.compile(
        r"\bvoid\s+DrawShaderInstancing\s*\("
    )

    draw_pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"graphicsDevice\.DrawIndexedPrimitives\s*\("
    )

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.relative_to(root).parts
        ):
            continue

        try:
            raw = path.read_bytes()
            bom = raw.startswith(b"\xef\xbb\xbf")
            text = raw.decode("utf-8-sig")
            original = text
            newline = "\r\n" if "\r\n" in text else "\n"

            commits = 0
            passes = 0

            # First, replace obsolete CommitChanges calls.
            def replace_commit(match):
                nonlocal commits

                commits += 1

                # Leave a marker for reviewing rendering behavior.
                return (
                    f"{match.group('indent')}"
                    "// TODO: Verify effect pass is applied "
                    "after parameter updates." + newline
                )

            text = commit_pattern.sub(replace_commit, text)

            # Locate DrawShaderInstancing and its matching
            # method body using brace depth.
            match = method_pattern.search(text)

            if match:
                opening = text.find("{", match.end())

                if opening != -1:
                    depth = 0
                    closing = None

                    for pos in range(opening, len(text)):
                        if text[pos] == "{":
                            depth += 1
                        elif text[pos] == "}":
                            depth -= 1
                            if depth == 0:
                                closing = pos
                                break

                    if closing is not None:
                        body = text[opening:closing]

                        # Only insert Apply when the method
                        # updates parameters and draws geometry.
                        if (
                            'effect.Parameters["InstanceTransforms"].SetValue'
                            in body
                            and
                            'effect.Parameters["TextureNumber"].SetValue'
                            in body
                            and
                            "DrawIndexedPrimitives" in body
                        ):
                            draw = draw_pattern.search(body)

                            if draw:
                                # Avoid inserting Apply twice.
                                before_draw = body[:draw.start()]
                                recent = before_draw[-500:]

                                if not re.search(
                                    r"\.Apply\s*\(\s*\)\s*;",
                                    recent
                                ):
                                    indent = draw.group("indent")

                                    replacement = (
                                        f"{indent}"
                                        "effect.CurrentTechnique."
                                        "Passes[0].Apply();"
                                        + newline
                                        + draw.group(0)
                                    )

                                    body = (
                                        body[:draw.start()]
                                        + replacement
                                        + body[draw.end():]
                                    )

                                    text = (
                                        text[:opening]
                                        + body
                                        + text[closing:]
                                    )

                                    passes += 1

            if text != original:
                data = text.encode("utf-8")

                if bom:
                    data = b"\xef\xbb\xbf" + data

                path.write_bytes(data)
                changed += 1

                log_message(
                    f"Fixed Effect: {path} "
                    f"(CommitChanges={commits}, "
                    f"PassApply={passes})"
                )

        except (OSError, UnicodeError) as exc:
            log_message(f"Error processing {path}: {exc}")

    log_message(
        f"Fixed {changed} file(s) "
        f"(fix_xna_effect_commit_changes) for {framework}."
    )

    return changed


def fix_xna_render_target_depth_buffer(root: str | Path, log_message) -> int:
    """Convert XNA 3.x render targets and remove obsolete depth buffer fields."""
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"RenderTarget2D\s+(?P<target>\w+)\s*=\s*new\s+RenderTarget2D\("
        r"(?P<game>\(\(Game\)game\))\.GraphicsDevice,\s*"
        r"(?P<width>[^,]+),\s*(?P<height>[^,]+),\s*1,\s*\(SurfaceFormat\)1\);\s*\n"
        r"(?P=indent)DepthStencilBuffer\s+\w+\s*=\s*new\s+DepthStencilBuffer\("
        r"(?P=game)\.GraphicsDevice,\s*(?P=width),\s*(?P=height),\s*"
        r"\(\(GameComponent\)this\)\.Game\.GraphicsDevice\.DepthStencilBuffer\.Format\);\s*\n"
        r"(?P=indent)DepthStencilBuffer\s+(?P<old>\w+)\s*=\s*"
        r"\(\(GameComponent\)this\)\.Game\.GraphicsDevice\.DepthStencilBuffer;\s*\n"
        r"(?P=indent)\(\(GameComponent\)this\)\.Game\.GraphicsDevice\.DepthStencilBuffer\s*=\s*\w+;\s*\n"
        r"(?P=indent)\(\(GameComponent\)this\)\.Game\.GraphicsDevice\.SetRenderTarget\(0,\s*(?P=target)\);\s*\n"
        r"(?P=indent)game\.SpriteBatch\.Begin\("
        r"\(SpriteBlendMode\)0,\s*\(SpriteSortMode\)0,\s*\(SaveStateMode\)1\);\s*\n"
        r"(?P=indent)game\.SpriteBatch\.Draw\(screen,\s*Vector2\.Zero,\s*Color\.White\);\s*\n"
        r"(?P=indent)game\.SpriteBatch\.End\(\);\s*\n"
        r"(?P=indent)\(\(GameComponent\)this\)\.Game\.GraphicsDevice\.SetRenderTarget\("
        r"0,\s*\(RenderTarget2D\)null\);\s*\n"
        r"(?P=indent)\(\(GameComponent\)this\)\.Game\.GraphicsDevice\.DepthStencilBuffer\s*=\s*(?P=old);\s*\n"
        r"(?P=indent)depthMap\s*=\s*new\s+byte\[\s*screen\.Width,\s*screen\.Height\s*\];\s*\n"
        r"(?P=indent)Color\[\]\s+array\s*=\s*new\s+Color\[\s*screen\.Width\s*\*\s*screen\.Height\s*\];\s*\n"
        r"(?P=indent)(?P=target)\.GetTexture\(\)\.GetData<Color>\(array\);"
    )

    field_pattern = re.compile(
        r"(?m)^[ \t]*(?:(?:private|public|protected|internal|static|readonly)\s+)*"
        r"DepthStencilBuffer\s+(?P<name>\w+)\s*;[ \t]*\r?\n?"
    )

    # Convert standalone XNA 3.x RenderTarget2D constructors.
    standalone_pattern = re.compile(
        r"new\s+RenderTarget2D\(\s*"
        r"(?P<device>(?:\w+\.)*GraphicsDevice),\s*"
        r"(?P<width>[^,]+),\s*"
        r"(?P<height>[^,]+),\s*"
        r"(?P<level>\d+),\s*"
        r"(?P<format>SurfaceFormat\.\w+)\s*\)"
    )

    def replace_standalone(match):
        device = match.group("device")
        width = match.group("width").strip()
        height = match.group("height").strip()
        level = int(match.group("level"))
        surface_format = match.group("format")

        # XNA 3.x: 0 = no mipmaps, 1 = one mip level.
        mipmap = "true" if level > 1 else "false"

        return (
            f"new RenderTarget2D({device}, {width}, {height}, "
            f"{mipmap}, {surface_format}, DepthFormat.None)"
        )

    def replace(match):
        i, game, target = (match.group(k) for k in ("indent", "game", "target"))
        return "\n".join(i + line for line in (
            f"RenderTarget2D {target} = new RenderTarget2D(",
            f"\t{game}.GraphicsDevice,",
            "\tscreen.Width, screen.Height, false,",
            "\tSurfaceFormat.Color, DepthFormat.Depth24);",
            f"{game}.GraphicsDevice.SetRenderTarget({target});",
            "game.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);",
            "game.SpriteBatch.Draw(screen, Vector2.Zero, Color.White);",
            "game.SpriteBatch.End();",
            f"{game}.GraphicsDevice.SetRenderTarget(null);",
            "depthMap = new byte[screen.Width, screen.Height];",
            "Color[] array = new Color[screen.Width * screen.Height];",
            f"{target}.GetData<Color>(array);",
        ))

    for path in root.rglob("*.cs"):
        if any(p.lower() in {"bin", "obj"} for p in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        updated, count = pattern.subn(replace, text)
        updated, standalone_count = standalone_pattern.subn(
            replace_standalone, updated
        )
        count += standalone_count

        # Remove fields only when no references remain in the file.
        def remove_unused_field(match):
            name = match.group("name")
            remaining = updated[:match.start()] + updated[match.end():]
            return "" if not re.search(rf"\b{re.escape(name)}\b", remaining) else match.group()

        updated, removed = field_pattern.subn(remove_unused_field, updated)

        if updated != text:
            path.write_text(updated, encoding="utf-8")
            changed += count + removed
            log_message(f"Fixed: {path} ({count} render targets, {removed} fields)")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_render_state_alpha_blend(
    root: str | Path,
    log_message
) -> int:
    root = Path(root)
    changed = 0

    if not root.is_dir():
        log_message(f"Directory not found: {root}")
        return 0

    # Pattern 1:
    # graphics.GraphicsDevice.RenderState.AlphaBlendEnable = true;
    # graphics.GraphicsDevice.RenderState.AlphaBlendOperation = BlendFunction.Add;

    pattern_alpha_blend = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"(?P<receiver>[\w.]+)\.GraphicsDevice"
        r"\.RenderState\.AlphaBlendEnable\s*=\s*true\s*;[ \t]*\r?\n"
        r"[ \t]*(?P=receiver)\.GraphicsDevice"
        r"\.RenderState\.AlphaBlendOperation\s*=\s*BlendFunction\.Add\s*;"
    )

    def replace_alpha_blend(match: re.Match) -> str:
        indent = match.group("indent")
        receiver = match.group("receiver")

        return (
            f"{indent}{receiver}.GraphicsDevice."
            f"BlendState = BlendState.AlphaBlend;"
        )

    # Pattern 2:
    # RenderState renderState = device.RenderState;
    # renderState.DepthBufferEnable = true;
    # renderState.AlphaBlendEnable = true;
    # renderState.AlphaTestEnable = true;

    pattern_render_state = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"RenderState\s+(?P<var>\w+)\s*=\s*"
        r"(?P<device>[\w.]+)\.RenderState\s*;[ \t]*\r?\n"
        r"[ \t]*(?P=var)\.DepthBufferEnable\s*=\s*true\s*;[ \t]*\r?\n"
        r"[ \t]*(?P=var)\.AlphaBlendEnable\s*=\s*true\s*;[ \t]*\r?\n"
        r"[ \t]*(?P=var)\.AlphaTestEnable\s*=\s*true\s*;"
    )

    def replace_render_state(match: re.Match) -> str:
        indent = match.group("indent")
        device = match.group("device")

        return (
            f"{indent}{device}.DepthStencilState = "
            f"DepthStencilState.Default;\n"
            f"{indent}{device}.BlendState = "
            f"BlendState.AlphaBlend;\n"
            f"{indent}// TODO: Preserve legacy alpha-test "
            f"behavior using an alpha-test shader."
        )

    for path in root.rglob("*.cs"):
        if any(
            part.lower() in {"bin", "obj"}
            for part in path.relative_to(root).parts
        ):
            continue

        try:
            with path.open(
                "r",
                encoding="utf-8-sig",
                newline=""
            ) as file:
                text = file.read()

            new_text, count1 = pattern_alpha_blend.subn(
                replace_alpha_blend, text
            )

            new_text, count2 = pattern_render_state.subn(
                replace_render_state, new_text
            )

            count = count1 + count2

            if count:
                # Preserve the file's original newline style.
                if "\r\n" in text:
                    new_text = new_text.replace("\r\n", "\n")
                    new_text = new_text.replace("\n", "\r\n")

                with path.open(
                    "w",
                    encoding="utf-8",
                    newline=""
                ) as file:
                    file.write(new_text)

                changed += count

                log_message(
                    f"Fixed: {path} "
                    f"(AlphaBlend={count1}, RenderState={count2})"
                )

        except (OSError, UnicodeError) as exc:
            log_message(f"Error processing {path}: {exc}")

    log_message(
        f"Fixed {changed} "
        f"(fix_xna_render_state_alpha_blend) occurrence(s)."
    )

    return changed


def fix_xna_depth_stencil_fields(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^[ \t]*private\s+DepthStencilBuffer\s+"
        r"(?:DSB|oldDSB)\s*;\s*(?:\r?\n|$)"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn("", text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} (fix_xna_depth_stencil_fields) occurrence(s).")
    return changed


def fix_xna_render_target_setup(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)"
        r"^(?P<indent>[ \t]*)"
        r"if\s*\(\s*multiSample\s*\)\s*\r?\n"
        r"(?P=indent)\{\s*\r?\n"
        r"(?P=indent)[ \t]*oldDSB\s*=\s*"
        r"(?P<device>base\.GraphicsDevice|\(\(Game\)this\)\.GraphicsDevice)"
        r"\.DepthStencilBuffer\s*;\s*\r?\n"
        r"(?P=indent)[ \t]*"
        r"(?P=device)\.SetRenderTarget\(\s*0\s*,\s*renderTarget\s*\)\s*;\s*\r?\n"
        r"(?P=indent)[ \t]*"
        r"(?P=device)\.DepthStencilBuffer\s*=\s*DSB\s*;\s*\r?\n"
        r"(?P=indent)\}"
    )

    def replacement(match: re.Match) -> str:
        indent = match.group("indent")
        device = match.group("device")

        return (
            f"{indent}if (multiSample)\n"
            f"{indent}{{\n"
            f"{indent}\t{device}.SetRenderTarget(renderTarget);\n"
            f"{indent}}}"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_render_target_reset(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"(?P<device>[\w.]+)\.SetRenderTarget\(\s*0\s*,\s*null\s*\)\s*;"
        r"(?:\r?\n"
        r"(?P=indent)(?P=device)\.DepthStencilBuffer\s*=\s*oldDSB\s*;)?"
    )

    def replacement(match: re.Match) -> str:
        return (
            f"{match.group('indent')}"
            f"{match.group('device')}.SetRenderTarget(null);"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_render_target_get_texture(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    draw_pattern = re.compile(
        r"(?P<receiver>[\w.]+)\.Draw\(\s*"
        r"(?P<target>\w+)\.GetTexture\(\)\s*,\s*"
        r"(?P<args>[^;]+?)\s*\)\s*;"
    )

    data_pattern = re.compile(
        r"(?P<target>\w+)\.GetTexture\(\)\.GetData\("
    )

    def draw_replacement(match: re.Match) -> str:
        return (
            f"{match.group('receiver')}.Draw("
            f"{match.group('target')}, "
            f"{match.group('args')});"
        )

    def data_replacement(match: re.Match) -> str:
        return f"{match.group('target')}.GetData("

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")

        new_text, draw_count = draw_pattern.subn(draw_replacement, text)
        new_text, data_count = data_pattern.subn(data_replacement, new_text)

        count = draw_count + data_count

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(
        f"Fixed {changed} (fix_xna_render_target_get_texture) occurrence(s)."
    )
    return changed


def fix_xna_vertex_declaration(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"new\s+VertexDeclaration\(\s*"
        r"[\w.]+\.GraphicsDevice\s*,\s*"
        r"(?P<elements>[\w.]+\.VertexElements)\s*\)"
    )

    def replacement(match: re.Match) -> str:
        return f"new VertexDeclaration({match.group('elements')})"

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_screen_effect_begin_end(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)"
        r"^(?P<indent>[ \t]*)"
        r"(?P<effect>\w+)\.Begin\(\);\s*\n"
        r"(?P=indent)(?P=effect)\.CurrentTechnique\.Passes\[0\]\.Begin\(\);\s*\n"
        r"(?P=indent)base\.Game\.GraphicsDevice\.DepthStencilState\s*=\s*DepthStencilState\.Default;\s*\n"
        r"(?P=indent)(?P<sprite>[\w.]+)\.Draw\("
        r"(?P<draw>[^;]+)\);\s*\n"
        r"(?P=indent)(?P=effect)\.CurrentTechnique\.Passes\[0\]\.End\(\);\s*\n"
        r"(?P=indent)(?P=effect)\.End\(\);"
    )

    def replacement(match: re.Match) -> str:
        indent = match.group("indent")
        effect = match.group("effect")
        sprite = match.group("sprite")
        draw = match.group("draw")

        return (
            f"{indent}base.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;\n"
            f"{indent}{sprite}.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, {effect});\n"
            f"{indent}{sprite}.Draw({draw});\n"
            f"{indent}{sprite}.End();"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_render_target_setup_text(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^(?P<indent>[ \t]*)"
        r"RenderTarget2D\s+(?P<target>\w+)\s*=\s*new\s+RenderTarget2D\("
        r"(?P<device>[\w.]+),\s*"
        r"(?P<width>[^,]+),\s*"
        r"(?P<height>[^,]+),\s*"
        r"1,\s*"
        r"SurfaceFormat\.Color\s*\);\s*\n"
        r"(?P=indent)DepthStencilBuffer\s+\w+\s*=\s*new\s+DepthStencilBuffer\("
        r"[^;]+;\s*\n"
        r"(?P=indent)DepthStencilBuffer\s+\w+\s*=\s*"
        r"[\w.]+\.DepthStencilBuffer\s*;\s*\n"
        r"(?P=indent)[\w.]+\.DepthStencilBuffer\s*=\s*\w+\s*;\s*\n"
        r"(?P=indent)(?P<setdevice>[\w.]+)\.SetRenderTarget\(\s*0\s*,\s*"
        r"(?P=target)\s*\)\s*;"
    )

    def replacement(match: re.Match) -> str:
        indent = match.group("indent")
        target = match.group("target")
        device = match.group("device")
        width = match.group("width").strip()
        height = match.group("height").strip()
        setdevice = match.group("setdevice")

        return (
            f"{indent}RenderTarget2D {target} = new RenderTarget2D(\n"
            f"{indent}    {device},\n"
            f"{indent}    {width},\n"
            f"{indent}    {height},\n"
            f"{indent}    false,\n"
            f"{indent}    SurfaceFormat.Color,\n"
            f"{indent}    DepthFormat.Depth24);\n"
            f"{indent}{setdevice}.SetRenderTarget({target});"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_render_target_restore_texture(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^[ \t]*"
        r"(?P<device>[\w.]+)\.DepthStencilBuffer\s*=\s*depthStencilBuffer2\s*;\s*\n"
        r"(?P<indent>[ \t]*)"
        r"(?P<texture>\w+)\s*=\s*(?P<target>\w+)\.GetTexture\(\)\s*;"
    )

    def replacement(match: re.Match) -> str:
        return (
            f"{match.group('indent')}"
            f"{match.group('texture')} = {match.group('target')};"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_vector3_interpolation(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?P<method>Vector3\.(?:Lerp|SmoothStep))\("
        r"(?P<a>ref\s+[\w.]+)\s*,\s*"
        r"(?P<b>ref\s+[\w.]+)\s*,\s*"
        r"(?P<amount>[^,]+)\s*,\s*"
        r"ref\s+(?P<result>[\w.]+)"
        r"\)"
    )

    def replacement(match: re.Match) -> str:
        return (
            f"{match.group('method')}("
            f"{match.group('a')}, "
            f"{match.group('b')}, "
            f"{match.group('amount').strip()}, "
            f"out {match.group('result')})"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_interpolation(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?P<method>(?:Vector3|Quaternion)\.(?:Lerp|Slerp|SmoothStep))\("
        r"(?P<a>ref\s+[\w.]+)\s*,\s*"
        r"(?P<b>ref\s+[\w.]+)\s*,\s*"
        r"(?P<amount>[^,]+)\s*,\s*"
        r"ref\s+(?P<result>[\w.]+)"
        r"\)"
    )

    def replacement(match: re.Match) -> str:
        return (
            f"{match.group('method')}("
            f"{match.group('a')}, "
            f"{match.group('b')}, "
            f"{match.group('amount').strip()}, "
            f"out {match.group('result')})"
        )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_skinned_model_effect_pool(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"new\s+SkinnedModelBasicEffect\(\s*"
        r"(?P<device>[\w.]+)\s*,\s*"
        r"\(EffectPool\)\s*null\s*\)"
    )

    def replacement(match: re.Match) -> str:
        return f"new SkinnedModelBasicEffect({match.group('device')})"

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn(replacement, text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def fix_xna_depth_stencil_restore(root: str | Path, log_message) -> int:
    root = Path(root)
    changed = 0

    pattern = re.compile(
        r"(?m)^[ \t]*"
        r"[\w.]+\.DepthStencilBuffer\s*=\s*depthStencilBuffer2\s*;\s*\r?\n?"
    )

    for path in root.rglob("*.cs"):
        if any(part.lower() in {"bin", "obj"} for part in path.parts):
            continue

        text = path.read_text(encoding="utf-8")
        new_text, count = pattern.subn("", text)

        if count:
            path.write_text(new_text, encoding="utf-8")
            changed += count
            log_message(f"Fixed: {path} ({count} occurrence(s))")

    log_message(f"Fixed {changed} occurrence(s).")
    return changed


def repair_sources(root, log_message):
    fix_xna_storage_container_path(root, log_message)
    fix_unused_storage_container_paths(root, log_message)
    fix_xna_signed_in_gamer_avatar(root, log_message)
    fix_xna_depth_stencil_buffer(root, log_message)
    fix_xna_effect_pool(root, log_message)
    fix_xna_effect_pass(root, log_message)
    fix_xna_vertex_buffer(root, log_message)
    fix_xna_color_constructor(root, log_message)
    fix_xna_draw_primitives(root, log_message)
    fix_xna_shader_profile(root, log_message)
    fix_xna_graphics_content(root, log_message)
    fix_xna_sampler_states(root, log_message)
    fix_xna_bloom_component(root, log_message)
    fix_xna_vertex_elements(root, log_message)
    fix_xna_index_buffer(root, log_message)
    fix_xna_renderstate(root, log_message, framework="fna")
    fix_xna_spritebatch_begin(root, log_message)
    fix_xna_sprite_blend_mode(root, log_message)
    fix_skinned_model_effect(root, log_message)
    fix_xna_render_target(root, log_message)
    fix_xna_render_target_draw(root, log_message)
    fix_xna_sound_effect_play(root, log_message)
    fix_xna_vertex_streams(root, log_message)
    fix_xna_effect_begin_end(root, log_message)
    fix_xna_render_target_restore(root, log_message)
    fix_xna_effect_commit_changes(root, log_message)
    fix_xna_render_target_depth_buffer(root, log_message)
    fix_xna_render_state_alpha_blend(root, log_message)
    fix_xna_depth_stencil_fields(root, log_message)
    fix_xna_render_target_setup(root, log_message)
    fix_xna_render_target_reset(root, log_message)
    fix_xna_render_target_get_texture(root, log_message)
    fix_xna_vertex_declaration(root, log_message)
    fix_xna_screen_effect_begin_end(root, log_message)
    fix_xna_render_target_setup_text(root, log_message)
    fix_xna_render_target_restore_texture(root, log_message)
    fix_xna_vector3_interpolation(root, log_message)
    fix_xna_interpolation(root, log_message)
    fix_xna_skinned_model_effect_pool(root, log_message)
    fix_xna_depth_stencil_restore(root, log_message)


def repair_cron_bloom(root):
    bloom = root / "CRON" / "BloomComponent.cs"
    puzzle = root / "CRON" / "Puzzle.cs"
    if not bloom.is_file() or not puzzle.is_file():
        print("Skipped CRON bloom repair: CRON source files were not found.")
        return

    bloom_text = bloom.read_text(encoding="utf-8-sig")
    puzzle_text = puzzle.read_text(encoding="utf-8-sig")
    if "public void BeginScene()" in bloom_text and "bloomComponent.BeginScene();" in puzzle_text:
        print("CRON bloom repair already applied.")
        return
    if "namespace CRON;" not in bloom_text or "namespace CRON;" not in puzzle_text:
        raise ValueError("CRON bloom repair requires the decompiled CRON source layout.")

    # Normalize only these two files with the existing conversion pass.
    with tempfile.TemporaryDirectory(prefix="cron-bloom-") as directory:
        staging = Path(directory)
        for path in (bloom, puzzle):
            shutil.copyfile(path, staging / path.name)
        repair_sources(staging, lambda *_: None)

        patches = {'BloomComponent.cs': [('\tpublic override void Draw(GameTime gameTime)\n'
                                '\t{\n'
                                '\t\t// TODO: Render scene into resolveTarget before bloom processing.\n'
                                '\t\t// ResolveBackBuffer removed.',
                                '\tpublic void BeginScene()\n'
                                '\t{\n'
                                '\t\tGraphicsDevice.SetRenderTarget(resolveTarget);\n'
                                '\t\tGraphicsDevice.Clear(Color.Black);\n'
                                '\t}\n'
                                '\n'
                                '\tpublic override void Draw(GameTime gameTime)\n'
                                '\t{\n'
                                '\t\tGraphicsDevice.SetRenderTarget(null);'),
                               ('\t\tDrawFullscreenQuad(renderTarget1, viewport.Width, viewport.Height, '
                                'bloomCombineEffect, IntermediateBuffer.FinalResult);',
                                '\t\tDrawFullscreenQuad(renderTarget1, viewport.Width, viewport.Height, '
                                'bloomCombineEffect, IntermediateBuffer.FinalResult);\n'
                                '\t\tGraphicsDevice.Textures[1] = null;'),
                               ('\t\tspriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);\n'
                                '\t\tif (showBuffer >= currentBuffer)\n'
                                '\t\t{\n'
                                '\t\t\teffect.CurrentTechnique.Passes[0].Apply();\n'
                                '\t\t}\n'
                                '\t\tspriteBatch.Draw(texture, new Rectangle(0, 0, width, height), '
                                'Color.White);\n'
                                '\t\tspriteBatch.End();\n'
                                '\t\tif (showBuffer >= currentBuffer)\n'
                                '\t\t{\n'
                                '\t\t\t\n'
                                '\t\t}',
                                '\t\tEffect activeEffect = showBuffer >= currentBuffer ? effect : null;\n'
                                '\t\tspriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, '
                                'null, null, activeEffect);\n'
                                '\t\tspriteBatch.Draw(texture, new Rectangle(0, 0, width, height), '
                                'Color.White);\n'
                                '\t\tspriteBatch.End();')],
         'Puzzle.cs': [('new BloomComponent(ScreenManager.Game);',
                        'new BloomComponent(ScreenManager.Game) { Visible = false };'),
                       ('\t\tbloomComponent.Visible = false;',
                        '\t\tScreenManager.Game.Components.Remove(bloomComponent);\n'
                        '\t\tbloomComponent.Dispose();'),
                       ('\t\tbloomComponent.Draw(gameTime);', '\t\tbloomComponent.BeginScene();'),
                       ('spriteBatch.Begin(BlendState.AlphaBlend, SpriteSortMode.BackToFront, '
                        'SaveStateMode.None, transformMatrix);',
                        'spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, '
                        'null, null, transformMatrix);'),
                       ('\t\tspriteBatch.End();\n\t\tif (TransitionPosition',
                        '\t\tspriteBatch.End();\n'
                        '\t\tbloomComponent.Draw(gameTime);\n'
                        '\t\tif (TransitionPosition')]}
        updated = {}
        for path in (bloom, puzzle):
            text = (staging / path.name).read_text(encoding="utf-8-sig")
            for before, after in patches[path.name]:
                if text.count(before) != 1:
                    raise ValueError(f"CRON bloom repair could not match the expected code in {path.name}.")
                text = text.replace(before, after)
            updated[path] = text

    for path, text in updated.items():
        path.write_text(text, encoding="utf-8")


def repair_other_scene_capture(root):
    avatar = {
        "BloomComponent.cs": ("dBase.game.GraphicsDevice", "resolveTarget"),
        "DepthOfField.cs": ("device", "resolveTarget"),
        "GaussianBlur.cs": ("game.GraphicsDevice", "backTex"),
    }
    jet_pack = {
        "BloomPostprocess/BloomComponent.cs": ("((DrawableGameComponent)this).GraphicsDevice", "resolveTarget"),
        "TerrainDemo/SimpleTerrain.cs": ("device", "reflectionMap"),
    }
    for layout in (avatar, jet_pack):
        if not all((root / name).is_file() for name in layout):
            continue
        updated = {}
        for name, (device, target) in layout.items():
            path = root / name
            text = path.read_text(encoding="utf-8-sig")
            old = f"{device}.ResolveBackBuffer({target});"
            new = f"global::BuildManagerBackBufferCapture.Copy({device}, {target});"
            if old not in text:
                if new in text:
                    continue
                break
            text = text.replace(old, new)
            if layout is jet_pack:
                text = re.sub(rf"\b(?:ResolveTexture2D|RenderTarget2D) {target};", f"Texture2D {target};", text)
                if target == "resolveTarget":
                    text = re.sub(
                        r"resolveTarget = new (?:ResolveTexture2D|RenderTarget2D)\(.*?, backBufferWidth, backBufferHeight, 1, backBufferFormat\);",
                        f"resolveTarget = new Texture2D({device}, backBufferWidth, backBufferHeight, false, backBufferFormat);",
                        text,
                    )
                    text = re.sub(
                        r"new RenderTarget2D\(.*?, backBufferWidth, backBufferHeight, 1, backBufferFormat, .*?MultiSampleQuality\)",
                        f"new RenderTarget2D({device}, backBufferWidth, backBufferHeight, false, backBufferFormat, DepthFormat.None)",
                        text,
                    )
                    text = text.replace("SetRenderTarget(0, (RenderTarget2D)null)", "SetRenderTarget(null)")
                    text = text.replace("SetRenderTarget(0, renderTarget)", "SetRenderTarget(renderTarget)")
                    text = text.replace(".GetTexture()", "")
                    text = re.sub(r"\(\(RenderTarget\)(renderTarget\w*)\)", r"\1", text)
                    text = re.sub(
                        r"(private void DrawFullscreenQuad\(Texture2D texture, int width, int height, "
                        r"Effect effect, IntermediateBuffer currentBuffer\)\s*\{)"
                        r".*?\n\t\}",
                        lambda match: match[1] + """
		Effect activeEffect = showBuffer >= currentBuffer ? effect : null;
		spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, null, activeEffect);
		spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
		spriteBatch.End();
	}""",
                        text,
                        flags=re.DOTALL,
                    )
                else:
                    text = re.sub(
                        r"reflectionMap = new (?:ResolveTexture2D|RenderTarget2D)\(device, width, viewport.Height, (?:0|false), \(SurfaceFormat\)1(?:, DepthFormat.None)?\);",
                        "reflectionMap = new Texture2D(device, width, viewport.Height, false, device.PresentationParameters.BackBufferFormat);",
                        text,
                    )
            updated[path] = text
        else:
            helper = root / "BuildManagerBackBufferCapture.cs"
            if helper.exists() and helper.read_text(encoding="utf-8") != BACK_BUFFER_CAPTURE:
                raise ValueError("BuildManagerBackBufferCapture.cs already contains different code.")
            if not updated and helper.is_file():
                print("Back-buffer capture repair already applied.")
                return True
            helper.write_text(BACK_BUFFER_CAPTURE, encoding="utf-8")
            for path, text in updated.items():
                path.write_text(text, encoding="utf-8")
            print("Back-buffer capture uses FNA readback at the original capture points.")
            return True
    return False


BACK_BUFFER_CAPTURE = """using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal static class BuildManagerBackBufferCapture
{
    private static Color[] pixels = Array.Empty<Color>();

    internal static void Copy(GraphicsDevice device, Texture2D destination)
    {
        int width = destination.Width;
        int height = destination.Height;
        var presentation = device.PresentationParameters;
        if (width > presentation.BackBufferWidth || height > presentation.BackBufferHeight ||
            presentation.BackBufferFormat != SurfaceFormat.Color || destination.Format != SurfaceFormat.Color)
        {
            throw new InvalidOperationException("Back-buffer capture requires a compatible Color texture.");
        }

        int count = checked(width * height);
        if (pixels.Length != count)
            pixels = new Color[count];

        device.GetBackBufferData(new Rectangle(0, 0, width, height), pixels, 0, count);
        destination.SetData(pixels);
    }
}
"""


def main():
    parser = argparse.ArgumentParser(description="Repair XNA 3.x source for FNA.")
    parser.add_argument("root", type=Path)
    parser.add_argument("--preview", action="store_true")
    parser.add_argument("--cron-bloom", action="store_true")
    parser.add_argument("--convert-xna3", action="store_true")
    args = parser.parse_args()
    root = args.root.resolve()
    if not root.is_dir():
        parser.error(f"Game folder does not exist: {root}")

    with tempfile.TemporaryDirectory(prefix="xna3-repair-") as directory:
        staging = Path(directory)
        files = []
        for folder, subfolders, filenames in os.walk(root):
            subfolders[:] = [name for name in subfolders if name.lower() not in
                             {"bin", "obj", ".git", ".vs"}]
            for name in filenames:
                source = Path(folder) / name
                if source.suffix.lower() != ".cs" or source.is_symlink():
                    continue
                staged = staging / source.relative_to(root)
                staged.parent.mkdir(parents=True, exist_ok=True)
                shutil.copyfile(source, staged)
                files.append((source, staged))

        if args.convert_xna3 or not args.cron_bloom:
            repair_sources(staging, lambda *_: None)
        if args.cron_bloom:
            if not repair_other_scene_capture(staging):
                repair_cron_bloom(staging)
            helper = staging / "BuildManagerBackBufferCapture.cs"
            destination = root / helper.name
            if helper.is_file() and not destination.exists():
                files.insert(0, (destination, helper))
        changed = 0
        for source, staged in files:
            original = source.read_bytes() if source.exists() else None
            repaired = staged.read_bytes()
            if original == repaired:
                continue
            if not args.preview:
                shutil.copyfile(staged, source)
            changed += 1
            print(f"{'Would repair' if args.preview else 'Repaired'}: {source.relative_to(root)}")
            if b"// ResolveBackBuffer removed." in repaired and b"// ResolveBackBuffer removed." not in (original or b""):
                print("Bloom needs a scene render target to replace ResolveBackBuffer.")
        print(f"Source repair: {changed} file(s) {'would change' if args.preview else 'changed'}.")


if __name__ == "__main__":
    main()
