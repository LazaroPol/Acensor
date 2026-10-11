# Sesión 21 — F3-05 Recetas de sinergia entre profesiones

- **Fecha:** 2026-10-11 · **PR:** [#23](https://github.com/LazaroPol/Acensor/pull/23) · **Estado:** ✅ cerrada

## Qué se hizo
- `SynergyRecipe`, `SynergyRecipeBook` y `SynergyCrafter` sobre las sinergias de `Professions`.
- Nuevo resultado `CraftResult.SynergyLocked`.
## Decisiones
- La receta de sinergia reutiliza la validación de `Recipe` por composición.
- La transacción atómica se extrae a `CraftTransaction` (sin duplicar código).
- Cualquiera de las dos profesiones de la sinergia puede fabricar.
## Calidad
- 12 tests NUnit nuevos (318 en total) en verde.
## Siguiente sesión
**F3-06** Reparación de equipo.
