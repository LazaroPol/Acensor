"""Verifica que la documentación de diseño mantiene sus secciones clave."""
import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[2]


class GddTests(unittest.TestCase):
    REQUIRED = [
        "Bucle de juego", "Piso 0", "Ascensor-Refugio", "Profesiones", "Pisos",
        "Ecos", "actos", "Lore", "ADN", "Dirección artística", "Arquitectura técnica",
    ]

    def test_gdd_contains_required_sections(self):
        text = (ROOT / "docs/GDD.md").read_text(encoding="utf-8")
        headings = "\n".join(l for l in text.splitlines() if l.startswith("#"))
        for section in self.REQUIRED:
            self.assertIn(section.lower(), headings.lower(), f"Falta la sección '{section}' en el GDD")

    def test_gdd_targets_unity(self):
        text = (ROOT / "docs/GDD.md").read_text(encoding="utf-8")
        self.assertIn("Unity", text)


if __name__ == "__main__":
    unittest.main()
