# Sesión 30 — F4-02 Mejora de módulos por niveles

- **Fecha:** 2026-10-11 · **PR:** [#38](https://github.com/MenaDeFerro/Project-Especimen/pull/38) · **Estado:** ✅ cerrada

## Qué se hizo
- Los planos definen costes de mejora por nivel; las ranuras guardan el nivel del módulo.
- `ElevatorPlatform.TryUpgrade` cobra de forma atómica y notifica con `ModuleUpgraded`.
## Decisiones
- Un módulo sin costes de mejora tiene nivel máximo 1 («algunos pueden subir de nivel»).
- La lógica de mejora vive en una clase parcial para respetar el límite de líneas.
## Calidad
- 9 tests NUnit nuevos (445 en total) en verde.
## Siguiente sesión
**F4-03** Hoguera: regeneración y recuperación de fatiga.
