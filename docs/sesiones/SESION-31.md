# Sesión 31 — F4-03 Hoguera: regeneración y recuperación de fatiga

- **Fecha:** 2026-10-11 · **PR:** [#39](https://github.com/MenaDeFerro/Project-Especimen/pull/39) · **Estado:** ✅ cerrada

## Qué se hizo
- `BonfireRest` (ITickable) regenera vida/stamina y recupera fatiga junto a la Hoguera.
- La plataforma expone el nivel de cada módulo vía `IModuleLevels`.
## Decisiones
- El efecto escala linealmente con el nivel; sin Hoguera (nivel 0) no hace nada.
- El descanso sin fatiga no se acumula para recuperar puntos después.
## Calidad
- 8 tests NUnit nuevos (453 en total) en verde.
## Siguiente sesión
**F4-04** Cocina: buffs de comida temporales.
