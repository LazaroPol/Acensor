"""El índice de arquitectura se genera del código y debe estar al día (como PROGRESO.md)."""
import pathlib
import tempfile
import unittest

from tools.arch import __main__ as cli, render, scan
from tools.tests.helpers import ROOT

SAMPLE_A = """using System;
using Specimen.Core.B;

namespace Specimen.Core.A
{
    /// <summary>
    /// Hace cosas. Segunda frase.
    /// </summary>
    [Serializable]
    public sealed partial class Thing { }

    internal enum Kind { X }
}
"""
SAMPLE_B = "namespace Specimen.Core.B\n{\n    public interface IDep { }\n}\n"


def build(tmp: pathlib.Path) -> dict:
    core = tmp / "Core"
    core.mkdir()
    (core / "A.cs").write_text(SAMPLE_A, encoding="utf-8")
    (core / "B.cs").write_text(SAMPLE_B, encoding="utf-8")
    (tmp / "Runtime").mkdir()
    return scan.scan_all(tmp)


class ScanTests(unittest.TestCase):
    def test_types_summary_and_dependencies(self):
        with tempfile.TemporaryDirectory() as d:
            modules = build(pathlib.Path(d))
        a = modules["Specimen.Core.A"]
        self.assertEqual([(t["kind"], t["name"]) for f in a["files"] for t in f["types"]],
                         [("class", "Thing"), ("enum", "Kind")])
        self.assertEqual(a["files"][0]["types"][0]["summary"], "Hace cosas.")
        self.assertEqual(a["deps"], {"Specimen.Core.B"})
        self.assertEqual(modules["Specimen.Core.B"]["deps"], set())

    def test_index_lists_modules_and_edges(self):
        with tempfile.TemporaryDirectory() as d:
            modules = build(pathlib.Path(d))
        text = render.render_index(modules)
        self.assertIn("[Core.A](modulos/Core.A.md) | 1 | `Thing`, `Kind` | Core.B", text)
        self.assertIn("n0 --> n1", text)
        self.assertEqual(sorted(render.outputs(modules)),
                         ["MODULOS.md", "modulos/Core.A.md", "modulos/Core.B.md"])


class RepoIndexTests(unittest.TestCase):
    def test_generated_docs_are_up_to_date(self):
        stale = cli.stale_outputs(render.outputs(scan.scan_all()))
        self.assertEqual(stale, [], "Ejecuta: python3 -m tools.arch")

    def test_every_core_namespace_has_a_page(self):
        for ns in scan.scan_all():
            self.assertTrue((ROOT / "docs/arquitectura/modulos" / render.page_name(ns)).exists(), ns)


if __name__ == "__main__":
    unittest.main()
