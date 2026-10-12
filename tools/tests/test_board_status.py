"""board_status.sh publica en main el movimiento de la tarjeta y sincroniza la rama de la tarea."""
import json
import os
import shutil
import subprocess
import tempfile
import unittest

from tools.tests.helpers import ROOT

ENV = dict(os.environ, GIT_AUTHOR_NAME="t", GIT_AUTHOR_EMAIL="t@t",
           GIT_COMMITTER_NAME="t", GIT_COMMITTER_EMAIL="t@t")
ENV.pop("SPECIMEN_REMOTE", None)


def sh(*args, cwd):
    return subprocess.run(list(args), cwd=cwd, check=True, capture_output=True, text=True, env=ENV).stdout.strip()


def todo_task(backlog_dir):
    for card in sorted((backlog_dir / "tareas").glob("*.json")):
        if json.loads(card.read_text(encoding="utf-8"))["status"] == "todo":
            return card.stem
    raise AssertionError("No hay tareas «Listo» en el backlog")


class BoardStatusTests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.mkdtemp()
        seed = os.path.join(self.tmp, "seed")
        shutil.copytree(ROOT / "tools", os.path.join(seed, "tools"),
                        ignore=shutil.ignore_patterns("__pycache__"))
        shutil.copytree(ROOT / "docs/backlog", os.path.join(seed, "docs/backlog"))
        shutil.copy(ROOT / ".gitignore", seed)
        sh("git", "init", "-q", "-b", "main", seed, cwd=self.tmp)
        sh("git", "add", "-A", cwd=seed)
        sh("git", "commit", "-q", "-m", "seed", cwd=seed)
        self.remote = os.path.join(self.tmp, "remote.git")
        sh("git", "clone", "-q", "--bare", seed, self.remote, cwd=self.tmp)
        self.work = os.path.join(self.tmp, "work")
        sh("git", "clone", "-q", self.remote, self.work, cwd=self.tmp)
        self.task = todo_task(ROOT / "docs/backlog")

    def tearDown(self):
        shutil.rmtree(self.tmp)

    def move(self, status):
        sh("tools/session/board_status.sh", self.task, status, cwd=self.work)

    def status_on(self, rev):
        text = sh("git", "show", f"{rev}:docs/backlog/tareas/{self.task}.json", cwd=self.remote)
        return json.loads(text)["status"]

    def test_publishes_in_progress_on_main_and_fast_forwards(self):
        self.move("in_progress")
        self.assertEqual(self.status_on("main"), "in_progress")
        self.assertEqual(sh("git", "rev-parse", "HEAD", cwd=self.work), sh("git", "rev-parse", "main", cwd=self.remote))
        self.assertEqual(sh("git", "status", "--porcelain", cwd=self.work), "")

    def test_review_from_task_branch_reaches_main_and_the_branch(self):
        self.move("in_progress")
        sh("git", "checkout", "-q", "-b", f"feature/{self.task}-x", cwd=self.work)
        with open(os.path.join(self.work, "nota.txt"), "w") as f:
            f.write("trabajo\n")
        sh("git", "add", "-A", cwd=self.work)
        sh("git", "commit", "-q", "-m", "wip", cwd=self.work)
        self.move("review")
        self.assertEqual(self.status_on("main"), "review")
        card = json.loads((open(os.path.join(self.work, f"docs/backlog/tareas/{self.task}.json"))).read())
        self.assertEqual(card["status"], "review")
        self.assertTrue(os.path.exists(os.path.join(self.work, "nota.txt")))
        self.assertNotIn("nota.txt", sh("git", "ls-tree", "-r", "--name-only", "main", cwd=self.remote))

    def test_invalid_status_fails_without_touching_main(self):
        before = sh("git", "rev-parse", "main", cwd=self.remote)
        with self.assertRaises(subprocess.CalledProcessError):
            self.move("inventado")
        self.assertEqual(sh("git", "rev-parse", "main", cwd=self.remote), before)


if __name__ == "__main__":
    unittest.main()
