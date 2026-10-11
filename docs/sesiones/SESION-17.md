# Sesión 17 — F3-01 Modelo de objetos, rareza y durabilidad

- **Fecha:** 2026-10-11 · **PR:** [#18](https://github.com/LazaroPol/Acensor/pull/18) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core/Items`: rareza, categoría, definición de objeto, durabilidad e instancia.
## Decisiones
- La rareza escala la durabilidad máxima (Común ×1, Poco común ×1,25, Raro ×1,5, Épico ×2, Legendario ×3).
- Durabilidad base 0 = indestructible; los objetos degradables tienen pila 1.
- Un objeto roto se conserva (reparable, F3-06) pero no es usable.
## Calidad
- 23 tests NUnit nuevos (257 en total) en verde.
## Siguiente sesión
**F3-02** Inventario con capacidad y apilado.
