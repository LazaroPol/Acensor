# Sesión 102 — AR-08 Campo de pruebas jugable

- **Fecha:** 2026-10-12 · **PR:** [#92](https://github.com/MenaDeFerro/Project-Especimen/pull/92) · **Estado:** ✅ cerrada

## Qué se hizo
- Núcleo `Core.Sandbox`: `SandboxLayout` (sala 22×18, bestiario, 7 bancos, decorado), `SignatureEffects` (firma jugable de cada kit) y `SandboxLoot`.
- Runtime `Runtime.Sandbox`: jugador sobre `PlayerController` con cambio de kit, `PlayerCombat` (melé con `MeleeArc` / proyectiles), `SignatureCaster`, `SandboxEnemy` (IA del núcleo + animación), botín, bancos de profesión y HUD.
- Editor: **SPECIMEN › Crear escena Campo de pruebas** y **SPECIMEN › ▶ Probar Campo de pruebas** (genera arte si falta, crea la escena y entra en Play).
- Guía de pruebas en `docs/arte/PACKS.md`; idea AR-09 (arte en la Incursión) en el tablero.
## Decisiones
- Escena aparte de la Incursión (la está construyendo otra sesión) para no pisar su trabajo; AR-09 unificará el arte allí.
- Sin arte generado todo funciona en greybox, así el Campo de pruebas también sirve en CI o en equipos sin los packs.
## Calidad
- 12 tests NUnit nuevos (1027 en total) en verde.
## Siguiente sesión
**F11-01** Integración de Netcode for GameObjects.
