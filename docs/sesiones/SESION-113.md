# Sesión 113 — OF-11 Contenido I: materiales, combustibles, piezas de estación y mods

- **Fecha:** 2026-10-12 · **PR:** [#115](https://github.com/MenaDeFerro/Project-Especimen/pull/115) · **Estado:** ✅ cerrada

## Qué se hizo
- 64 materiales nuevos en `ItemCatalog` (ficheros parciales por grupo de etiquetas), intermedios y 4 combustibles.
- `FuelTable.Default` derivada del catálogo; el aceite de lámpara solo arde en la Mesa de alquimia.
- 19 piezas de estación (`DefaultStationParts`) y 15 mods (`DefaultMods`).
## Decisiones
- Se reutilizan los materiales existentes (madera, piedra, hierro, lingote, chatarra, cuero, carne, cristal); ids nuevos en `snake_case`.
- Piezas con id `pieza.*` (como citan las recetas); `StationPartEntry` guarda nombre, texto «habilita» y objeto.
- `DefaultMods.All` perezoso: los campos están en varios parciales.
## Calidad
- 23 tests NUnit nuevos (1321 en total) en verde.
## Siguiente sesión
**OF-12** Contenido II: recetario medieval por oficio y conocimientos.
