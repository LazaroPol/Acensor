# Sesión 83 — F10-03 HUD: temporizador, recursos, avisos

- **Fecha:** 2026-10-12 · **PR:** [#73](https://github.com/MenaDeFerro/Project-Especimen/pull/73) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Hud` (`GameHudPresenter`, `HudNotices`, `HudBar`, `PhaseLabels`) y `Runtime.Hud.GameHud`.
## Decisiones
- El presenter es C# puro; los avisos nacen de eventos de la iteración y caducan a los 4 s.
## Calidad
- 5 tests NUnit nuevos en verde.
## Siguiente sesión
**(ver `python3 -m tools.progress --next`)**
