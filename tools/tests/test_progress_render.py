import unittest

from tools.progress import render
from tools.tests.helpers import make_backlog


class RenderTests(unittest.TestCase):
    def test_bar_width_and_clamping(self):
        self.assertEqual(render.bar(50, 10), "█████░░░░░")
        self.assertEqual(render.bar(150, 4), "████")
        self.assertEqual(render.bar(-5, 4), "░░░░")

    def test_progress_page_shows_total_and_links_phases(self):
        text = render.render_progress(make_backlog([("F0", ["done", "todo"])]))
        self.assertIn("50.0%", text)
        self.assertIn("(fases/F0.md)", text)
        self.assertIn("**Siguiente tarea:** F0-02", text)

    def test_phase_page_lists_session_and_pr(self):
        b = make_backlog([("F0", ["done", "todo"])])
        text = render.render_phase(b["phases"][0])
        self.assertIn("| ✅ | **F0-01** t | S01 | #1 |", text)
        self.assertIn("| ⬜ | **F0-02** t |  |  |", text)

    def test_outputs_has_one_page_per_phase(self):
        files = render.outputs(make_backlog([("F0", ["todo"]), ("F1", ["todo"])]))
        self.assertEqual(sorted(files), ["PROGRESO.md", "fases/F0.md", "fases/F1.md"])


if __name__ == "__main__":
    unittest.main()
