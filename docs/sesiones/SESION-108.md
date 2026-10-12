# Sesión 108 — OF-06 RecipeDefinition: estación, niveles, piezas, tiempo, XP y desbloqueo

- **Fecha:** 2026-10-12 · **PR:** [#110](https://github.com/MenaDeFerro/Project-Especimen/pull/110) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Crafting.Definitions`: `StationType`, `UnlockCondition` (+3 estrategias y «Y»), `RecipeOutput`, `RecipeDefinition`, `RecipeCatalog`.
- `RecipeDefinition.FromLegacy` y `RecipeCatalog.FromLegacy` convierten `DefaultRecipes`.
## Decisiones
- `IRecipeKnowledge` (conocimientos y colecciones) es el contrato que implementará `KnownRecipes` en OF-07.
- Con varias mejoras conocidas, `ResolveVariant` sigue la cadena y elige la primera por id; el catálogo rechaza ciclos.
- `SynergyRecipe` no se convierte todavía (no lo pide la tarjeta).
## Calidad
- 16 tests NUnit nuevos (1199 en total) en verde.
## Siguiente sesión
**OF-07** Conocimiento: pergaminos, planos, tratados y libros de oficio.
