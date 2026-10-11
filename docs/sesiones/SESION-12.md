# Sesión 12 — F2-03 Fatiga entre pisos

- **Fecha:** 2026-10-11 · **PR:** [#12](https://github.com/LazaroPol/Acensor/pull/12) · **Estado:** ✅ cerrada

## Qué se hizo
- `FatigueConfig` y `Fatigue` en `Core/Vitals`: acumulación por piso (+extra si atrapado), tope y recuperación.
## Decisiones
- Penalización como un único `Multiplier` sobre `MaxStamina` (1 − penalización × puntos), reemplazado en cada cambio; no toca otros modificadores.
- Valores por defecto: 1 punto/piso, +2 si atrapado, −10% por punto, tope 5 (ajustables en balance).
## Calidad
- 9 tests NUnit nuevos (180 en total) en verde.
## Siguiente sesión
**F2-04** Definición de los 6 kits de clase.
