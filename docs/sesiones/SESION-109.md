# Sesión 109 — OF-07 Conocimiento: pergaminos, planos, tratados y libros de oficio

- **Fecha:** 2026-10-12 · **PR:** [#111](https://github.com/MenaDeFerro/Project-Especimen/pull/111) · **Estado:** ✅ cerrada

## Qué se hizo
- `KnowledgeDefinition`, `KnownRecipes` (implementa `IRecipeKnowledge`) y `KnowledgeReader` con `ReadResult`.
- Volúmenes de libro registrados por colección; los cinco cumplen `CollectionComplete`.
## Decisiones
- `WrongProfession` = el oficio de la habilidad no coincide; no aprende ni consume (se puede volver a leer después).
- `AlreadyKnown` también si todas las recetas que enseña ya se conocen (salvo volúmenes).
- `Knows(id)` acepta id de conocimiento o de receta aprendida.
## Calidad
- 24 tests NUnit nuevos (1223 en total) en verde.
## Siguiente sesión
**OF-08** Estaciones: nivel mejorable, ranuras y piezas colocables.
