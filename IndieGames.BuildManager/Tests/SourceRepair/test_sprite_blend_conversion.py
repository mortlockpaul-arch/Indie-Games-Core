import ast
import re
import tempfile
import unittest
from pathlib import Path

SOURCE = Path(__file__).resolve().parents[2] / 'scripts' / 'repair_xna3.py'
NAMES = {'_xna_sprite_code_mask', 'fix_xna_spritebatch_begin', 'fix_xna_sprite_blend_mode'}
module = ast.parse(SOURCE.read_text(encoding='utf-8-sig'))
scope = {'Path': Path, 're': re}
exec(compile(ast.Module(body=[node for node in module.body if isinstance(node, ast.FunctionDef) and node.name in NAMES], type_ignores=[]), str(SOURCE), 'exec'), scope)


class SpriteBlendConversionTests(unittest.TestCase):
    def convert(self, source):
        with tempfile.TemporaryDirectory() as folder:
            path = Path(folder) / 'Game.cs'
            path.write_text(source, encoding='utf-8')
            scope['fix_xna_spritebatch_begin'](folder, lambda *args: None)
            scope['fix_xna_sprite_blend_mode'](folder, lambda *args: None)
            result = path.read_text(encoding='utf-8')
            scope['fix_xna_sprite_blend_mode'](folder, lambda *args: None)
            self.assertEqual(result, path.read_text(encoding='utf-8'), 'Conversion must be idempotent')
            return result

    def test_single_argument_constants_and_numeric_values(self):
        for legacy, modern in [('AlphaBlend', 'NonPremultiplied'), ('Additive', 'Additive'), ('None', 'Opaque')]:
            self.assertEqual(self.convert(f'b.Begin(SpriteBlendMode.{legacy});'),
                             f'b.Begin(SpriteSortMode.Deferred, BlendState.{modern});')
        self.assertEqual(self.convert('b.Begin((SpriteBlendMode)2, (SpriteSortMode)0, (SaveStateMode)0);'),
                         'b.Begin(SpriteSortMode.Immediate, BlendState.Additive);')

    def test_field_declaration_and_variable_call(self):
        result = self.convert('protected SpriteBlendMode blend; void Init() { blend = SpriteBlendMode.AlphaBlend; b.Begin(blend); }')
        self.assertIn('protected BlendState blend;', result)
        self.assertIn('blend = BlendState.NonPremultiplied;', result)
        self.assertIn('b.Begin(SpriteSortMode.Deferred, blend);', result)

    def test_sort_and_nested_transform_are_preserved(self):
        self.assertEqual(self.convert('b.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState, Matrix.CreateTranslation(new Vector3(1, 2, 3)));'),
                         'b.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, null, null, null, null, Matrix.CreateTranslation(new Vector3(1, 2, 3)));')

    def test_earlier_partial_conversion_is_repaired(self):
        self.assertEqual(self.convert('b.Begin(BlendState.AlphaBlend, SpriteSortMode.Texture);'),
                         'b.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied);')

    def test_named_and_positional_effects(self):
        for call in ['b.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, effect: customEffect);',
                     'b.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.None, customEffect);']:
            self.assertEqual(self.convert(call), 'b.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, null, customEffect);')

    def test_modern_calls_comments_strings_and_effect_calls_are_unchanged(self):
        source = 'b.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); effect.Begin(); // b.Begin(SpriteBlendMode.None);\nstring s = "SpriteBlendMode.AlphaBlend";'
        self.assertEqual(self.convert(source), source)

    def test_generated_directories_are_not_modified(self):
        with tempfile.TemporaryDirectory() as folder:
            for directory in ('bin', 'obj', '.git'):
                path = Path(folder) / directory / 'Game.cs'
                path.parent.mkdir()
                path.write_text('b.Begin(SpriteBlendMode.None);')
            scope['fix_xna_sprite_blend_mode'](folder, lambda *args: None)
            for path in Path(folder).rglob('*.cs'):
                self.assertEqual(path.read_text(), 'b.Begin(SpriteBlendMode.None);')


if __name__ == '__main__':
    unittest.main()
