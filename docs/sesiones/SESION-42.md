# Sesión 42 — F6-03 Percepción de IA (visión/oído) y modificadores de Atrapado

- **Fecha:** 2026-10-11 · **PR:** [#57](https://github.com/MenaDeFerro/Project-Especimen/pull/57) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Combat.Perception`: visión en cono, oído por ruido y persecución.
- Modificadores de Atrapado aplicados vía estrategia inyectable (`TrappedPerceptionModifiers`).
## Decisiones
- Ver tiene prioridad sobre oír; el ruido del objetivo (0-1) escala el alcance auditivo.
- Atrapado no altera el oído (el GDD solo pide visión y velocidad).
## Calidad
- 8 tests NUnit nuevos (768 en total) en verde.
## Siguiente sesión
**F6-04** Habilidades de clase y rasgos de movilidad.
