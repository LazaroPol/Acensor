# Sesión 6 — F1-04 Sistema de "Listos" y bonus Prisa

- **Fecha:** 2026-10-11 · **PR:** [#5](https://github.com/LazaroPol/Acensor/pull/5) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 14/73 · 19,2 %

## Qué se hizo
- Los jugadores marcan "Listo" durante la Calma; con todo el grupo listo, las puertas se abren antes.
- El tiempo ahorrado se convierte en bonus Prisa para el loot del piso (GDD §3.1), registrado por piso.

## Decisiones
- La fórmula del bonus es una estrategia inyectable (`IHasteBonusPolicy`): se podrá balancear en F12 sin tocar la iteración (OCP/DIP).
- Los jugadores se identifican por `int` de momento; el modelo de jugador llegará en F2.
- Marcar listo fuera de la Calma lanza error: evita estados imposibles en red más adelante.

## Calidad
- 17 tests NUnit nuevos (95 en total) y 44 tests Python en verde.

## Siguiente sesión
**F1-05** Voto de cierre anticipado de puertas.
