# Sesión 10 — F2-01 Atributos y modificadores

- **Fecha:** 2026-10-11 · **PR:** [#10](https://github.com/LazaroPol/Acensor/pull/10) · **Estado:** ✅ cerrada

## Qué se hizo
- Sistema de atributos en `Core/Stats`: `Stat`, `StatSheet`, `StatModifier`, `IStatFormula`.
## Decisiones
- Tres tipos de modificador: Flat (suma), PercentAdd (porcentajes sumados entre sí) y Multiplier (producto).
- Fórmula como estrategia inyectable; resultado nunca negativo; valor cacheado hasta que cambia algo.
- `Source` en cada modificador para retirar en bloque los de un objeto/buff.
## Calidad
- 19 tests NUnit nuevos (154 en total) en verde.
## Siguiente sesión
**F2-02** Recursos: vida, recurso de clase, stamina.
