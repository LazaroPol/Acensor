# Sesión 50 — F8-02 Fortificaciones y torretas

- **Fecha:** 2026-10-12 · **PR:** [#67](https://github.com/MenaDeFerro/Project-Especimen/pull/67) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Siege.Defenses`: `Fortification` y `Turret`.
## Decisiones
- La torreta devuelve un `Hit` físico sin crítico; elegir objetivo y aplicar el daño lo hace el combate.
- Una fortificación destruida no se repara (hay que reconstruirla).
## Calidad
- 7 tests NUnit nuevos (831 en total) en verde.
## Siguiente sesión
**(ver `python3 -m tools.progress --next`)**
