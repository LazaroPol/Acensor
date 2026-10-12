# Sesión 105 — OF-03 Ingredientes por objeto o etiqueta y consumo por etiqueta

- **Fecha:** 2026-10-12 · **PR:** [#107](https://github.com/MenaDeFerro/Project-Especimen/pull/107) · **Estado:** ✅ cerrada

## Qué se hizo
- `IngredientSpec` (por objeto / por etiqueta con tier mínimo), `IngredientChoice`, `TagConsumptionPlanner`, `ConsumptionPlan`.
- `CraftTransaction` aplica planes de forma atómica y rechaza planes obsoletos.
## Decisiones
- El orden «pila más pequeña» se evalúa por ítem (unidades totales), ya que el inventario quita por id.
- Un ítem fijado se gasta primero y, si no basta, se completa con el orden por defecto; una elección inválida lanza `ArgumentException`.
## Calidad
- 18 tests NUnit nuevos (1142 en total) en verde.
## Siguiente sesión
**OF-04** Nivel y XP por oficio con curva configurable e hitos.
