# Sesión 38 — F5-05 Caminos alternativos según profesión

- **Fecha:** 2026-10-11 · **PR:** [#52](https://github.com/MenaDeFerro/Project-Especimen/pull/52) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Floors.Paths`: puertas selladas que abren Manitas (forzar), Químico (disolver) o Escultor (restaurar estatua).
- `AlternativePathPlanner` sella hasta N pasillos redundantes; `FloorPaths.ReachableBy` calcula alcance según el grupo.
## Decisiones
- Las puertas solo sellan pasillos no puente: son atajos y el piso entero es alcanzable sin profesiones (GDD §7).
## Calidad
- 18 tests NUnit nuevos (692 en total) en verde.
## Siguiente sesión
**F5-06** Restos de clones anteriores y Fragmentos de memoria.
