"""Tests de arquitectura: garantizan las reglas de capas del proyecto.

- El núcleo (Specimen.Core) no puede depender de UnityEngine (Humble Object / DIP).
- Los asmdef y el manifest deben ser JSON válido.
- Runtime puede depender de Core, nunca al revés.
"""
import json
import pathlib
import re
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[2]
CORE = ROOT / "Assets/_Project/Scripts/Core"
RUNTIME = ROOT / "Assets/_Project/Scripts/Runtime"
TESTS = ROOT / "Assets/_Project/Tests/EditMode"


def load(path: pathlib.Path) -> dict:
    with path.open(encoding="utf-8") as f:
        return json.load(f)


class ProjectStructureTests(unittest.TestCase):
    def test_unity_project_markers_exist(self):
        self.assertTrue((ROOT / "Packages/manifest.json").is_file())
        self.assertTrue((ROOT / "ProjectSettings/ProjectVersion.txt").is_file())

    def test_manifest_is_valid_and_has_required_packages(self):
        deps = load(ROOT / "Packages/manifest.json")["dependencies"]
        for pkg in ("com.unity.render-pipelines.universal", "com.unity.test-framework", "com.unity.inputsystem"):
            self.assertIn(pkg, deps)

    def test_all_asmdefs_are_valid_json_with_matching_names(self):
        asmdefs = list((ROOT / "Assets").rglob("*.asmdef"))
        self.assertGreaterEqual(len(asmdefs), 3)
        for a in asmdefs:
            data = load(a)
            self.assertEqual(data["name"], a.stem, f"{a} name mismatch")


class LayeringTests(unittest.TestCase):
    def test_core_asmdef_has_no_engine_references(self):
        data = load(CORE / "Specimen.Core.asmdef")
        self.assertTrue(data["noEngineReferences"])
        self.assertEqual(data["references"], [])

    def test_core_sources_do_not_use_unity(self):
        forbidden = re.compile(r"^\s*using\s+Unity(Engine|Editor)\b", re.M)
        for cs in CORE.rglob("*.cs"):
            self.assertIsNone(forbidden.search(cs.read_text(encoding="utf-8")), f"{cs} usa UnityEngine")

    def test_core_does_not_depend_on_runtime(self):
        for cs in CORE.rglob("*.cs"):
            self.assertNotIn("Specimen.Runtime", cs.read_text(encoding="utf-8"), f"{cs} depende de Runtime")

    def test_runtime_references_core(self):
        self.assertIn("Specimen.Core", load(RUNTIME / "Specimen.Runtime.asmdef")["references"])

    def test_tests_asmdef_is_editor_only(self):
        data = load(TESTS / "Specimen.Tests.EditMode.asmdef")
        self.assertEqual(data["includePlatforms"], ["Editor"])
        self.assertIn("Specimen.Core", data["references"])

    def test_every_core_file_declares_specimen_core_namespace(self):
        for cs in CORE.rglob("*.cs"):
            self.assertRegex(cs.read_text(encoding="utf-8"), r"namespace\s+Specimen\.Core", f"{cs}")


if __name__ == "__main__":
    unittest.main()
