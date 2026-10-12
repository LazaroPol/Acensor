# Sesión 103 — OF-01 Diseño de oficios: GDD §17 y backlog de la fase

- **Fecha:** 2026-10-12 · **PR:** [#105](https://github.com/MenaDeFerro/Project-Especimen/pull/105) · **Estado:** ✅ cerrada

## Qué se hizo
- GDD §17 Oficios: progreso por oficio, calidad, materiales y etiquetas, recetas, estaciones, desbloqueos, mods, contenido, datos y guardado.
- Nueva fase OF (OF-01…OF-16) insertada antes de F11 y al frente de la cola del tablero.
## Decisiones
- Se amplía lo existente (ProfessionId, ItemDefinition, Recipe/Crafter, sinergias); lo antiguo sigue funcionando.
- Núcleo puro con datos; ScriptableObjects solo en Runtime (OF-14) convertidos con mappers.
- Nombres visibles medievales (Alquimista, Maestro de ingenios, Carpintero, Cantero) sin cambiar los ids.
## Calidad
- Sin cambios de código: 1116 tests NUnit y 88 de herramientas en verde.
## Siguiente sesión
**OF-02** Materiales: tier, peso, etiquetas e icono en ItemDefinition.
