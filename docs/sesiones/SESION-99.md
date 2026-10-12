# Sesión 99 — AR-05 Bestiario

- **Fecha:** 2026-10-12 · **PR:** [#84](https://github.com/MenaDeFerro/Project-Especimen/pull/84) · **Estado:** ✅ cerrada

## Qué se hizo
- Núcleo `Core.Bestiary`: `BestiaryEntry` y `DefaultBestiary` (3 diablillos elementales, 2 puglins y el Guardián del Acto I).
- Núcleo `Core.Combat.Enemies`: `EnemyBrain` (esperar → perseguir → atacar con enfriamiento → soltar) y `EnemyHealth`.
- Editor: `MonsterStep` genera `Enemigo_<id>.prefab` con variante de color, escala y `Monstruo.controller`.
## Decisiones
- Los monstruos del pack vienen sin animaciones pero con el mismo esqueleto: se animan con UAL vía Humanoid (andar/arañar de zombi).
- Las criaturas elementales resisten un 50 % su propio elemento.
## Calidad
- 15 tests NUnit nuevos (994 en total) en verde.
## Siguiente sesión
**AR-06** Entorno modular.
