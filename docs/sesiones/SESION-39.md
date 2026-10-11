# Sesión 39 — F5-06 Restos de clones anteriores y Fragmentos de memoria

- **Fecha:** 2026-10-11 · **PR:** [#53](https://github.com/MenaDeFerro/Project-Especimen/pull/53) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Floors.Remnants`: `RemnantPlacer` siembra restos de clones anteriores y Fragmentos de memoria por anillo.
- `MemoryFragmentLog` registra los fragmentos recogidos (base para desempates de Ecos, GDD §9).
## Decisiones
- Solo las salas de saqueo reciben restos/fragmentos; como mucho un fragmento por sala y nunca uno ya recogido.
## Calidad
- 35 tests NUnit nuevos (727 en total) en verde. Cierra la fase F5.
## Siguiente sesión
**F6-01** Modelo de daño, resistencias y críticos.
