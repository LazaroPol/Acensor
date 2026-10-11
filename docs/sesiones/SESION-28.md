# Sesión 28 — PG-06 Sistema de colisiones

- **Fecha:** 2026-10-11 · **PR:** [#36](https://github.com/MenaDeFerro/Project-Especimen/pull/36) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Physics`: capas (Jugador, Muro, Enemigo, Clon, Trigger), matriz simétrica y matriz por defecto.
- `ColliderBox` y `LevelColliders`: colliders de muro generados desde el layout, sin depender del visual.
## Decisiones
- Los jugadores no se bloquean entre sí; los triggers solo detectan jugadores; las puertas no tienen collider.
- Del épico quedan pendientes: movimiento con CharacterController, triggers de zona y tests PlayMode.
## Calidad
- 15 tests NUnit nuevos (422 en total) en verde.
## Siguiente sesión
La que indique `should_continue.sh`.
