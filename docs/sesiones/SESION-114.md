# Sesión 114 — OF-12 Contenido II: recetario medieval por oficio y conocimientos

- **Fecha:** 2026-10-12 · **PR:** [#116](https://github.com/MenaDeFerro/Project-Especimen/pull/116) · **Estado:** ✅ cerrada

## Qué se hizo
- Recetario por defecto de §17.6: 108 recetas T1–T4 en los 7 oficios (`Core.Crafting.Content`).
- Batería de conocimientos: 34 pergaminos/planos/tratados y 7 libros de 5 volúmenes.
- Recetas de sinergia para las 7 sinergias; las 17 recetas antiguas integradas con sus ids.
## Decisiones
- Recetas como datos compactos (`"id*2 #Madera*3@2"`) con `RecipeLadder`; tier → nivel 1/5/10/15 y estación por `RecipeTiers`.
- Los libros desbloquean por colección completa (`libro.*`); el carpintero fabrica piezas de estación con un plano.
## Calidad
- 23 tests NUnit nuevos (1344 en total) en verde.
## Siguiente sesión
**OF-13** Guardado y carga del progreso de oficios e inventario.
