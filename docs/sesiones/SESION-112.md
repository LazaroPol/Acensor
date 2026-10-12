# Sesión 112 — OF-10 Mods de equipo: definición, instalación y stats efectivos

- **Fecha:** 2026-10-12 · **PR:** [#114](https://github.com/MenaDeFerro/Project-Especimen/pull/114) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Crafting.Mods`: definición de mods, huecos por calidad, instalador atómico y stats efectivos.
- Ejemplos del GDD (Palo de madera + esquirlas/piedra/clavo) cubiertos por tests.
## Decisiones
- `ModdedItem` envuelve `ItemInstance` (no se toca `Core.Items`); el guardado de mods queda para OF-13.
- La calidad escala daños y armadura; velocidad y peso no. Los stats efectivos nunca son negativos.
- Nuevo resultado `ModNotOwned`: instalar consume el mod de la bolsa.
- Compatibilidad por `GearTags` derivadas de categoría/material + extras por id (`DefaultGearTags`).
## Calidad
- 26 tests NUnit nuevos (1281 en total) en verde.
## Siguiente sesión
**OF-11** Contenido I: materiales, combustibles, piezas de estación y mods.
