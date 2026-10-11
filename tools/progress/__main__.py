"""CLI del backlog.

  python3 -m tools.progress                    regenera PROGRESO.md y fases/*.md
  python3 -m tools.progress --check            falla si los ficheros generados no están al día
  python3 -m tools.progress --start F1-01      marca la tarea en curso
  python3 -m tools.progress --done F1-01 --pr '#12' --session 3
  python3 -m tools.progress --next             imprime la siguiente tarea (cola de prioridad del tablero)
"""
import argparse
import sys

from . import model, render


def write_outputs(backlog: dict, backlog_dir=None) -> None:
    for rel, content in render.outputs(backlog).items():
        ((backlog_dir or model.BACKLOG_DIR) / rel).write_text(content, encoding="utf-8")


def stale_outputs(backlog: dict) -> list:
    stale = []
    for rel, content in render.outputs(backlog).items():
        path = model.BACKLOG_DIR / rel
        if not path.exists() or path.read_text(encoding="utf-8") != content:
            stale.append(rel)
    return stale


def main(argv=None) -> int:
    ap = argparse.ArgumentParser(prog="progress")
    ap.add_argument("--check", action="store_true")
    ap.add_argument("--next", action="store_true")
    ap.add_argument("--start")
    ap.add_argument("--done")
    ap.add_argument("--pr")
    ap.add_argument("--session", type=int)
    args = ap.parse_args(argv)

    backlog = model.load()
    if args.check:
        stale = stale_outputs(backlog)
        if stale:
            print("Desactualizados (ejecuta python3 -m tools.progress):", ", ".join(stale))
        return 1 if stale else 0
    if args.next:
        print(model.summarize(backlog)["next_task"] or "")
        return 0
    if args.start or args.done:
        tid = args.start or args.done
        status = "in_progress" if args.start else "done"
        task = model.set_status(backlog, tid, status, args.pr, args.session)
        model.validate(backlog)
        model.save_task(task)
    write_outputs(backlog)
    s = model.summarize(backlog)
    print(f"{render.bar(s['pct'])} {s['pct']}% ({s['done']}/{s['total']})")
    return 0


if __name__ == "__main__":
    sys.exit(main())
