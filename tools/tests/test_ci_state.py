import unittest

from tools.git.ci_state import aggregate


def run(status="completed", conclusion="success"):
    return {"status": status, "conclusion": conclusion}


class CiStateTests(unittest.TestCase):
    def test_no_runs_yet_is_pending(self):
        self.assertEqual(aggregate([]), "pending")

    def test_any_running_is_pending(self):
        self.assertEqual(aggregate([run(), run("in_progress", None)]), "pending")

    def test_all_green_or_skipped_is_success(self):
        self.assertEqual(aggregate([run(), run(conclusion="skipped")]), "success")

    def test_any_failure_blocks(self):
        self.assertEqual(aggregate([run(), run(conclusion="failure")]), "failure")
        self.assertEqual(aggregate([run(conclusion="cancelled")]), "failure")


if __name__ == "__main__":
    unittest.main()
