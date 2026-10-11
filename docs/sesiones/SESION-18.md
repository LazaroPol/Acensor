# Sesión 18 — F3-02 Inventario con capacidad y apilado

- **Fecha:** 2026-10-11 · **PR:** [#19](https://github.com/LazaroPol/Acensor/pull/19) · **Estado:** ✅ cerrada

## Qué se hizo
- `ItemStack`, `IInventory` e `Inventory` en `Core/Items`.
## Decisiones
- Capacidad en ranuras (no peso); cada ranura guarda una pila de hasta `MaxStack`.
- `Add` es parcial: añade lo que cabe y devuelve la cantidad; el resto queda fuera (lo gestiona quien llama).
- `Remove` vacía primero las pilas más recientes; los degradables se quitan por instancia exacta.
- Un objeto degradable es único: añadirlo dos veces o con cantidad > 1 lanza excepción.
## Calidad
- 14 tests NUnit nuevos (271 en total) en verde.
## Siguiente sesión
**F3-03** Tablas de loot ponderadas por anillo de profundidad.
