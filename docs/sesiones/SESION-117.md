# Sesión 117 — OF-15 Estaciones en el Campo de pruebas: ventana, cola y piezas

- **Fecha:** 2026-10-12 · **PR:** [#119](https://github.com/MenaDeFerro/Project-Especimen/pull/119) · **Estado:** ✅ cerrada

## Qué se hizo
- `Core.Crafting.Session`: `StationSession`, `CraftingProfile`, `StationSessionFactory`, `StationServices`, `DefaultStationUpgrades`, `StationLabels`.
- `BenchStation` envuelve una estación completa; `StationWindow` con recetas, cola, ranuras, combustible, piezas y mejora.
## Decisiones
- Los materiales salen de la entrada y, si faltan, de la bolsa; la UI no rellena la entrada a mano.
- Al retirar una pieza vuelve a la bolsa y la estación recuerda su desgaste si se recoloca.
- Mejoras por defecto iguales para todas las estaciones (tabla orientativa de la Fragua) hasta tener datos propios.
- La XP de oficio no suma XP de héroe; se muestra con el color de profesión.
## Calidad
- 12 tests NUnit nuevos (1405 en total) en verde.
## Siguiente sesión
**OF-16** Mods, conocimiento y guardado en el Campo de pruebas + guía de prueba.
