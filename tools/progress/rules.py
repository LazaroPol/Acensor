"""Reglas puras del backlog: estados, validación y elección de la siguiente tarea."""

from . import urgency

# idea = bandeja de entrada (sin refinar, no cuenta ni se trabaja); todo = «Listo»;
# paused = interrumpida por una urgente, se retoma justo después.
VALID_STATUS = {"idea", "todo", "in_progress", "paused", "review", "blocked", "done"}
COUNTED = VALID_STATUS - {"idea"}
PENDING = COUNTED - {"done"}


def percent(done: int, total: int) -> float:
    return 0.0 if total == 0 else round(100.0 * done / total, 1)


def task_key(task: dict):
    """Orden natural por número: F3-02 < F3-10."""
    prefix, _, num = task["id"].rpartition("-")
    return (prefix, int(num) if num.isdigit() else 0)


def _tasks(backlog: dict):
    return [t for p in backlog["phases"] for t in p["tasks"]]


def priority_order(backlog: dict) -> list:
    """Tareas en orden de prioridad: primero la cola del tablero, luego el resto en orden de backlog."""
    by_id = {t["id"]: t for t in _tasks(backlog)}
    ordered = [by_id[i] for i in backlog.get("priority", []) if i in by_id]
    seen = {t["id"] for t in ordered}
    return ordered + [t for t in _tasks(backlog) if t["id"] not in seen]


def next_task(backlog: dict):
    """Urgentes primero; luego la tarea en curso, la pausada y la primera «Listo» de la cola."""
    return urgency.choose(priority_order(backlog))


def preempting(backlog: dict, current_id: str):
    """La urgente que obliga a pausar `current_id`, o None."""
    return urgency.preempting(priority_order(backlog), current_id)


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
            manual = task.get("origin") == "tablero"
            if task["status"] == "done" and not manual and not (task.get("pr") and task.get("session")):
                raise ValueError(f"{tid} está hecha sin PR o sin sesión")
    order = backlog.get("priority", [])
    if len(order) != len(set(order)):
        raise ValueError("prioridad.json tiene IDs repetidos")
    unknown = [i for i in order if i not in seen]
    if unknown:
        raise ValueError(f"prioridad.json menciona tareas inexistentes: {unknown}")
