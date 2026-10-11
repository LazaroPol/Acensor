"""CLI del índice de arquitectura.

  python3 -m tools.arch           regenera docs/arquitectura/MODULOS.md, GRAFO.md y modulos/*.md
  python3 -m tools.arch --check   falla si lo generado no está al día
"""
import argparse

from . import render, scan

OUT_DIR = scan.ROOT / "docs/arquitectura"


def stale_outputs(files: dict) -> list:
    stale = [rel for rel, content in files.items()
             if not (OUT_DIR / rel).exists() or (OUT_DIR / rel).read_text(encoding="utf-8") != content]
    modulos = OUT_DIR / "modulos"
    extra = [f"modulos/{p.name}" for p in modulos.glob("*.md") if f"modulos/{p.name}" not in files]
    return stale + extra


def main(argv=None) -> int:
    ap = argparse.ArgumentParser(prog="arch")
    ap.add_argument("--check", action="store_true")
    args = ap.parse_args(argv)
    files = render.outputs(scan.scan_all())
    if args.check:
        stale = stale_outputs(files)
        if stale:
            print("Desactualizados (ejecuta python3 -m tools.arch):", ", ".join(stale))
        return 1 if stale else 0
    (OUT_DIR / "modulos").mkdir(parents=True, exist_ok=True)
    for old in stale_outputs({}):
        (OUT_DIR / old).unlink()
    for rel, content in files.items():
        (OUT_DIR / rel).write_text(content, encoding="utf-8", newline="\n")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
