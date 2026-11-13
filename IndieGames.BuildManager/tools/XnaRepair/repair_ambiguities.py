"""Repair compiler-reported XNA Color and Farseer Path name ambiguities.

Preview is the default. Only --apply writes source; each changed file is backed
up. Other ambiguous names and overloads are reported without guessing a type.
"""
import argparse
import codecs
import json
import os
import re
import subprocess
import xml.etree.ElementTree as ET
from datetime import datetime
from pathlib import Path

DIAGNOSTIC = re.compile(r'^(?P<file>.+\.cs)\((?P<line>\d+),(?P<column>\d+)\):\s*error\s+(?P<code>CS0104|CS0121):\s*(?P<message>.*?)(?:\s+\[[^\r\n]*\])?$', re.MULTILINE)
AMBIGUOUS_TYPE = re.compile(r"'([^']+)' is an ambiguous reference between '([^']+)' and '([^']+)'")
IO_MEMBERS = {'Combine', 'Join', 'TryJoin', 'GetFullPath', 'GetFileName', 'GetFileNameWithoutExtension', 'GetDirectoryName', 'GetExtension', 'GetPathRoot', 'GetRandomFileName', 'GetTempFileName', 'GetTempPath', 'HasExtension', 'IsPathRooted', 'IsPathFullyQualified', 'ChangeExtension', 'GetRelativePath', 'GetInvalidPathChars', 'GetInvalidFileNameChars', 'EndsInDirectorySeparator', 'TrimEndingDirectorySeparator', 'DirectorySeparatorChar', 'AltDirectorySeparatorChar', 'PathSeparator', 'VolumeSeparatorChar'}


def diagnostics(log):
    seen = set()
    for match in DIAGNOSTIC.finditer(log):
        item = match.groupdict()
        identity = tuple(item.values())
        if identity not in seen:
            seen.add(identity)
            yield item


def decode_source(data):
    for bom, encoding in ((codecs.BOM_UTF8, 'utf-8'), (codecs.BOM_UTF16_LE, 'utf-16-le'), (codecs.BOM_UTF16_BE, 'utf-16-be')):
        if data.startswith(bom):
            return data[len(bom):].decode(encoding), encoding, bom
    return data.decode('utf-8'), 'utf-8', b''


def plan_repairs(log, root, color_type='Microsoft.Xna.Framework.Color'):
    root = Path(root).resolve()
    snapshots = {}
    edits = {}
    skipped = []
    for item in diagnostics(log):
        path = Path(item['file']).resolve()
        if not path.is_relative_to(root) or {'bin', 'obj', '.git', '.backups'}.intersection(path.relative_to(root).parts):
            skipped.append({**item, 'reason': 'Outside selected source tree'})
            continue
        match = AMBIGUOUS_TYPE.search(item['message']) if item['code'] == 'CS0104' else None
        if not match:
            skipped.append({**item, 'reason': 'Overload ambiguity requires a type-specific source repair'})
            continue
        name, left, right = match.groups()
        candidates = {left, right}
        try:
            if path not in snapshots:
                data = path.read_bytes()
                text, encoding, bom = decode_source(data)
                snapshots[path] = (data, text, encoding, bom)
            _, text, _, _ = snapshots[path]
            lines = text.splitlines(keepends=True)
            line_index = int(item['line']) - 1
            line = lines[line_index]
            # Roslyn columns count UTF-16 code units, not Python characters.
            prefix = line.encode('utf-16-le')[:(int(item['column']) - 1) * 2].decode('utf-16-le')
            offset = sum(map(len, lines[:line_index])) + len(prefix)
            if text[offset:offset + len(name)] != name or re.match(r'\w', text[offset + len(name):offset + len(name) + 1]):
                raise ValueError('Diagnostic position no longer matches the source')
            target = None
            if name == 'Color' and candidates == {'Microsoft.Xna.Framework.Color', 'System.Drawing.Color'}:
                target = color_type
            elif name == 'Path' and candidates == {'System.IO.Path', 'FarseerGames.FarseerPhysics.Dynamics.Path'}:
                member = re.match(r'\s*\.\s*(\w+)', text[offset + len(name):])
                target = 'System.IO.Path' if member and member[1] in IO_MEMBERS else 'FarseerGames.FarseerPhysics.Dynamics.Path'
            if target is None:
                skipped.append({**item, 'reason': 'No supported unambiguous repair rule'})
                continue
            edits.setdefault(path, {})[offset] = (name, 'global::' + target)
        except (OSError, UnicodeError, IndexError, ValueError) as error:
            skipped.append({**item, 'reason': str(error)})
    return snapshots, edits, skipped


def apply_repairs(snapshots, edits, root):
    root = Path(root).resolve()
    # Validate every snapshot before making the first change.
    for path in edits:
        if path.read_bytes() != snapshots[path][0]:
            raise RuntimeError(f'Source changed during repair: {path}')
    backup = root / '.backups' / ('ambiguities-' + datetime.now().strftime('%Y%m%d-%H%M%S-%f'))
    for path, replacements in edits.items():
        original, text, encoding, bom = snapshots[path]
        destination = backup / path.relative_to(root)
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_bytes(original)
        for offset, (name, replacement) in sorted(replacements.items(), reverse=True):
            text = text[:offset] + replacement + text[offset + len(name):]
        path.write_bytes(bom + text.encode(encoding))
    return backup if edits else None


def find_project(root):
    candidates = list(Path(root).glob('*.csproj'))
    if len(candidates) == 1:
        return candidates[0]
    executables = []
    for project in candidates:
        xml = ET.parse(project)
        if any(e.tag.rsplit('}', 1)[-1] == 'OutputType' and (e.text or '').strip() in {'Exe', 'WinExe'} for e in xml.iter()):
            executables.append(project)
    if len(executables) != 1:
        raise ValueError('Select an explicit --project: the folder has several or no executable projects.')
    return executables[0]


def build(project, platform, configuration):
    environment = dict(os.environ, DOTNET_CLI_UI_LANGUAGE='en', VSLANG='1033')
    result = subprocess.run(['dotnet', 'build', str(project), '-c', configuration, '-p:Platform=' + platform,
                             '-p:GenerateFullPaths=true', '-v:q', '--nologo'], cwd=project.parent,
                            env=environment, capture_output=True, text=True, encoding='utf-8', errors='replace')
    return result.returncode, result.stdout + result.stderr


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    selection = parser.add_mutually_exclusive_group(required=True)
    selection.add_argument('--project', type=Path)
    selection.add_argument('--root', type=Path)
    parser.add_argument('--platform', choices=['x86', 'x64', 'AnyCPU'], default='x86')
    parser.add_argument('--configuration', default='Debug')
    parser.add_argument('--color-type', choices=['Microsoft.Xna.Framework.Color', 'System.Drawing.Color'], default='Microsoft.Xna.Framework.Color', help='Intended Color type; game source defaults to XNA Color.')
    parser.add_argument('--apply', action='store_true')
    parser.add_argument('--report', type=Path)
    args = parser.parse_args()
    project = (args.project or find_project(args.root)).resolve()
    code, log = build(project, args.platform, args.configuration)
    found = list(diagnostics(log))
    snapshots, edits, skipped = plan_repairs(log, project.parent, args.color_type)
    for path, replacements in edits.items():
        for offset, (name, replacement) in replacements.items():
            print(f'{"Repair" if args.apply else "Preview"}: {path}: {name} -> {replacement}')
    backups = []
    count = sum(len(items) for items in edits.values())
    if args.apply:
        # C# can reveal further ambiguous expressions after their declaration
        # types are repaired. Always obtain fresh positions before the next pass.
        for iteration in range(8):
            if not edits:
                break
            backup = apply_repairs(snapshots, edits, project.parent)
            backups.append(str(backup))
            print('Source backups:', backup)
            code, log = build(project, args.platform, args.configuration)
            snapshots, edits, skipped = plan_repairs(log, project.parent, args.color_type)
            if not edits:
                break
            if iteration < 7:
                count += sum(len(items) for items in edits.values())
                print('Further compiler-reported occurrences revealed; applying another pass.')
        if edits:
            print('Additional supported diagnostics remain; rerun the repair after reviewing the report.')
    remaining = list(diagnostics(log))
    backup = backups[0] if backups else None
    for item in skipped:
        print(f"Review: {item['file']}({item['line']}): {item['code']}: {item['message']} [{item['reason']}]")
    print(f'{count} supported occurrence(s); {len(skipped)} diagnostic(s) require review; build exit code: {code}.')
    if args.apply:
        print(f'{len(remaining)} ambiguous diagnostic(s) remain after repair.')
    if args.report:
        args.report.parent.mkdir(parents=True, exist_ok=True)
        args.report.write_text(json.dumps({'project': str(project), 'applied': args.apply, 'planned_occurrences': count,
                              'backup': str(backup) if backup else None, 'backups': backups, 'build_exit_code': code,
                              'review': skipped, 'remaining_ambiguities': remaining}, indent=2), encoding='utf-8')
    # Unrelated source errors are not a failure of the preview; applied builds
    # still propagate failure so the UI never reports a broken game as successful.
    return code if args.apply else 0


if __name__ == '__main__':
    raise SystemExit(main())
