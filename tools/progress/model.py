"""Modelo del backlog: carga, validación, resumen y actualización de tareas."""
import json
import pathlib

ROOT = pathlib.Path(__file__).resolve().parents[2]
BACKLOG_DIR = ROOT / "docs/backlog"
VALID_STATUS = {"todo", "in_progress", "done"}


def load(backlog_dir: pathlib.Path = BACKLOG_DIR) -> dict:
    project = json.loads((backlog_dir / "proyecto.json").read_text(encoding="utf-8"))
    phases = [json.loads((backlog_dir / "fases" / f"{pid}.json").read_text(encoding="utf-8"))
              for pid in project["phases"]]
    return {"project": project["project"], "engine": project["engine"], "phases": phases}


def save_phase(phase: dict, backlog_dir: pathlib.Path = BACKLOG_DIR) -> None:
    text = json.dumps(phase, ensure_ascii=False, indent=2) + "\n"
    (backlog_dir / "fases" / f"{phase['id']}.json").write_text(text, encoding="utf-8")


def validate(backlog: dict) -> None:
    seen = set()
    for phase in backlog["phases"]:
        for task in phase["tasks"]:
            tid = task["id"]
            if tid in seen:
                raise ValueError(f"ID duplicado: {tid}")
            seen.add(tid)
            if task["status"] not in VALID_STATUS:
                raise ValueError(f"Estado inválido en {tid}: {task['status']}")
            if not tid.startswith(phase["id"] + "-"):
                raise ValueError(f"{tid} no pertenece a {phase['id']}")
            if task["status"] == "done" and not (task.get("pr") and task.get("session")):
                raise ValueError(f"{tid} está hecha sin PR o sin sesión")


def percent(done: int, total: int) -> float:
    return 0.0 if total == 0 else round(100.0 * done / total, 1)


def summarize(backlog: dict) -> dict:
    validate(backlog)
    phases = []
    for p in backlog["phases"]:
        done = sum(t["status"] == "done" for t in p["tasks"])
        total = len(p["tasks"])
        phases.append({"id": p["id"], "name": p["name"], "done": done,
                       "total": total, "pct": percent(done, total)})
    done = sum(p["done"] for p in phases)
    total = sum(p["total"] for p in phases)
    current = next((p["id"] for p in phases if p["done"] < p["total"]), None)
    next_task = next((t["id"] for p in backlog["phases"] for t in p["tasks"] if t["status"] != "done"), None)
    return {"done": done, "total": total, "pct": percent(done, total), "phases": phases,
            "current_phase": current, "next_task": next_task}


def find_task(backlog: dict, task_id: str):
    for phase in backlog["phases"]:
        for task in phase["tasks"]:
            if task["id"] == task_id:
                return phase, task
    raise KeyError(task_id)


def set_status(backlog: dict, task_id: str, status: str, pr: str = None, session: int = None) -> dict:
    if status not in VALID_STATUS:
        raise ValueError(status)
    phase, task = find_task(backlog, task_id)
    task["status"] = status
    if pr is not None:
        task["pr"] = pr
    if session is not None:
        task["session"] = session
    return phase


def last_session(backlog: dict) -> int:
    return max((t.get("session") or 0 for p in backlog["phases"] for t in p["tasks"]), default=0)
