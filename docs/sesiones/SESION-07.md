# Sesión 7 — F1-05 Voto de cierre anticipado de puertas

- **Fecha:** 2026-10-11 · **PR:** [#6](https://github.com/LazaroPol/Acensor/pull/6) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 15/73 · 20,5 %

## Qué se hizo
- Durante la Incursión el grupo puede votar cerrar las puertas antes si presiente una amenaza (GDD §3.1).
- Con mayoría, quedan 15 s de gracia para volver al ascensor; luego el flujo normal (Ascenso, o Atrapado en F1-06).

## Decisiones
- Mayoría estricta: con 2 jugadores hacen falta los 2; con 4, tres. Evita que un solo jugador deje fuera a los demás.
- La votación (`MajorityVote`) está separada de su efecto (`EarlyCloseVote`): F7-01 reutilizará la primera con desempate.
- El margen de gracia vive en `IterationConfig` y se valida contra la duración de la Incursión.

## Calidad
- 14 tests NUnit nuevos (109 en total) y 44 tests Python en verde.

## Siguiente sesión
**F1-06** Estado Atrapado y Hueco de Emergencia.
