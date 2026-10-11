"""Utilidades compartidas por los tests de herramientas."""
import pathlib

ROOT = pathlib.Path(__file__).resolve().parents[2]


def make_backlog(tasks_by_phase):
    """[("F0", ["done", "todo"]), ...] -> backlog en memoria."""
    phases = []
    for pid, statuses in tasks_by_phase:
        tasks = []
        for n, st in enumerate(statuses, 1):
            done = st == "done"
            tasks.append({"id": f"{pid}-{n:02d}", "phase": pid, "title": "t", "status": st, "origin": "backlog",
                          "pr": "#1" if done else None, "session": n if done else None})
        phases.append({"id": pid, "name": pid, "goal": "", "tasks": tasks})
    return {"project": "T", "engine": "Unity", "phases": phases, "priority": []}
