"""Urgencias: una «Listo» con etiqueta `urgente` interrumpe a la que está en curso y la deja en pausa."""
import json
import pathlib
import shutil
import tempfile
import unittest

from tools.progress import __main__ as cli
from tools.progress import model, rules
from tools.tests.helpers import ROOT, make_backlog


def _with(statuses, urgent=(), priority=None):
    b = make_backlog([("F0", statuses)])
    for t in b["phases"][0]["tasks"]:
        t["labels"] = ["urgente"] if t["id"] in urgent else []
    b["priority"] = priority or [t["id"] for t in b["phases"][0]["tasks"]]
    return b


class ChoiceTests(unittest.TestCase):
    def test_urgent_ready_task_goes_before_the_one_in_progress(self):
        self.assertEqual(rules.next_task(_with(["in_progress", "todo", "todo"], urgent={"F0-03"})), "F0-03")

    def test_urgent_task_in_progress_is_not_interrupted(self):
        b = _with(["in_progress", "todo"], urgent={"F0-01", "F0-02"})
        self.assertEqual(rules.next_task(b), "F0-01")

    def test_paused_task_resumes_right_after_the_urgent_ones(self):
        b = _with(["todo", "paused", "done"], priority=["F0-01", "F0-02"])
        self.assertEqual(rules.next_task(b), "F0-02")

    def test_urgent_runs_before_paused(self):
        self.assertEqual(rules.next_task(_with(["paused", "todo"], urgent={"F0-02"})), "F0-02")

    def test_without_urgents_nothing_changes(self):
        self.assertEqual(rules.next_task(_with(["todo", "in_progress"])), "F0-02")

    def test_urgent_label_on_an_idea_does_not_interrupt(self):
        self.assertIsNone(rules.preempting(_with(["in_progress", "idea"], urgent={"F0-02"}), "F0-01"))


class PreemptionTests(unittest.TestCase):
    def test_preempting_names_the_urgent(self):
        self.assertEqual(rules.preempting(_with(["todo", "todo"], urgent={"F0-02"}), "F0-01"), "F0-02")

    def test_no_urgent_means_keep_going(self):
        self.assertIsNone(rules.preempting(_with(["todo", "todo"]), "F0-01"))

    def test_paused_is_valid_and_counts_as_pending(self):
        b = _with(["paused", "done"])
        model.validate(b)
        self.assertEqual(model.summarize(b)["done"], 1)
        self.assertEqual(model.summarize(b)["total"], 2)


class CliTests(unittest.TestCase):
    def setUp(self):
        self.dir = pathlib.Path(tempfile.mkdtemp())
        shutil.copytree(ROOT / "docs/backlog", self.dir / "backlog")
        self.backlog = self.dir / "backlog"

    def tearDown(self):
        shutil.rmtree(self.dir)

    def _card(self, tid):
        return json.loads((self.backlog / "tareas" / f"{tid}.json").read_text(encoding="utf-8"))

    def test_urgent_flag_exits_1_when_nothing_interrupts(self):
        first = model.load(self.backlog)["priority"][0]
        self.assertEqual(cli.main(["--urgent", first, "--dir", str(self.backlog)]), 1)

    def test_urgent_flag_reports_the_urgent_task(self):
        order = model.load(self.backlog)["priority"]
        card = self._card(order[1])
        card["labels"] = card["labels"] + ["urgente"]
        (self.backlog / "tareas" / f"{order[1]}.json").write_text(json.dumps(card), encoding="utf-8")
        self.assertEqual(cli.main(["--urgent", order[0], "--dir", str(self.backlog)]), 0)


if __name__ == "__main__":
    unittest.main()
