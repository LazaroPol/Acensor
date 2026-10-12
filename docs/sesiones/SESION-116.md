# Sesión 116 — OF-14 Authoring en Unity: ScriptableObjects, base de datos y validación

- **Fecha:** 2026-10-12 · **PR:** [#118](https://github.com/MenaDeFerro/Project-Especimen/pull/118) · **Estado:** ✅ cerrada

## Qué se hizo
- Datos serializables + mappers en el núcleo (`Core.Crafting.Authoring`), `CraftingCatalog`, `DefaultCraftingData` y validador.
- ScriptableObjects, `CraftingDatabase` y menús de validación/generación en Unity.
## Decisiones
- Los SO guardan clases `[Serializable]` del núcleo: los mappers se prueban sin Unity.
- El desbloqueo se aplana a dos listas (conocimientos y colecciones, todas exigidas).
- Generar no pisa assets existentes: tras la primera vez, los datos mandan.
## Calidad
- 19 tests NUnit nuevos (1393 en total) en verde.
## Siguiente sesión
**OF-15** Estaciones en el Campo de pruebas: ventana, cola y piezas.
