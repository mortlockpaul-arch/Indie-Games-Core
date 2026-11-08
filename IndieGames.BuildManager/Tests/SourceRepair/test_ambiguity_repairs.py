import importlib.util
import tempfile
import unittest
from pathlib import Path

SCRIPT = Path(__file__).resolve().parents[2] / 'scripts' / 'repair_ambiguities.py'
spec = importlib.util.spec_from_file_location('repair_ambiguities', SCRIPT)
repair = importlib.util.module_from_spec(spec)
spec.loader.exec_module(repair)

class AmbiguityRepairTests(unittest.TestCase):
    def test_precise_color_and_physics_path_repairs_preserve_other_code_and_backup(self):
        with tempfile.TemporaryDirectory() as folder:
            path = Path(folder) / 'Game.cs'
            source = 'Color c; Path p; string f = Path.Combine("a", "b"); // Color is a comment\r\n'
            data = b'\xef\xbb\xbf' + source.encode()
            path.write_bytes(data)
            pairs = [('Color', 'Microsoft.Xna.Framework.Color', 'System.Drawing.Color'), ('Path', 'FarseerGames.FarseerPhysics.Dynamics.Path', 'System.IO.Path')]
            log = ''
            for name, first, second in pairs:
                start = 0
                while True:
                    pos = source.find(name, start)
                    if pos < 0 or pos > source.index('//'): break
                    log += f"{path}(1,{pos+1}): error CS0104: '{name}' is an ambiguous reference between '{first}' and '{second}' [Game.csproj]\n"
                    start = pos + len(name)
            snapshots, edits, skipped = repair.plan_repairs(log + log, folder)
            self.assertEqual(sum(map(len, edits.values())), 3)
            self.assertEqual(skipped, [])
            self.assertEqual(path.read_bytes(), data)
            backup = repair.apply_repairs(snapshots, edits, folder)
            result = path.read_bytes()
            self.assertTrue(result.startswith(b'\xef\xbb\xbf'))
            self.assertIn(b'global::Microsoft.Xna.Framework.Color c', result)
            self.assertIn(b'global::FarseerGames.FarseerPhysics.Dynamics.Path p', result)
            self.assertIn(b'global::System.IO.Path.Combine', result)
            self.assertIn(b'// Color is a comment\r\n', result)
            self.assertEqual((backup / 'Game.cs').read_bytes(), data)

    def test_utf16_columns_and_stale_source(self):
        with tempfile.TemporaryDirectory() as folder:
            path = Path(folder) / 'Game.cs'
            source = '/* 🐈 */ Color color;'
            path.write_text(source, encoding='utf-16')
            column = len(source[:source.index('Color')].encode('utf-16-le')) // 2 + 1
            log = f"{path}(1,{column}): error CS0104: 'Color' is an ambiguous reference between 'System.Drawing.Color' and 'Microsoft.Xna.Framework.Color'"
            snapshots, edits, skipped = repair.plan_repairs(log, folder)
            self.assertEqual(sum(map(len, edits.values())), 1)
            path.write_text(source + ' // changed', encoding='utf-16')
            with self.assertRaises(RuntimeError): repair.apply_repairs(snapshots, edits, folder)

    def test_overloads_and_unknown_names_are_review_only(self):
        with tempfile.TemporaryDirectory() as folder:
            path = Path(folder) / 'Game.cs'; path.write_text('Timer t;')
            log = f"{path}(1,1): error CS0104: 'Timer' is an ambiguous reference between 'A.Timer' and 'B.Timer'\n{path}(1,1): error CS0121: The call is ambiguous"
            _, edits, skipped = repair.plan_repairs(log, folder)
            self.assertEqual(edits, {})
            self.assertEqual(len(skipped), 2)

if __name__ == '__main__': unittest.main()
