# Sesión 4 — F1-02 Temporizador de fase con avisos

- **Fecha:** 2026-10-11 · **PR:** [#3](https://github.com/LazaroPol/Acensor/pull/3) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 12/73 · 16,4 %

## Qué se hizo
- `PhaseTimer`: cuenta atrás reutilizable para Calma e Incursión, con pausa, avisos por umbral y expiración.
- `ShortenTo` permite cerrar antes las puertas sin poder alargar nunca una fase.

## Decisiones
- Eventos C# en el temporizador en lugar de publicar en el bus: el temporizador no conoce el bus (DIP);
  la máquina de estados de F1-03 hará de puente hacia los eventos de dominio.
- Avisos fuera de la duración se ignoran en lugar de lanzar: la validación estricta ya la hace `IterationConfig`.
- Robustez ante ticks grandes: los avisos saltados se emiten en orden y antes de la expiración.

## Calidad
- 12 tests NUnit nuevos (69 en total) y 44 tests Python en verde.

## Siguiente sesión
**F1-03** Máquina de estados de la iteración.
