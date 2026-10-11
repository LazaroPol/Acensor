# Sesión 40 — F6-01 Modelo de daño, resistencias y críticos

- **Fecha:** 2026-10-11 · **PR:** [#54](https://github.com/MenaDeFerro/Project-Especimen/pull/54) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Combat`: golpe (`Hit`), defensas (`DefenseProfile`), resultado (`DamageResult`) y `DamageCalculator`.
- Resistencias por tipo, armadura con rendimientos decrecientes y críticos deterministas.
## Decisiones
- Orden de resolución: crítico → armadura (solo físico) → resistencia; nunca daño negativo.
- Resistencia acotada a [-1, 0.9]; armadura K = 100 (100 de armadura = 50 % de reducción).
## Calidad
- 17 tests NUnit nuevos (744 en total) en verde.
## Siguiente sesión
**F6-02** Enemigos con escalado por profundidad.
