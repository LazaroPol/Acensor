# Sesión 19 — F3-03 Tablas de loot ponderadas por anillo de profundidad

- **Fecha:** 2026-10-11 · **PR:** [#20](https://github.com/LazaroPol/Acensor/pull/20) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core/Loot`: `LootTable`, `LootEntry`, `LootDrop`, `DepthRings`, `IRingWeighting`, `RarityRingWeighting`.
## Decisiones
- Peso efectivo = peso base × (1 + 0,5 × nivel de rareza × (anillo − 1)); estrategia inyectable.
- Filas con anillo mínimo: el loot top solo aparece en anillos profundos.
- Tiradas deterministas vía `IRandom` (misma semilla, mismo loot).
## Calidad
- 23 tests NUnit nuevos (294 en total) en verde.
## Siguiente sesión
**F3-04** Recetas y crafting por profesión.
