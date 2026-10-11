"""Verifica que la documentación de diseño mantiene sus secciones clave."""
import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[2]


class GddTests(unittest.TestCase):
    REQUIRED = [
        "Bucle de juego", "Piso 0", "Ascensor-Refugio", "Profesiones", "Pisos",
        "Ecos", "actos", "Lore", "ADN", "Dirección artística", "Arquitectura técnica",
    ]

    def test_gdd_contains_required_sections(self):
        text = (ROOT / "docs/GDD.md").read_text(encoding="utf-8")
        headings = "\n".join(l for l in text.splitlines() if l.startswith("#"))
        for section in self.REQUIRED:
            self.assertIn(section.lower(), headings.lower(), f"Falta la sección '{section}' en el GDD")

    def test_gdd_targets_unity(self):
        text = (ROOT / "docs/GDD.md").read_text(encoding="utf-8")
        self.assertIn("Unity", text)



class HistoryTests(unittest.TestCase):
    """Cada fase cerrada debe tener su informe de sesión enlazado en el historial."""

    def test_every_completed_phase_has_session_report_in_history(self):
        import json
        backlog = json.loads((ROOT / "docs/backlog.json").read_text(encoding="utf-8"))
        history = (ROOT / "docs/HISTORIAL.md").read_text(encoding="utf-8")
        for phase in backlog["phases"]:
            if all(t["status"] == "done" for t in phase["tasks"]):
                name = f"SESION-{phase['session']:02d}"
                self.assertTrue((ROOT / f"docs/sessions/{name}.md").is_file(), f"Falta el informe {name}")
                self.assertIn(f"sessions/{name}.md", history, f"{name} no está en HISTORIAL.md")

    def test_every_done_task_references_an_existing_or_pending_pr(self):
        import json
        backlog = json.loads((ROOT / "docs/backlog.json").read_text(encoding="utf-8"))
        prs = sorted(p.stem for p in (ROOT / "docs/pull-requests").glob("PR-*.md"))
        last = int(prs[-1].split("-")[1]) if prs else 0
        for phase in backlog["phases"]:
            for t in phase["tasks"]:
                if t["status"] == "done":
                    n = int(t["pr"].split("-")[1])
                    self.assertLessEqual(n, last + 1, f"{t['id']} apunta a una PR inexistente")


if __name__ == "__main__":
    unittest.main()
