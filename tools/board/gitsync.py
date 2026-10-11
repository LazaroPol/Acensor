"""Sincronización del backlog con GitHub: traer cambios y publicar los del tablero.

Solo se añaden ficheros de docs/backlog, así nunca se sube trabajo a medias de otras carpetas.
"""
import subprocess

PATHS = ["docs/backlog"]


class GitSync:
    def __init__(self, root):
        self.root = root

    def _git(self, *args, check=True) -> str:
        res = subprocess.run(["git", *args], cwd=self.root, capture_output=True, text=True)
        if check and res.returncode != 0:
            raise RuntimeError((res.stderr or res.stdout).strip() or f"git {args[0]} falló")
        return res.stdout.strip()

    def status(self) -> dict:
        branch = self._git("rev-parse", "--abbrev-ref", "HEAD")
        changes = self._git("status", "--porcelain", "--", *PATHS).splitlines()
        return {"branch": branch, "changes": len(changes), "main": branch == "main"}

    def pull(self) -> str:
        self._git("pull", "--rebase", "--autostash", "origin", self.status()["branch"])
        return "Cambios traídos de GitHub"

    def publish(self, message: str = "") -> str:
        st = self.status()
        if st["changes"]:
            self._git("add", "--", *PATHS)
            self._git("commit", "-m", message or "chore(tablero): actualizar backlog y prioridades")
        try:
            self._git("pull", "--rebase", "--autostash", "origin", st["branch"])
        except RuntimeError as e:
            self._git("rebase", "--abort", check=False)
            raise RuntimeError(f"Conflicto al traer cambios; resuélvelo a mano: {e}") from None
        self._git("push", "origin", st["branch"])
        return f"Subido a origin/{st['branch']}"
