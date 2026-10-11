# Sesión 34 — F5-01 Generador procedural de pasillos por anillos (con semilla)

- **Fecha:** 2026-10-11 · **PR:** [#46](https://github.com/MenaDeFerro/Project-Especimen/pull/46) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Floors`: trazado de piso como grafo de salas (anillo 0 = ascensor, 1-3 = profundidad) y pasillos.
- `RingCorridorGenerator` determinista por semilla (`Pcg32Random` inyectable por factoría).
## Decisiones
- Conectividad por construcción: cada sala enlaza con una del anillo anterior; pasillos solo entre anillos iguales o adyacentes.
- Un único Hueco de Emergencia, elegido al azar en el anillo más profundo. Reutiliza `DepthRings` de `Core.Loot`.
## Calidad
- 14 tests NUnit nuevos (572 casos en total) en verde.
## Siguiente sesión
**F5-02** Tipos de piso (patrón Strategy).
