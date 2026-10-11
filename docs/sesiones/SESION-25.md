# Sesión 25 — PG-03 Jugador y cámara en tercera persona (Input System)

- **Fecha:** 2026-10-11 · **PR:** [#33](https://github.com/MenaDeFerro/Project-Especimen/pull/33) · **Estado:** ✅ cerrada

## Qué se hizo
- `Core.Movement`: velocidad plana relativa al giro de cámara, sprint que consume Aguante y cámara en tercera persona.
- Runtime: `PlayerMotor`, `ThirdPersonCamera`, acciones del Input System por código; el bootstrap monta jugador y cámara.
## Decisiones
- Empezar a esprintar exige un mínimo de Aguante (10); seguir solo exige que quede algo. La regeneración se pausa al esprintar.
- Cámara fija cenital-oblicua (12 m, 55°, giro 0); sin rotación de cámara en el prototipo.
- El grafo de `MODULOS.md` agrupa aristas por origen para respetar el límite de 120 líneas.
## Calidad
- 22 tests NUnit nuevos (391 en total) en verde.
## Siguiente sesión
**PG-04** HUD de depuración: vida, recursos y temporizador del núcleo.
