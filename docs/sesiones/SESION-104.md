# Sesión 104 — OF-02 Materiales: tier, peso, etiquetas e icono en ItemDefinition

- **Fecha:** 2026-10-12 · **PR:** [#106](https://github.com/MenaDeFerro/Project-Especimen/pull/106) · **Estado:** ✅ cerrada

## Qué se hizo
- `MaterialTags` `[Flags]` (Madera…Vidrio, bits 0–10) y `ItemDefinition` con `Tier`, `Weight`, `Tags`, `IconKey`, `HasTag`.
- Tier, peso y etiquetas para los materiales del catálogo; `Inventory.TotalWeight`.
## Decisiones
- `IconKey` vacío → usa el `Id`; `HasTag(None)` devuelve false; `HasTag` con varias etiquetas exige todas.
- Madera y Núcleo también son `Combustible`; Cristal = Mineral|Vidrio (T3); Reliquia = Hueso|Mineral (T4).
## Calidad
- 8 tests NUnit nuevos (1124 en total) en verde.
## Siguiente sesión
**OF-03** Ingredientes por objeto o etiqueta y consumo por etiqueta.
