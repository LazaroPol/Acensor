# Sesión 106 — OF-04 Nivel y XP por oficio con curva configurable e hitos

- **Fecha:** 2026-10-12 · **PR:** [#108](https://github.com/MenaDeFerro/Project-Especimen/pull/108) · **Estado:** ✅ cerrada

## Qué se hizo
- Curva de XP por datos, skill por oficio, hitos y reducción de XP por dificultad de receta.
- `HeroProgression` guarda el nivel de los 7 oficios (aprendidos o no).
## Decisiones
- La reducción de XP vive en `Core.Progression` (`ProfessionXp`) para reutilizar `LevelDifficulty` sin ciclo de dependencias.
- Calidad máxima = 3 + bonus del hito; el hito 20 mantiene tier 4 (las recetas maestras llegan con OF-07).
## Calidad
- 16 tests NUnit nuevos (1162 en total) en verde.
## Siguiente sesión
**OF-05** Calidad del objeto: grados, tirada y huecos de mod.
