# Sesión 20 — F3-04 Recetas y crafting por profesión

- **Fecha:** 2026-10-11 · **PR:** [#21](https://github.com/LazaroPol/Acensor/pull/21) · **Estado:** ✅ cerrada

## Qué se hizo
- Modelo de recetas (`Recipe`, `RecipeIngredient`) y recetario (`IRecipeBook`, `RecipeBook`).
- `Crafter` con `Check` (sin efectos) y `TryCraft` atómico.
## Decisiones
- Los ingredientes no pueden tener durabilidad: consumir/devolver unidades es siempre equivalente.
- Si el resultado no cabe ni con el hueco liberado por los ingredientes → `NoSpace` y rollback completo.
## Calidad
- 12 tests NUnit nuevos (306 en total) en verde.
## Siguiente sesión
**F3-05** Recetas de sinergia entre profesiones.
