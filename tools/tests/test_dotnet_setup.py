"""La instalación de .NET en cada sesión es silenciosa e idempotente (ahorro de tokens)."""
import os
import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[2]
SETUP = ROOT / "tools/setup_dotnet.sh"


class DotnetSetupTests(unittest.TestCase):
    def test_setup_script_exists_and_is_executable(self):
        self.assertTrue(SETUP.is_file())
        self.assertTrue(os.access(SETUP, os.X_OK))

    def test_setup_is_idempotent_and_sends_detail_to_a_log(self):
        text = SETUP.read_text(encoding="utf-8")
        self.assertIn("command -v dotnet", text)
        self.assertIn('>>"$LOG" 2>&1', text)
        self.assertIn("DOTNET_NOLOGO=1", text)

    def test_runbook_and_verify_use_the_setup_script(self):
        runbook = (ROOT / "docs/sesiones/RUNBOOK.md").read_text(encoding="utf-8")
        self.assertIn("tools/setup_dotnet.sh", runbook)
        self.assertNotIn("apt-get install", runbook)
        verify = (ROOT / "tools/verify.sh").read_text(encoding="utf-8")
        self.assertIn("tools/setup_dotnet.sh", verify)
        self.assertIn("DOTNET_NOLOGO=1", verify)


if __name__ == "__main__":
    unittest.main()
