# Sesión 16 — F2-07 Muerte, Espectro y revivir

- **Fecha:** 2026-10-11 · **PR:** [#16](https://github.com/LazaroPol/Acensor/pull/16) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core/Life`: `CharacterLife` (vivo ↔ Espectro), `ReviveRules`, `IReviveCost`, `PartyLife`.
## Decisiones
- Litera: solo en Calma, vida 100 % y rellena recurso de clase y stamina; objeto de campo: cualquier fase, 35 % de vida.
- El coste concreto (materiales u objeto) se inyecta con `IReviveCost`; se conectará al inventario en F3.
- Dejar a un Espectro fuera al partir ya lo resuelve `FateResolver` (LeftBehind).
## Calidad
- 10 tests NUnit nuevos (234 en total) en verde.
## Siguiente sesión
**F3-01** Modelo de objetos, rareza y durabilidad.
