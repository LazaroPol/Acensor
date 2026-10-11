# Sesión 43 — F6-04 Habilidades de clase y rasgos de movilidad

- **Fecha:** 2026-10-11 · **PR:** [#58](https://github.com/MenaDeFerro/Project-Especimen/pull/58) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Abilities`: habilidades con coste (recurso de clase/stamina), enfriamiento y una habilidad de firma por kit.
- Módulo `Core.Abilities.Mobility`: perfil de movilidad por rasgos del kit, saltos aéreos, planeo y teletransporte con alcance.
## Decisiones
- El cobro es atómico (o ambos costes o ninguno); dash y teletransporte se modelan como `AbilityDefinition` para reutilizar enfriamiento y cobro.
- Costes y enfriamientos provisionales, a ajustar en balance.
## Calidad
- 13 tests NUnit nuevos (781 en total) en verde.
## Siguiente sesión
**F6-05** Jefe del Acto I con fases.
