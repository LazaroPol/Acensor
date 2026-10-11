# Sesión 14 — F2-05 Armería con stock limitado por tipo

- **Fecha:** 2026-10-11 · **PR:** [#14](https://github.com/LazaroPol/Acensor/pull/14) · **Estado:** ✅ cerrada

## Qué se hizo
- `ArmoryStock<T>` genérico: stock por tipo por servidor/iteración, devolución y reposición.
- `ClassKitArmory`: reparto de kits de clase con límite de duplicados.
## Decisiones
- Stock genérico (`TItem`) para reutilizarlo con armas/objetos; tipos no definidos = sin stock.
- Un jugador debe liberar su kit antes de coger otro; límite de duplicados global y configurable.
## Calidad
- 13 tests NUnit nuevos (210 en total) en verde.
## Siguiente sesión
**F2-06** Selección de profesión.
