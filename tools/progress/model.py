"""Modelo del backlog: una tarjeta JSON por tarea, fases como metadatos y una cola de prioridad.

docs/backlog/proyecto.json      orden de las fases
docs/backlog/fases/<F>.json     id, nombre y objetivo de la fase
docs/backlog/tareas/<ID>.json   una tarjeta por tarea (la edita el tablero o la sesión)
docs/backlog/prioridad.json     {"orden": [...]} — la posición manda (la ordena el tablero)
"""
import json
import pathlib

from . import rules  # noqa: F401
from .rules import (COUNTED, VALID_STATUS, next_task, percent,  # noqa: F401 (API pública)
                    task_key, validate)

ROOT = pathlib.Path(__file__).resolve().parents[2]
BACKLOG_DIR = ROOT / "docs/backlog"


def _read(path: pathlib.Path):
    return json.loads(path.read_text(encoding="utf-8"))


def _write(path: pathlib.Path, data) -> None:
    path.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def load(backlog_dir: pathlib.Path = BACKLOG_DIR) -> dict:
    project = _read(backlog_dir / "proyecto.json")
    phases = [dict(_read(backlog_dir / "fases" / f"{pid}.json"), tasks=[]) for pid in project["phases"]]
    by_id = {p["id"]: p for p in phases}
    for path in (backlog_dir / "tareas").glob("*.json"):
        task = _read(path)
        by_id[task["phase"]]["tasks"].append(task)
    for p in phases:
        p["tasks"].sort(key=task_key)
    prio = backlog_dir / "prioridad.json"
    priority = _read(prio)["orden"] if prio.exists() else []
    return {"project": project["project"], "engine": project["engine"], "phases": phases, "priority": priority}


def save_task(task: dict, backlog_dir: pathlib.Path = BACKLOG_DIR) -> None:
    _write(backlog_dir / "tareas" / f"{task['id']}.json", task)


def save_priority(order: list, backlog_dir: pathlib.Path = BACKLOG_DIR) -> None:
    _write(backlog_dir / "prioridad.json", {"orden": order})


def all_tasks(backlog: dict) -> list:
    return [t for p in backlog["phases"] for t in p["tasks"]]


def summarize(backlog: dict) -> dict:
    validate(backlog)
    phases = []
    for p in backlog["phases"]:
        counted = [t for t in p["tasks"] if t["status"] in COUNTED]
        done = sum(t["status"] == "done" for t in counted)
        phases.append({"id": p["id"], "name": p["name"], "done": done,
                       "total": len(counted), "pct": percent(done, len(counted))})
    done = sum(p["done"] for p in phases)
    total = sum(p["total"] for p in phases)
    current = next((p["id"] for p in phases if p["done"] < p["total"]), None)
    return {"done": done, "total": total, "pct": percent(done, total), "phases": phases,
            "current_phase": current, "next_task": next_task(backlog)}


def find_task(backlog: dict, task_id: str):
    for phase in backlog["phases"]:
        for task in phase["tasks"]:
            if task["id"] == task_id:
                return phase, task
    raise KeyError(task_id)


def set_status(backlog: dict, task_id: str, status: str, pr: str = None, session: int = None) -> dict:
    """Cambia el estado de una tarea y devuelve la tarjeta (para guardarla con save_task)."""
    if status not in VALID_STATUS:
        raise ValueError(status)
    _, task = find_task(backlog, task_id)
    task["status"] = status
    if pr is not None:
        task["pr"] = pr
    if session is not None:
        task["session"] = session
    return task


def last_session(backlog: dict) -> int:
    return max((t.get("session") or 0 for t in all_tasks(backlog)), default=0)
