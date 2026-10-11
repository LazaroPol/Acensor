# Sesión 26 — PG-04 HUD de depuración: vida, recursos y temporizador del núcleo

- **Fecha:** 2026-10-11 · **PR:** [#34](https://github.com/MenaDeFerro/Project-Especimen/pull/34) · **Estado:** ✅ cerrada

## Qué se hizo
- Presentador puro `DebugHudPresenter` + `HudTime` + `DebugCommands` en Core.Prototype.
- `DebugHud` (IMGUI) en Runtime: pinta el texto y atiende F1 dañar, F2 curar, F3 avanzar 30 s.
- `PrototypeBootstrap` monta el HUD con las Vitals del jugador.
## Decisiones
- IMGUI en vez de uGUI: cero assets/escena para un HUD de depuración; F10 lo sustituirá.
- Avanzar tiempo usa `PhaseTimer.ShortenTo`, así dispara la expiración igual que el tick normal.
- Fase fija «Calma» de 300 s hasta que el bucle de iteración se conecte al prototipo.
## Calidad
- 16 tests NUnit nuevos (407 en total) en verde.
## Siguiente sesión
**PG-05** Guía de prueba manual en Unity y checklist de importación de assets.
