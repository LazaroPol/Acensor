"""La documentación vive en docs/, dividida en ficheros pequeños y enlazados."""
import re
import unittest

from tools.tests.helpers import ROOT

DOCS = ROOT / "docs"


class DocsTests(unittest.TestCase):
    def test_docs_index_exists_and_links_main_sections(self):
        text = (DOCS / "README.md").read_text(encoding="utf-8")
        for target in ("gdd/README.md", "backlog/PROGRESO.md", "sesiones/README.md", "workflow.md"):
            self.assertIn(target, text)

    def test_all_relative_markdown_links_resolve(self):
        link = re.compile(r"\]\(([^)#:]+\.md)\)")
        for md in DOCS.rglob("*.md"):
            for target in link.findall(md.read_text(encoding="utf-8")):
                self.assertTrue((md.parent / target).resolve().is_file(), f"{md.relative_to(ROOT)} -> {target}")

    def test_gdd_is_split_into_sections_and_targets_unity(self):
        sections = sorted((DOCS / "gdd").glob("[0-9][0-9]-*.md"))
        self.assertGreaterEqual(len(sections), 15)
        self.assertIn("Unity", (DOCS / "gdd/14-arquitectura-tecnica.md").read_text(encoding="utf-8"))


if __name__ == "__main__":
    unittest.main()
