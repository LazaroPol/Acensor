# Sesión 5 — F1-03 Máquina de estados de la iteración

- **Fecha:** 2026-10-11 · **PR:** [#4](https://github.com/LazaroPol/Acensor/pull/4) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 13/73 · 17,8 %

## Qué se hizo
- `IterationRun` recorre la partida completa: Armería, Calma e Incursión de cada piso, Ascenso, Asedio en la azotea, Extracción y Fin.
- Estados pequeños (`CalmState`, `IncursionState`, `SignalState`) sobre la máquina genérica de F0-08 y el temporizador de F1-02.
- Eventos `IterationPhaseChanged`, `FloorReached` y `DoorsClosingWarning` para HUD, audio y red.

## Decisiones
- `IterationContext` concentra el estado compartido; los estados solo conocen el contexto (bajo acoplamiento).
- Asedio y Extracción avanzan por señal externa: sus reglas reales llegarán en F8 sin tocar esta máquina (OCP).
- Las transiciones se evalúan en el tick siguiente al que expira el temporizador (un frame de margen, documentado en los tests).

## Calidad
- 9 tests NUnit nuevos (78 en total) y 44 tests Python en verde.

## Siguiente sesión
**F1-04** Sistema de "Listos" y bonus Prisa.
