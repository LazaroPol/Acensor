# Sesión 13 — F2-04 Definición de los 6 kits de clase

- **Fecha:** 2026-10-11 · **PR:** [#13](https://github.com/LazaroPol/Acensor/pull/13) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core/Classes` con `ClassKit`, catálogo (`IClassKitCatalog`/`ClassKitCatalog`) y los 6 kits del GDD.
## Decisiones
- Todos los kits comparten el pool `ClassResource`; `ClassResourceType` solo da el sabor (Furia, Maná…).
- Rasgos de exploración como `[Flags]`; stats base provisionales hasta la fase de balance.
## Calidad
- 17 tests NUnit nuevos (197 en total) en verde.
## Siguiente sesión
**F2-05** Armería con stock limitado por tipo.
