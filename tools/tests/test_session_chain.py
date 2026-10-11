"""La cadena de sesiones automáticas: siguiente tarea, pausa y documentación del runbook."""
import os
import subprocess
import tempfile
import unittest

from tools.tests.helpers import ROOT

SCRIPT = ROOT / "tools/session/should_continue.sh"
PAUSE = ROOT / "docs/sesiones/PAUSA"


def git(*args, cwd):
    subprocess.run(["git", *args], cwd=cwd, check=True, capture_output=True)


def run(remote=None):
    """Ejecuta el guion contra un remoto local y vacío (o el dado), sin depender de GitHub."""
    with tempfile.TemporaryDirectory() as empty:
        git("init", "-q", "--bare", empty, cwd=empty)
        env = dict(os.environ, SPECIMEN_REMOTE=remote or empty)
        env.pop("SPECIMEN_IGNORE_BRANCH", None)
        return subprocess.run([str(SCRIPT)], capture_output=True, text=True, cwd=ROOT, env=env)


def remote_with_branch(tmp, branch):
    """Remoto local con una rama: simula otra sesión que ya reclamó la tarea."""
    work = os.path.join(tmp, "work")
    git("init", "-q", "-b", branch, work, cwd=tmp)
    git("-c", "user.name=t", "-c", "user.email=t@t", "commit", "-q", "--allow-empty", "-m", "x", cwd=work)
    return work


class SessionChainTests(unittest.TestCase):
    def test_prints_next_task_session_and_phase(self):
        result = run()
        self.assertEqual(result.returncode, 0, result.stderr)
        task, session, phase = result.stdout.split()
        self.assertTrue(task.startswith(phase + "-"))
        self.assertGreater(int(session), 1)

    def test_claimed_task_stops_a_duplicate_chain(self):
        task = run().stdout.split()[0]
        with tempfile.TemporaryDirectory() as tmp:
            result = run(remote_with_branch(tmp, f"feature/{task}-otra-sesion"))
        self.assertEqual(result.returncode, 1)
        self.assertIn(task, result.stderr)

    def test_pause_file_stops_the_chain(self):
        self.assertFalse(PAUSE.exists(), "No se debe commitear la pausa en una PR de tarea")
        PAUSE.write_text("pausa\n")
        try:
            self.assertEqual(run().returncode, 1)
        finally:
            PAUSE.unlink()

    def test_next_prompt_points_to_runbook_and_token_rules(self):
        text = (ROOT / "docs/sesiones/NEXT_PROMPT.md").read_text(encoding="utf-8")
        for needle in ("MenaDeFerro/Project-Especimen", "RUNBOOK.md", "CLAUDE.md", "ship.sh"):
            self.assertIn(needle, text)

    def test_claude_md_has_token_saving_rules(self):
        text = (ROOT / "CLAUDE.md").read_text(encoding="utf-8")
        self.assertIn("Ahorro de tokens", text)
        self.assertIn("una sesión = una tarea", text)


if __name__ == "__main__":
    unittest.main()
