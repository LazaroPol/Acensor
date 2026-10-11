# Sesión 11 — F2-02 Recursos: vida, recurso de clase, stamina

- **Fecha:** 2026-10-11 · **PR:** [#11](https://github.com/LazaroPol/Acensor/pull/11) · **Estado:** ✅ cerrada

## Qué se hizo
- Recursos en `Core/Vitals`: `ResourcePool`, `ResourceRegen`, `CharacterVitals`, `ResourceKind`.
## Decisiones
- Namespace `Vitals` (no `Resources`) para no chocar con `UnityEngine.Resources`.
- El máximo sale de un `Stat`: los modificadores (y la futura fatiga) lo cambian solos; bajar recorta, subir no rellena.
- `TrySpend` es todo-o-nada (habilidades/stamina); `Drain` resta lo que pueda (daño).
- Regeneración como `ITickable` aparte, con espera reiniciada en cada gasto.
## Calidad
- 17 tests NUnit nuevos (171 en total) en verde.
## Siguiente sesión
**F2-03** Fatiga entre pisos.
