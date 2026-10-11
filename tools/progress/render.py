"""Renderizado Markdown del backlog: barra de progreso y una página por fase."""
from . import model

ICON = {"done": "✅", "in_progress": "🔄", "todo": "⬜"}


def bar(pct: float, width: int = 30) -> str:
    pct = max(0.0, min(100.0, pct))
    filled = int(round(width * pct / 100.0))
    return "█" * filled + "░" * (width - filled)


def render_progress(backlog: dict) -> str:
    s = model.summarize(backlog)
    lines = [
        f"# Progreso — {backlog['project']}", "",
        "```",
        f"TOTAL  {bar(s['pct'])} {s['pct']:5.1f}%  ({s['done']}/{s['total']} tareas)",
        "```", "",
        f"**Fase en curso:** {s['current_phase'] or 'Proyecto completado'} · "
        f"**Siguiente tarea:** {s['next_task'] or '—'}", "",
        "| Fase | Progreso | % | Tareas |",
        "|---|---|---|---|",
    ]
    for p in s["phases"]:
        link = f"[{p['id']} · {p['name']}](fases/{p['id']}.md)"
        lines.append(f"| {link} | `{bar(p['pct'], 15)}` | {p['pct']:.0f}% | {p['done']}/{p['total']} |")
    return "\n".join(lines) + "\n"


def render_phase(phase: dict) -> str:
    lines = [f"# {phase['id']} — {phase['name']}", "", f"_{phase['goal']}_", "",
             "| | Tarea | Sesión | PR |", "|---|---|---|---|"]
    for t in phase["tasks"]:
        session = f"S{t['session']:02d}" if t.get("session") else ""
        lines.append(f"| {ICON[t['status']]} | **{t['id']}** {t['title']} | {session} | {t.get('pr') or ''} |")
    return "\n".join(lines) + "\n"


def outputs(backlog: dict) -> dict:
    """Ruta relativa a docs/backlog -> contenido generado."""
    files = {"PROGRESO.md": render_progress(backlog)}
    for phase in backlog["phases"]:
        files[f"fases/{phase['id']}.md"] = render_phase(phase)
    return files
