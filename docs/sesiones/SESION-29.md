# Sesión 29 — F4-01 Ranuras de módulo y construcción con materiales

- **Fecha:** 2026-10-11 · **PR:** [#37](https://github.com/MenaDeFerro/Project-Especimen/pull/37) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Elevator`: tipos de módulo, planos con coste en materiales, catálogo y plataforma con ranuras.
## Decisiones
- Cada tipo de módulo se construye una sola vez por plataforma (`AlreadyBuilt`).
- Se reutiliza `RecipeIngredient` (Crafting) como unidad de coste; la validación se hace antes de consumir, así un fallo nunca toca el inventario.
- `Reset()` vacía la plataforma al empezar una iteración nueva.
## Calidad
- 15 tests NUnit nuevos (436 en total) en verde.
## Siguiente sesión
**F4-02** Mejora de módulos por niveles.
