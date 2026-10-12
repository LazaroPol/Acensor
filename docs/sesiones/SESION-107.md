# Sesión 107 — OF-05 Calidad del objeto: grados, tirada y huecos de mod

- **Fecha:** 2026-10-12 · **PR:** [#109](https://github.com/MenaDeFerro/Project-Especimen/pull/109) · **Estado:** ✅ cerrada

## Qué se hizo
- `Quality`, `QualityGrade`, `QualityRules`/`QualityWeights` (datos) y `QualityRoller` con `IRandom`.
- `ItemInstance.Quality` y `CreateNew(def, grade)` con durabilidad escalada; `Recipe.QualityCap` (por defecto Legendaria).
## Decisiones
- El tier medio ponderado se multiplica por su peso y se redondea antes de sumar (puntuación entera).
- El tope efectivo es min(calidad máxima del hito + pieza, `QualityCap` de la receta); nunca baja de Tosca.
## Calidad
- 21 tests NUnit nuevos (1183 en total) en verde.
## Siguiente sesión
**OF-06** RecipeDefinition: estación, niveles, piezas, tiempo, XP y desbloqueo.
