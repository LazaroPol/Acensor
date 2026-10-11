"""Reglas del proceso aplicadas al backlog real del repositorio."""
import unittest

from tools.progress import model
from tools.progress.__main__ import stale_outputs
from tools.tests.helpers import ROOT

SESSIONS = ROOT / "docs/sesiones"


class BacklogRulesTests(unittest.TestCase):
    def setUp(self):
        self.backlog = model.load()

    def test_backlog_is_valid(self):
        model.validate(self.backlog)

    def test_generated_pages_are_up_to_date(self):
        self.assertEqual(stale_outputs(self.backlog), [], "Ejecuta: python3 -m tools.progress")

    def test_phases_are_completed_in_order(self):
        seen_incomplete = False
        for p in model.summarize(self.backlog)["phases"]:
            if seen_incomplete:
                self.assertEqual(p["done"], 0, f"{p['id']} avanza antes de cerrar fases anteriores")
            seen_incomplete = seen_incomplete or p["done"] < p["total"]

    def test_at_most_one_task_in_progress(self):
        active = [t["id"] for p in self.backlog["phases"] for t in p["tasks"] if t["status"] == "in_progress"]
        self.assertLessEqual(len(active), 1, f"Solo una tarea por sesión: {active}")


class SessionHistoryTests(unittest.TestCase):
    def setUp(self):
        self.backlog = model.load()
        self.index = (SESSIONS / "README.md").read_text(encoding="utf-8")

    def done_by_session(self):
        out = {}
        for p in self.backlog["phases"]:
            for t in p["tasks"]:
                if t["status"] == "done":
                    out.setdefault(t["session"], []).append(t["id"])
        return out

    def test_every_session_has_a_report_listing_its_tasks(self):
        for session, ids in self.done_by_session().items():
            report = SESSIONS / f"SESION-{session:02d}.md"
            self.assertTrue(report.is_file(), f"Falta {report.name}")
            text = report.read_text(encoding="utf-8")
            for tid in ids:
                self.assertIn(tid, text, f"{report.name} no menciona {tid}")

    def test_history_index_links_every_session(self):
        for session in self.done_by_session():
            self.assertIn(f"(SESION-{session:02d}.md)", self.index)

    def test_sessions_after_the_first_cover_exactly_one_task(self):
        for session, ids in self.done_by_session().items():
            if session > 1:
                self.assertEqual(len(ids), 1, f"La sesión {session} cierra varias tareas: {ids}")


if __name__ == "__main__":
    unittest.main()
