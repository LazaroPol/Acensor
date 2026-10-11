# Sesión 37 — F5-04 Distribución de loot y enemigos por anillo

- **Fecha:** 2026-10-11 · **PR:** [#51](https://github.com/MenaDeFerro/Project-Especimen/pull/51) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Floors/Population`: cuotas por anillo, `FloorPopulator`, `FloorPopulation` y `RoomPopulation`.
## Decisiones
- La población se deriva de la semilla del trazado con un `Fork` propio: no altera salas ni pasillos.
- Entrada vacía; el Hueco de Emergencia solo tiene guardianes; `hasCombat = false` elimina enemigos.
- Los enemigos son de momento un recuento + nivel de amenaza (aún no hay módulo de enemigos).
## Calidad
- 41 tests NUnit nuevos (650 en total) en verde.
## Siguiente sesión
**F5-05** Caminos alternativos según profesión.
