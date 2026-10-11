"""La cadena de sesiones automáticas: siguiente tarea, pausa y documentación del runbook."""
import subprocess
import unittest

from tools.tests.helpers import ROOT

SCRIPT = ROOT / "tools/session/should_continue.sh"
PAUSE = ROOT / "docs/sesiones/PAUSA"


def run():
    return subprocess.run([str(SCRIPT)], capture_output=True, text=True, cwd=ROOT)


class SessionChainTests(unittest.TestCase):
    def test_prints_next_task_session_and_phase(self):
        result = run()
        self.assertEqual(result.returncode, 0, result.stderr)
        task, session, phase = result.stdout.split()
        self.assertTrue(task.startswith(phase + "-"))
        self.assertGreater(int(session), 1)

    def test_pause_file_stops_the_chain(self):
        self.assertFalse(PAUSE.exists(), "No se debe commitear la pausa en una PR de tarea")
        PAUSE.write_text("pausa\n")
        try:
            self.assertEqual(run().returncode, 1)
        finally:
            PAUSE.unlink()

    def test_next_prompt_points_to_runbook_and_token_rules(self):
        text = (ROOT / "docs/sesiones/NEXT_PROMPT.md").read_text(encoding="utf-8")
        for needle in ("LazaroPol/Acensor", "RUNBOOK.md", "CLAUDE.md", "ship.sh"):
            self.assertIn(needle, text)

    def test_claude_md_has_token_saving_rules(self):
        text = (ROOT / "CLAUDE.md").read_text(encoding="utf-8")
        self.assertIn("Ahorro de tokens", text)
        self.assertIn("una sesión = una tarea", text)


if __name__ == "__main__":
    unittest.main()
