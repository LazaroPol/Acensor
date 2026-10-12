# Sesión 115 — OF-13 Guardado y carga del progreso de oficios e inventario

- **Fecha:** 2026-10-12 · **PR:** [#117](https://github.com/MenaDeFerro/Project-Especimen/pull/117) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Crafting.Save`: instantánea de datos, escritor/lector `oficios v1`, almacén y servicio `CraftingSave`.
## Decisiones
- El guardado es una instantánea de datos planos (ids estables); el mapeo a objetos vivos llega con la integración (OF-15/16).
- Calidad como entero del enum `Quality`; oficios por nombre de `ProfessionId`; orden de líneas = orden de los datos (la cola conserva su orden).
- Texto corrupto → excepción + copia de respaldo; serializar antes de escribir evita pisar un guardado bueno.
## Calidad
- 30 tests NUnit nuevos (1374 en total) en verde.
## Siguiente sesión
**OF-14** Authoring en Unity: ScriptableObjects, base de datos y validación.
