"""Interrupción por urgencias (reglas puras).

Una tarea «Listo» con la etiqueta `urgente` pasa por delante de la que está en curso: la interrumpida
queda en «En pausa» (`paused`) y se retoma justo después de las urgentes, antes que el resto de «Listo».
"""
URGENT = "urgente"


def is_urgent(task: dict) -> bool:
    return URGENT in (task.get("labels") or [])


def _first(tasks: list, status: str, urgent_only: bool = False):
    return next((t for t in tasks if t["status"] == status and (not urgent_only or is_urgent(t))), None)


def choose(tasks: list):
    """Siguiente tarea dada la cola ya ordenada por prioridad.

    Orden: en curso urgente > urgente lista > en curso > en pausa > primera lista.
    """
    current = _first(tasks, "in_progress")
    if current and is_urgent(current):
        return current["id"]
    for pick in (_first(tasks, "todo", urgent_only=True), current,
                 _first(tasks, "paused"), _first(tasks, "todo")):
        if pick:
            return pick["id"]
    return None


def preempting(tasks: list, current_id: str):
    """ID de la urgente que debe interrumpir a `current_id`, o None si se puede seguir."""
    current = next((t for t in tasks if t["id"] == current_id), None)
    if current is None or is_urgent(current):
        return None
    urgent = next((t for t in tasks if t["id"] != current_id and t["status"] == "todo" and is_urgent(t)), None)
    return urgent["id"] if urgent else None
