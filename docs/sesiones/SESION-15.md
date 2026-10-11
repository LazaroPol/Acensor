# Sesión 15 — F2-06 Selección de profesión

- **Fecha:** 2026-10-11 · **PR:** [#15](https://github.com/LazaroPol/Acensor/pull/15) · **Estado:** ✅ cerrada

## Qué se hizo
- Namespace `Specimen.Core.Professions`: `ProfessionId`, `ProfessionBench`, `ProfessionSynergy`, `SynergyCatalog`.
- Las 7 sinergias del GDD §7 como datos por defecto.
## Decisiones
- Profesiones sin stock: varios jugadores pueden repetir profesión (el GDD no lo limita).
- La selección se puede cambiar hasta bloquearla al salir de la Armería (`TryLock` exige que todos hayan elegido).
- Sinergias inyectadas vía `ISynergyCatalog` para poder ampliarlas sin tocar el banco.
## Calidad
- 14 tests NUnit nuevos (224 en total) en verde.
## Siguiente sesión
**F2-07** Muerte, Espectro y revivir.
