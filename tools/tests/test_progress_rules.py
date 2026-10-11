"""Cola de prioridad del tablero, estados nuevos y reglas de validación."""
import unittest

from tools.progress import model, rules
from tools.tests.helpers import make_backlog


class PriorityTests(unittest.TestCase):
    def test_next_task_follows_board_priority(self):
        b = make_backlog([("F0", ["todo", "todo"]), ("F1", ["todo"])])
        b["priority"] = ["F1-01", "F0-02"]
        self.assertEqual(rules.next_task(b), "F1-01")

    def test_task_in_progress_always_wins(self):
        b = make_backlog([("F0", ["todo", "in_progress"])])
        b["priority"] = ["F0-01", "F0-02"]
        self.assertEqual(rules.next_task(b), "F0-02")

    def test_ideas_blocked_and_review_are_never_picked(self):
        b = make_backlog([("F0", ["idea", "blocked", "review", "todo"])])
        self.assertEqual(rules.next_task(b), "F0-04")

    def test_tasks_missing_from_queue_go_last_in_backlog_order(self):
        b = make_backlog([("F0", ["todo", "todo", "todo"])])
        b["priority"] = ["F0-03"]
        self.assertEqual([t["id"] for t in rules.priority_order(b)], ["F0-03", "F0-01", "F0-02"])

    def test_natural_order_by_number(self):
        self.assertLess(rules.task_key({"id": "F3-02"}), rules.task_key({"id": "F3-10"}))


class CountingTests(unittest.TestCase):
    def test_ideas_do_not_count_towards_progress(self):
        s = model.summarize(make_backlog([("F0", ["done", "idea", "todo"])]))
        self.assertEqual((s["done"], s["total"]), (1, 2))


class BoardValidationTests(unittest.TestCase):
    def test_manual_board_task_can_be_done_without_pr(self):
        b = make_backlog([("F0", ["done"])])
        b["phases"][0]["tasks"][0].update(pr=None, session=None, origin="tablero")
        model.validate(b)

    def test_priority_rejects_unknown_and_repeated_ids(self):
        for order in (["F9-01"], ["F0-01", "F0-01"]):
            b = make_backlog([("F0", ["todo"])])
            b["priority"] = order
            self.assertRaises(ValueError, model.validate, b)


if __name__ == "__main__":
    unittest.main()
