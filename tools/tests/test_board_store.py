"""El tablero escribe tarjetas y prioridad válidas sobre una copia del backlog real."""
import json
import shutil
import tempfile
import unittest
import pathlib

from tools.board.store import BoardError, BoardStore
from tools.progress import model


class BoardStoreTests(unittest.TestCase):
    def setUp(self):
        self.tmp = pathlib.Path(tempfile.mkdtemp()) / "backlog"
        shutil.copytree(model.BACKLOG_DIR, self.tmp)
        self._without_tasks_in_progress()
        self.store = BoardStore(self.tmp)

    def tearDown(self):
        shutil.rmtree(self.tmp.parent)

    def _without_tasks_in_progress(self):
        """Aísla los tests de la sesión activa: en la copia no queda ninguna tarea en curso."""
        for card in (self.tmp / "tareas").glob("*.json"):
            data = json.loads(card.read_text(encoding="utf-8"))
            if data.get("status") == "in_progress":
                data["status"] = "todo"
                card.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")

    def backlog(self):
        return model.load(self.tmp)

    def test_snapshot_has_tasks_priority_and_catalog(self):
        snap = self.store.snapshot()
        self.assertGreater(len(snap["tasks"]), 70)
        self.assertIn("tipos", snap["catalog"])
        self.assertEqual(snap["priority"][0], snap["summary"]["next_task"])

    def test_quick_task_gets_next_id_in_phase_and_goes_to_inbox(self):
        task = self.store.create({"phase": "F4", "title": "Rampa", "position": "top"})
        self.assertEqual((task["id"], task["status"], task["origin"]), ("F4-06", "idea", "tablero"))
        self.assertEqual(self.backlog()["priority"][0], "F4-06")
        self.assertIn("F4-06", (self.tmp / "fases/F4.md").read_text(encoding="utf-8"))

    def test_moving_to_ready_before_first_makes_it_the_next_task(self):
        task = self.store.create({"phase": "PG", "title": "Colisiones"})
        first = model.summarize(self.backlog())["next_task"]
        self.store.move(task["id"], "todo", before=first)
        self.assertEqual(model.summarize(self.backlog())["next_task"], task["id"])

    def test_update_only_editable_fields(self):
        self.store.update("F4-01", {"labels": ["ascensor", "urgente"], "description": "x"})
        self.assertEqual(model.find_task(self.backlog(), "F4-01")[1]["labels"], ["ascensor", "urgente"])
        self.assertRaises(BoardError, self.store.update, "F4-01", {"id": "F9-99"})

    def test_rejects_two_tasks_in_progress_and_invalid_status(self):
        self.store.move("F4-01", "in_progress")
        self.assertRaises(BoardError, self.store.move, "F4-02", "in_progress")
        self.assertRaises(BoardError, self.store.update, "F4-02", {"status": "wip"})

    def test_backlog_tasks_need_pr_to_be_done_but_board_tasks_do_not(self):
        self.assertRaises(BoardError, self.store.move, "F4-01", "done")
        task = self.store.create({"phase": "F4", "title": "Manual"})
        self.store.move(task["id"], "done")
        self.assertNotIn(task["id"], self.backlog()["priority"])

    def test_delete_only_board_tasks(self):
        task = self.store.create({"phase": "F4", "title": "Borrar"})
        self.store.delete(task["id"])
        self.assertFalse((self.tmp / "tareas" / f"{task['id']}.json").exists())
        self.assertRaises(BoardError, self.store.delete, "F4-01")


if __name__ == "__main__":
    unittest.main()
