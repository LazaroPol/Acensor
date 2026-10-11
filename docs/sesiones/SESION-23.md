# Sesión 23 — PG-01 Catálogo visual: IDs lógicos y resolución asset/greybox

- **Fecha:** 2026-10-11 · **PR:** [#31](https://github.com/MenaDeFerro/Project-Especimen/pull/31) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Visuals`: IDs lógicos, catálogo ID → asset y fallback greybox por defecto.
- La sesión automática anterior no pudo subir la rama (sin repo asociado); se reaplicó su trabajo en un chat manual.
## Decisiones
- `VisualId` con valores explícitos: es el contrato serializado con los assets de Unity.
- Núcleo genérico en `TAsset` y sin UnityEngine; los estilos greybox se inyectan (`IGreyboxDefaults`).
## Calidad
- 19 tests NUnit nuevos (349 en total) en verde.
## Siguiente sesión
**PG-02** Bootstrap del prototipo: escena Prototipo y nivel greybox desde el catálogo.
