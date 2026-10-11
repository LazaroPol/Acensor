# Sesión 9 — F1-07 Fin de iteración

- **Fecha:** 2026-10-11 · **PR:** [#8](https://github.com/LazaroPol/Acensor/pull/8) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 17/73 · 23,3 % · **Fase F1 completada**

## Qué se hizo
- Tres finales posibles: victoria al completar la extracción, derrota si caen todos los clones y abandono.
- Cada clon recibe un destino (extraído, abandonado en la torre, eliminado o abandono) que se publica en `IterationEnded`.

## Decisiones
- La eliminación es la versión mínima de la muerte; en F2-07 el caído pasará antes por Espectro y revivir.
- El fin anticipado usa una transición "desde cualquier fase" de la máquina genérica: no hay que tocar los demás estados.
- Un atrapado en el último piso puede seguir trepando durante el Asedio; si no llega antes de la extracción, queda en la torre.

## Calidad
- 14 tests NUnit nuevos (135 en total) y 44 tests Python en verde.

## Cierre de la Fase F1 — Bucle de iteración
El núcleo ya simula una partida completa sin Unity: armería, Calma con Listos y Prisa, Incursión con avisos y voto
de cierre, Atrapados y rescate, Asedio, Extracción y final con destinos por jugador.

## Siguiente sesión
**F2-01** Atributos y modificadores (aditivos / multiplicativos).
