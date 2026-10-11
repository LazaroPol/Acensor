"""Operaciones del tablero sobre los ficheros del backlog (tarjetas + cola de prioridad).

Cada mutación valida el backlog completo antes de escribir y regenera las páginas Markdown.
"""
import json
import pathlib
import threading

from tools.progress import model, rules
from tools.progress.__main__ import write_outputs

EDITABLE = {"title", "description", "acceptance", "labels", "type", "status"}


class BoardError(ValueError):
    """Petición inválida del tablero (se devuelve como 400)."""


class BoardStore:
    def __init__(self, backlog_dir: pathlib.Path = model.BACKLOG_DIR):
        self.dir = backlog_dir
        self.lock = threading.Lock()

    def snapshot(self) -> dict:
        backlog = model.load(self.dir)
        catalog = json.loads((self.dir / "etiquetas.json").read_text(encoding="utf-8"))
        phases = [{k: p[k] for k in ("id", "name", "goal")} for p in backlog["phases"]]
        return {"project": backlog["project"], "phases": phases, "tasks": model.all_tasks(backlog),
                "priority": [t["id"] for t in rules.priority_order(backlog)],
                "catalog": catalog, "summary": model.summarize(backlog)}

    def create(self, data: dict) -> dict:
        with self.lock:
            backlog = model.load(self.dir)
            phase = next((p for p in backlog["phases"] if p["id"] == data.get("phase")), None)
            title = (data.get("title") or "").strip()
            if not phase or not title:
                raise BoardError("Hacen falta fase y título")
            nums = [rules.task_key(t)[1] for t in phase["tasks"]]
            task = {"id": f"{phase['id']}-{max(nums, default=0) + 1:02d}", "phase": phase["id"],
                    "title": title, "status": data.get("status") or "idea", "type": data.get("type") or "feature",
                    "labels": list(data.get("labels") or []), "origin": "tablero",
                    "description": data.get("description") or "", "acceptance": [], "pr": None, "session": None}
            order = self._order(backlog)
            phase["tasks"].append(task)
            order.insert(0 if data.get("position") == "top" else len(order), task["id"])
            self._commit(backlog, [task], order)
            return task

    def update(self, task_id: str, fields: dict) -> dict:
        unknown = set(fields) - EDITABLE
        if unknown:
            raise BoardError(f"Campos no editables: {sorted(unknown)}")
        with self.lock:
            backlog = model.load(self.dir)
            task = self._find(backlog, task_id)
            task.update(fields)
            self._commit(backlog, [task], self._order(backlog))
            return task

    def move(self, task_id: str, status: str, before: str = None) -> dict:
        """Mueve una tarjeta a otra columna y la coloca justo antes de `before` (o al final)."""
        with self.lock:
            backlog = model.load(self.dir)
            task = self._find(backlog, task_id)
            task["status"] = status
            order = [i for i in self._order(backlog) if i != task_id]
            if before and before in order:
                order.insert(order.index(before), task_id)
            else:
                order.append(task_id)
            self._commit(backlog, [task], order)
            return task

    def delete(self, task_id: str) -> None:
        with self.lock:
            backlog = model.load(self.dir)
            task = self._find(backlog, task_id)
            if task.get("origin") != "tablero" or task["status"] == "done":
                raise BoardError("Solo se borran tarjetas creadas en el tablero y no terminadas")
            for p in backlog["phases"]:
                p["tasks"] = [t for t in p["tasks"] if t["id"] != task_id]
            self._commit(backlog, [], self._order(backlog))
            (self.dir / "tareas" / f"{task_id}.json").unlink()

    # -- internos -------------------------------------------------------------
    def _find(self, backlog: dict, task_id: str) -> dict:
        try:
            return model.find_task(backlog, task_id)[1]
        except KeyError:
            raise BoardError(f"No existe la tarea {task_id}") from None

    def _order(self, backlog: dict) -> list:
        """Cola completa sin tareas terminadas (las hechas no necesitan prioridad)."""
        return [t["id"] for t in rules.priority_order(backlog) if t["status"] != "done"]

    def _commit(self, backlog: dict, changed: list, order: list) -> None:
        backlog["priority"] = [i for i in order if model.find_task(backlog, i)[1]["status"] != "done"]
        try:
            model.validate(backlog)
        except ValueError as e:
            raise BoardError(str(e)) from None
        active = [t["id"] for t in model.all_tasks(backlog) if t["status"] == "in_progress"]
        if len(active) > 1:
            raise BoardError(f"Solo puede haber una tarea en curso: {active}")
        for task in changed:
            model.save_task(task, self.dir)
        model.save_priority(backlog["priority"], self.dir)
        write_outputs(backlog, self.dir)
