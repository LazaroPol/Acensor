import json
import pathlib
import sys
import unittest

sys.path.insert(0, str(pathlib.Path(__file__).resolve().parents[1]))
import progress  # noqa: E402


def make(tasks_by_phase):
    return {"project": "T", "phases": [
        {"id": pid, "name": pid, "session": i + 1, "goal": "",
         "tasks": [{"id": f"{pid}-{n:02d}", "title": "t", "status": st, "pr": "PR-1" if st == "done" else None}
                   for n, st in enumerate(sts, 1)]}
        for i, (pid, sts) in enumerate(tasks_by_phase)]}


class ProgressTests(unittest.TestCase):
    def test_percent_handles_zero_total(self):
        self.assertEqual(progress.percent(0, 0), 0.0)

    def test_percent_rounds_to_one_decimal(self):
        self.assertEqual(progress.percent(1, 3), 33.3)

    def test_bar_width_and_clamping(self):
        self.assertEqual(progress.bar(50, 10), "█████░░░░░")
        self.assertEqual(progress.bar(150, 4), "████")
        self.assertEqual(progress.bar(-5, 4), "░░░░")

    def test_summary_counts_only_done(self):
        s = progress.summarize(make([("F0", ["done", "done"]), ("F1", ["in_progress", "todo"])]))
        self.assertEqual((s["done"], s["total"], s["pct"]), (2, 4, 50.0))
        self.assertEqual(s["current_phase"], "F1")

    def test_current_phase_none_when_complete(self):
        self.assertIsNone(progress.summarize(make([("F0", ["done"])]))["current_phase"])

    def test_validate_rejects_duplicates_bad_status_wrong_phase_and_done_without_pr(self):
        b = make([("F0", ["todo", "todo"])])
        b["phases"][0]["tasks"][1]["id"] = "F0-01"
        self.assertRaises(ValueError, progress.validate, b)
        b = make([("F0", ["todo"])]); b["phases"][0]["tasks"][0]["status"] = "wip"
        self.assertRaises(ValueError, progress.validate, b)
        b = make([("F0", ["todo"])]); b["phases"][0]["tasks"][0]["id"] = "F9-01"
        self.assertRaises(ValueError, progress.validate, b)
        b = make([("F0", ["done"])]); b["phases"][0]["tasks"][0]["pr"] = None
        self.assertRaises(ValueError, progress.validate, b)

    def test_next_pr_id_counts_existing_records(self):
        import tempfile
        with tempfile.TemporaryDirectory() as d:
            d = pathlib.Path(d)
            self.assertEqual(progress.next_pr_id(d / "missing"), "PR-001")
            (d / "PR-001.md").write_text("x"); (d / "PR-002.md").write_text("x")
            self.assertEqual(progress.next_pr_id(d), "PR-003")

    def test_complete_marks_task_and_rejects_unknown(self):
        b = progress.complete(make([("F0", ["todo"])]), "F0-01", "PR-009")
        self.assertEqual(b["phases"][0]["tasks"][0], {"id": "F0-01", "title": "t", "status": "done", "pr": "PR-009"})
        self.assertRaises(KeyError, progress.complete, b, "F9-99", "PR-1")

    def test_real_backlog_is_valid_and_generated_docs_are_up_to_date(self):
        backlog = json.loads(progress.BACKLOG.read_text(encoding="utf-8"))
        progress.validate(backlog)
        self.assertEqual(progress.main(["--check"]), 0, "Ejecuta tools/progress.py para regenerar los docs")

    def test_phases_are_completed_in_order(self):
        """Orden lógico: no puede haber tareas hechas en una fase si una fase anterior está incompleta."""
        backlog = json.loads(progress.BACKLOG.read_text(encoding="utf-8"))
        s = progress.summarize(backlog)
        seen_incomplete = False
        for p in s["phases"]:
            if seen_incomplete:
                self.assertEqual(p["done"], 0, f"{p['id']} avanza antes de cerrar fases anteriores")
            if p["done"] < p["total"]:
                seen_incomplete = True


if __name__ == "__main__":
    unittest.main()
