# Sesión 33 — F4-05 Litera: revivir Espectros durante la Calma

- **Fecha:** 2026-10-11 · **PR:** [#45](https://github.com/MenaDeFerro/Project-Especimen/pull/45) · **Estado:** ✅ cerrada

## Qué se hizo
- `Bunk` + `BunkConfig` en Core.Elevator: revivir en la Litera solo en Calma, con Litera construida, pagando materiales.
- Nuevo `ReviveResult.NoBunk`.
## Decisiones
- La Litera es el `IReviveCost` inyectado en `CharacterLife`; el coste de objeto de campo se compone por delegación.
- El coste baja N unidades por nivel por encima del primero, nunca por debajo de 1.
## Calidad
- 8 tests NUnit nuevos (469 en total) en verde.
## Siguiente sesión
**F5-01** Generador procedural de pasillos por anillos (con semilla). Cierra la fase F4.
