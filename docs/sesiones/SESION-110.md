# Sesión 110 — OF-08 Estaciones: nivel mejorable, ranuras y piezas colocables

- **Fecha:** 2026-10-12 · **PR:** [#112](https://github.com/MenaDeFerro/Project-Especimen/pull/112) · **Estado:** ✅ cerrada

## Qué se hizo
- `Workstation` con ranuras de entrada, salida, combustible y piezas; mejora de nivel por `StationUpgrade`.
- Piezas de estación (`StationPartDefinition`, `StationPart`, `PartSlots`) con durabilidad.
- `RecipeAvailability` filtra por tipo, nivel, conocimiento y piezas; marca en gris con `RecipeBlockReason`.
## Decisiones
- Las recetas no conocidas, de nivel de estación mayor o sin pieza no se muestran; en gris solo nivel de oficio y materiales (GDD §17.3).
- Materiales se comprueban sobre un `IInventory` que pasa el llamante (la cola de OF-09 combinará estación + bolsa).
- Una pieza rota no habilita recetas ni da bonus hasta repararla.
## Calidad
- 15 tests NUnit nuevos (1238 en total) en verde.
## Siguiente sesión
**OF-09** Cola de fabricación con temporizador y combustible.
