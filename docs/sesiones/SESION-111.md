# Sesión 111 — OF-09 Cola de fabricación con temporizador y combustible

- **Fecha:** 2026-10-12 · **PR:** [#113](https://github.com/MenaDeFerro/Project-Especimen/pull/113) · **Estado:** ✅ cerrada

## Qué se hizo
- `CraftQueue` (nuevo módulo `Core.Crafting.Queue`): encolar/cancelar con reserva de materiales, temporizador, combustible, entrega con calidad y XP.
- `InventoryChain` (entrada + bolsa), `CraftTiming`, `FuelTable`, `CraftJob`.
## Decisiones
- La cola es un objeto aparte que envuelve la `Workstation` (necesita tirador de calidad y tabla de combustible).
- El combustible solo arde mientras avanza un trabajo; con la salida llena no se quema nada y la calidad no se vuelve a tirar.
- `FuelTable.Default` solo tiene la madera (leña); carbón y hulla entran cuando existan sus materiales.
## Calidad
- 17 tests NUnit nuevos (1255 en total) en verde.
## Siguiente sesión
**Siguiente tarea del backlog** (ver `python3 -m tools.progress --next`).
