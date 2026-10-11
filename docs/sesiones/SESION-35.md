# Sesión 35 — F5-02 Tipos de piso (patrón Strategy)

- **Fecha:** 2026-10-11 · **PR:** [#47](https://github.com/MenaDeFerro/Project-Especimen/pull/47) · **Estado:** ✅ cerrada

## Qué se hizo
- Estrategias `IFloorType` para los 9 tipos de piso del GDD, en `Core.Floors.Types`.
- `FloorTypeCatalog` y `FloorPlanner` generan un `FloorPlan` (tipo + trazado) con semilla.
## Decisiones
- Cada tipo fija su forma de anillos (Jefe: antesala + arena única; Híbrido: el mayor) y reutiliza `RingCorridorGenerator`; `CreateGenerator` es virtual por si un tipo necesita otro generador.
- Objetivo (`FloorObjective`) y `HasCombat` quedan como datos de la estrategia para F5-03/F5-04.
## Calidad
- 21 tests NUnit nuevos (593 en total) en verde.
## Siguiente sesión
**F5-03** Presagio y modificadores de piso.
