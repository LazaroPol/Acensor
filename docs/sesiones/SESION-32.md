# Sesión 32 — F4-04 Cocina: buffs de comida temporales

- **Fecha:** 2026-10-11 · **PR:** [#43](https://github.com/MenaDeFerro/Project-Especimen/pull/43) · **Estado:** ✅ cerrada

## Qué se hizo
- `Meal`, `KitchenConfig` y `FoodBuffs` en `Core.Elevator`: buffs temporales de atributo al comer en la Cocina.
## Decisiones
- El propio `Meal` es la fuente del modificador: repetir plato renueva la duración sin apilar; platos distintos se acumulan.
- Duración = base × (1 + 0,5 × (nivel − 1)); sin Cocina (nivel 0) no se puede comer.
## Calidad
- 8 tests NUnit nuevos (461 en total) en verde.
## Siguiente sesión
**F4-05** Litera: revivir Espectros durante la Calma.
