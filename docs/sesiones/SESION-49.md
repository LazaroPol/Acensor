# Sesión 49 — F8-01 Director de oleadas del asedio

- **Fecha:** 2026-10-12 · **PR:** [#64](https://github.com/MenaDeFerro/Project-Especimen/pull/64) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Siege`: `WaveConfig`, `WaveState` y `WaveDirector`.
## Decisiones
- El director no crea enemigos: entrega apariciones (`TrySpawn`) y el juego decide qué arquetipo generar.
- Valores por defecto (5 oleadas, 6 + 3 por oleada, 20 s de pausa) provisionales, a ajustar en balance.
## Calidad
- 9 tests NUnit nuevos (824 en total) en verde.
## Siguiente sesión
**(ver `python3 -m tools.progress --next`)**
