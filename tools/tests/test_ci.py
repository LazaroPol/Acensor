import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[2]
CI = ROOT / ".github/workflows/ci.yml"


class CiTests(unittest.TestCase):
    def test_ci_runs_on_pull_requests_to_main(self):
        text = CI.read_text(encoding="utf-8")
        self.assertIn("pull_request:", text)
        self.assertIn("branches: [main]", text)

    def test_ci_runs_the_same_gate_as_local_prs(self):
        self.assertIn("run: tools/verify.sh", CI.read_text(encoding="utf-8"))

    def test_ci_uses_dotnet_8_and_unity_editmode_runner(self):
        text = CI.read_text(encoding="utf-8")
        self.assertIn('dotnet-version: "8.0.x"', text)
        self.assertIn("testMode: editmode", text)

    def test_scripts_are_executable(self):
        import os
        for script in ("tools/verify.sh", "tools/git/open_pr.sh", "tools/git/merge_pr.sh", "tools/session/ship.sh", "tools/session/should_continue.sh"):
            self.assertTrue(os.access(ROOT / script, os.X_OK), f"{script} no es ejecutable")


if __name__ == "__main__":
    unittest.main()
