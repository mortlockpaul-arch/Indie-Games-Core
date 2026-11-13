"""Safe launcher for individual XNA 3 source repairs, with preview and backups."""
import argparse
import importlib.util
import json
import os
import shutil
import tempfile
import zipfile
from datetime import datetime
from pathlib import Path

HERE = Path(__file__).resolve().parent
MANIFEST = json.loads((HERE / 'repair_manifest.json').read_text(encoding='utf-8'))

def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('root', type=Path, nargs='?')
    parser.add_argument('--list', action='store_true')
    parser.add_argument('--fix', action='append', choices=MANIFEST['individual_repairs'])
    parser.add_argument('--all', action='store_true', help='Run the original XNA3 preset')
    parser.add_argument('--apply', action='store_true', help='Write changes; preview is default')
    args = parser.parse_args()
    if args.list:
        print('\n'.join(MANIFEST['individual_repairs']))
        return 0
    if not args.root or not args.root.is_dir():
        parser.error('Supply an existing game root folder')
    if not args.fix and not args.all:
        parser.error('Select --fix NAME (repeatable) or --all')
    root = args.root.resolve()
    spec = importlib.util.spec_from_file_location('xna3_repairs', HERE / 'repair_xna3.py')
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    with tempfile.TemporaryDirectory(prefix='xna3-select-') as temp:
        stage = Path(temp)
        sources = []
        for folder, dirs, files in os.walk(root):
            dirs[:] = [d for d in dirs if d.lower() not in {'bin','obj','.git','.vs','.backups'}]
            for name in files:
                src = Path(folder) / name
                if src.suffix.lower() != '.cs' or src.is_symlink():
                    continue
                dest = stage / src.relative_to(root)
                dest.parent.mkdir(parents=True, exist_ok=True)
                shutil.copyfile(src,dest)
                sources.append((src,dest))
        def log(*values):
            print(str(values[0]) if values else '')
        if args.all:
            module.repair_sources(stage,log)
        else:
            for name in dict.fromkeys(args.fix):
                print('Running:',name)
                func = getattr(module,name)
                if name in {'fix_xna_renderstate','fix_xna_vertex_elements','fix_xna_index_buffer','fix_xna_vertex_streams'}:
                    func(stage,log,framework='fna')
                else:
                    func(stage,log)
        changes = [(src,dst) for src,dst in sources if src.read_bytes()!=dst.read_bytes()]
        print(f'{len(changes)} source file(s) {"to modify" if not args.apply else "modified"}')
        for src,_ in changes:
            print(('CHANGE ' if args.apply else 'PREVIEW ')+str(src.relative_to(root)))
        if args.apply and changes:
            backups = root / '.backups'
            backups.mkdir(exist_ok=True)
            archive = backups / ('xna3-selected-'+datetime.now().strftime('%Y%m%d-%H%M%S-%f')+'.zip')
            with zipfile.ZipFile(archive,'w',compression=zipfile.ZIP_DEFLATED) as z:
                for src,_ in changes:
                    z.write(src,src.relative_to(root).as_posix())
            print('Backup:',archive)
            for src,dst in changes:
                shutil.copyfile(dst,src)
    return 0

if __name__=='__main__':
    raise SystemExit(main())
