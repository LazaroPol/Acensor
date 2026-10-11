# Sesión 41 — F6-02 Enemigos con escalado por profundidad

- **Fecha:** 2026-10-11 · **PR:** [#56](https://github.com/MenaDeFerro/Project-Especimen/pull/56) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Combat.Enemies`: arquetipos, profundidad (piso + anillo), escalado lineal y fábrica de enemigos.
- El grafo de módulos se genera en `docs/arquitectura/GRAFO.md`.
## Decisiones
- Escalado lineal por defecto: +12 % vida, +8 % daño, +5 % armadura por piso; +15 % vida/daño por anillo.
- La armadura no escala por anillo (los anillos profundos pegan y aguantan más, sin volverse inmunes).
## Calidad
- 16 tests NUnit nuevos (760 en total) en verde.
## Siguiente sesión
**F6-03** Percepción de IA (visión/oído) y modificadores de Atrapado.
