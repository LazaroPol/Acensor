import unittest

from tools.progress import model
from tools.tests.helpers import make_backlog


class PercentTests(unittest.TestCase):
    def test_zero_total(self):
        self.assertEqual(model.percent(0, 0), 0.0)

    def test_rounds_to_one_decimal(self):
        self.assertEqual(model.percent(1, 3), 33.3)


class SummaryTests(unittest.TestCase):
    def test_counts_only_done_and_finds_current_and_next(self):
        s = model.summarize(make_backlog([("F0", ["done", "done"]), ("F1", ["in_progress", "todo"])]))
        self.assertEqual((s["done"], s["total"], s["pct"]), (2, 4, 50.0))
        self.assertEqual((s["current_phase"], s["next_task"]), ("F1", "F1-01"))

    def test_complete_project_has_no_current(self):
        s = model.summarize(make_backlog([("F0", ["done"])]))
        self.assertIsNone(s["current_phase"])
        self.assertIsNone(s["next_task"])


class ValidationTests(unittest.TestCase):
    def assertInvalid(self, backlog):
        self.assertRaises(ValueError, model.validate, backlog)

    def test_duplicate_ids(self):
        b = make_backlog([("F0", ["todo", "todo"])])
        b["phases"][0]["tasks"][1]["id"] = "F0-01"
        self.assertInvalid(b)

    def test_bad_status(self):
        b = make_backlog([("F0", ["todo"])])
        b["phases"][0]["tasks"][0]["status"] = "wip"
        self.assertInvalid(b)

    def test_task_in_wrong_phase(self):
        b = make_backlog([("F0", ["todo"])])
        b["phases"][0]["tasks"][0]["id"] = "F9-01"
        self.assertInvalid(b)

    def test_done_requires_pr_and_session(self):
        for field in ("pr", "session"):
            b = make_backlog([("F0", ["done"])])
            b["phases"][0]["tasks"][0][field] = None
            self.assertInvalid(b)


class UpdateTests(unittest.TestCase):
    def test_set_status_updates_fields_and_returns_phase(self):
        b = make_backlog([("F0", ["todo"])])
        phase = model.set_status(b, "F0-01", "done", "#7", 4)
        self.assertEqual(phase["id"], "F0")
        self.assertEqual(b["phases"][0]["tasks"][0]["pr"], "#7")
        self.assertEqual(b["phases"][0]["tasks"][0]["session"], 4)

    def test_set_status_rejects_unknown_task_and_status(self):
        b = make_backlog([("F0", ["todo"])])
        self.assertRaises(KeyError, model.set_status, b, "F9-99", "done")
        self.assertRaises(ValueError, model.set_status, b, "F0-01", "wip")

    def test_last_session(self):
        self.assertEqual(model.last_session(make_backlog([("F0", ["done", "done", "todo"])])), 2)
        self.assertEqual(model.last_session(make_backlog([("F0", ["todo"])])), 0)


if __name__ == "__main__":
    unittest.main()
