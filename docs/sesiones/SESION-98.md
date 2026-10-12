# Sesión 98 — AR-04 Armas y equipo por clase

- **Fecha:** 2026-10-12 · **PR:** [#83](https://github.com/MenaDeFerro/Project-Especimen/pull/83) · **Estado:** ✅ cerrada

## Qué se hizo
- Núcleo `Core.Combat.Attacks`: `AttackProfile`, `ClassAttackProfiles` (3 kits de melé y 3 de hechizo), `ClassAttack` (enfriamiento por velocidad de ataque + coste) y `MeleeArc`.
- Núcleo `Core.Appearance`: `WeaponId`, `WeaponModels` (pack RPG Weapons, longitud por tipo) y `ClassLoadouts`.
- Editor: `WeaponDecorator` + `GripSolver` ponen el arma del kit en la mano derecha del prefab de personaje.
## Decisiones
- El daño sale de la stat Damage del kit × multiplicador; el enfriamiento base se divide por AttackSpeed.
- La empuñadura se calcula con los huesos de la mano (eje meñique → índice); si queda torcida se corrige moviendo «Arma» en el prefab.
## Calidad
- 22 tests NUnit nuevos (979 en total) en verde.
## Siguiente sesión
**AR-05** Bestiario.
