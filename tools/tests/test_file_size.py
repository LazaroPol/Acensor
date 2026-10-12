"""Regla de estilo: muchos ficheros pequeños mejor que pocos grandes."""
import unittest

from tools.tests.helpers import ROOT

MAX_LINES = 120
EXTENSIONS = {".cs", ".py", ".md", ".sh", ".json", ".yml", ".asmdef", ".csproj"}
EXCLUDED_DIRS = {"bin", "obj", "Library", "Temp", ".git", "historico"}
GENERATED_FILES = {"packages-lock.json", "GRAFO.md"}  # los escribe Unity / tools.arch


def tracked_text_files():
    for path in ROOT.rglob("*"):
        if path.is_file() and path.suffix in EXTENSIONS and path.name not in GENERATED_FILES \
                and not EXCLUDED_DIRS & set(path.parts):
            yield path


class FileSizeTests(unittest.TestCase):
    def test_no_file_exceeds_line_limit(self):
        too_big = []
        for path in tracked_text_files():
            lines = len(path.read_text(encoding="utf-8").splitlines())
            if lines > MAX_LINES:
                too_big.append(f"{path.relative_to(ROOT)} ({lines})")
        self.assertEqual(too_big, [], f"Divide estos ficheros (> {MAX_LINES} líneas)")


if __name__ == "__main__":
    unittest.main()
