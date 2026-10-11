#!/usr/bin/env python3
"""Genera docs/BACKLOG.md y docs/PROGRESS.md a partir de docs/backlog.json.

Uso:
  python3 tools/progress.py          # regenera los ficheros
  python3 tools/progress.py --check  # falla si los ficheros no están al día
  python3 tools/progress.py --complete F0-05  # marca la tarea como hecha con la PR que se va a abrir
"""
import json
import pathlib
import sys

ROOT = pathlib.Path(__file__).resolve().parents[1]
BACKLOG = ROOT / "docs/backlog.json"
VALID_STATUS = {"todo", "in_progress", "done"}
ICON = {"done": "✅", "in_progress": "🔄", "todo": "⬜"}


def percent(done: int, total: int) -> float:
    return 0.0 if total == 0 else round(100.0 * done / total, 1)


def bar(pct: float, width: int = 30) -> str:
    pct = max(0.0, min(100.0, pct))
    filled = int(round(width * pct / 100.0))
    return "█" * filled + "░" * (width - filled)


def validate(backlog: dict) -> None:
    seen = set()
    for phase in backlog["phases"]:
        for task in phase["tasks"]:
            if task["id"] in seen:
                raise ValueError(f"ID duplicado: {task['id']}")
            seen.add(task["id"])
            if task["status"] not in VALID_STATUS:
                raise ValueError(f"Estado inválido en {task['id']}: {task['status']}")
            if not task["id"].startswith(phase["id"] + "-"):
                raise ValueError(f"{task['id']} no pertenece a {phase['id']}")
            if task["status"] == "done" and not task.get("pr"):
                raise ValueError(f"{task['id']} está hecha sin PR asociada")


def summarize(backlog: dict) -> dict:
    validate(backlog)
    phases = []
    for p in backlog["phases"]:
        done = sum(t["status"] == "done" for t in p["tasks"])
        total = len(p["tasks"])
        phases.append({"id": p["id"], "name": p["name"], "session": p["session"],
                       "done": done, "total": total, "pct": percent(done, total)})
    done = sum(p["done"] for p in phases)
    total = sum(p["total"] for p in phases)
    current = next((p for p in phases if p["done"] < p["total"]), None)
    return {"done": done, "total": total, "pct": percent(done, total), "phases": phases,
            "current_phase": current["id"] if current else None}


def render_progress(backlog: dict) -> str:
    s = summarize(backlog)
    lines = [
        f"# Progreso — {backlog['project']}",
        "",
        "```",
        f"TOTAL  {bar(s['pct'])} {s['pct']:5.1f}%  ({s['done']}/{s['total']} tareas)",
        "```",
        "",
        f"**Fase en curso:** {s['current_phase'] or 'Proyecto completado'}",
        "",
        "| Fase | Sesión | Progreso | % | Tareas |",
        "|---|---|---|---|---|",
    ]
    for p in s["phases"]:
        lines.append(f"| {p['id']} · {p['name']} | {p['session']} | `{bar(p['pct'], 15)}` | {p['pct']:.0f}% | {p['done']}/{p['total']} |")
    return "\n".join(lines) + "\n"


def render_backlog(backlog: dict) -> str:
    lines = [f"# Backlog — {backlog['project']}", "",
             "Una fase = una sesión. Orden lógico de dependencias: cada fase se apoya en las anteriores.",
             "Leyenda: ✅ hecha · 🔄 en curso · ⬜ pendiente", ""]
    for p in backlog["phases"]:
        lines += [f"## {p['id']} — {p['name']} (Sesión {p['session']})", "", f"_{p['goal']}_", ""]
        for t in p["tasks"]:
            pr = f" — {t['pr']}" if t.get("pr") else ""
            lines.append(f"- {ICON[t['status']]} **{t['id']}** {t['title']}{pr}")
        lines.append("")
    return "\n".join(lines)


def outputs(backlog: dict) -> dict:
    return {ROOT / "docs/PROGRESS.md": render_progress(backlog),
            ROOT / "docs/BACKLOG.md": render_backlog(backlog)}


def next_pr_id(pr_dir: pathlib.Path) -> str:
    count = len(list(pr_dir.glob("PR-*.md"))) if pr_dir.exists() else 0
    return f"PR-{count + 1:03d}"


def complete(backlog: dict, task_id: str, pr: str) -> dict:
    for phase in backlog["phases"]:
        for task in phase["tasks"]:
            if task["id"] == task_id:
                task["status"], task["pr"] = "done", pr
                return backlog
    raise KeyError(task_id)


def main(argv) -> int:
    backlog = json.loads(BACKLOG.read_text(encoding="utf-8"))
    if "--complete" in argv:
        task_id = argv[argv.index("--complete") + 1]
        complete(backlog, task_id, next_pr_id(ROOT / "docs/pull-requests"))
        BACKLOG.write_text(json.dumps(backlog, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    files = outputs(backlog)
    if "--check" in argv:
        stale = [str(p.relative_to(ROOT)) for p, c in files.items()
                 if not p.exists() or p.read_text(encoding="utf-8") != c]
        if stale:
            print("Ficheros desactualizados (ejecuta tools/progress.py):", ", ".join(stale))
            return 1
        return 0
    for path, content in files.items():
        path.write_text(content, encoding="utf-8")
    s = summarize(backlog)
    print(f"{bar(s['pct'])} {s['pct']}% ({s['done']}/{s['total']})")
    return 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
