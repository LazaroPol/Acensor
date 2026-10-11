"""Cierra la sesión de una tarea: la marca hecha y añade la fila al historial.

  python3 -m tools.session.close F1-01 7 3   (tarea, número de PR, número de sesión)
El resumen docs/sesiones/SESION-SS.md se escribe a mano (lo exige un test).
"""
import datetime
import sys

from tools.progress import model
from tools.progress.__main__ import write_outputs

INDEX = model.ROOT / "docs/sesiones/README.md"
REPO_URL = "https://github.com/LazaroPol/Acensor"


def history_row(session: int, task_id: str, pr: int, done: int, total: int, date: str) -> str:
    pct = f"{model.percent(done, total):.1f}".replace(".", ",")
    return (f"| {session} | {task_id} | {date} | [#{pr}]({REPO_URL}/pull/{pr}) | "
            f"{done}/{total} · {pct} % | [SESION-{session:02d}](SESION-{session:02d}.md) |")


def close(task_id: str, pr: int, session: int, date: str = None) -> str:
    backlog = model.load()
    phase = model.set_status(backlog, task_id, "done", f"#{pr}", session)
    model.validate(backlog)
    model.save_phase(phase)
    write_outputs(backlog)
    s = model.summarize(backlog)
    row = history_row(session, task_id, pr, s["done"], s["total"], date or datetime.date.today().isoformat())
    text = INDEX.read_text(encoding="utf-8").rstrip("\n") + "\n" + row + "\n"
    INDEX.write_text(text, encoding="utf-8")
    return row


if __name__ == "__main__":
    print(close(sys.argv[1], int(sys.argv[2]), int(sys.argv[3])))
