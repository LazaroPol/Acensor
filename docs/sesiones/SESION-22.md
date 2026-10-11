# Sesión 22 — F3-06 Reparación de equipo

- **Fecha:** 2026-10-11 · **PR:** [#24](https://github.com/LazaroPol/Acensor/pull/24) · **Estado:** ✅ cerrada

## Qué se hizo
- `RepairService` repara equipo gastando material del inventario según una `RepairRule` por categoría.
- `RepairCatalog` (una regla por categoría) y `RepairReport` con el resultado detallado.
## Decisiones
- Si falta material se repara en parte con lo que haya (`PartiallyRepaired`); sin nada, no cambia nada.
- Objetos intactos, indestructibles o de categoría sin regla se rechazan sin gastar material.
## Calidad
- 11 tests NUnit nuevos (329 en total) en verde.
## Siguiente sesión
Fase F3 completada; **PG-01** (fase PG).
