# Sesión 97 — AR-02 Personajes del kit de clases

- **Fecha:** 2026-10-12 · **PR:** [#82](https://github.com/MenaDeFerro/Project-Especimen/pull/82) · **Estado:** ✅ cerrada

## Qué se hizo
- Núcleo `Core.Appearance`: `CharacterAppearance`, `ClassAppearances` (6 kits) y `AppearanceModels` (modelo base, atuendo, textura de variante).
- Editor: `CharacterAssembler` + `SkeletonMerger` acoplan atuendo y peinados al esqueleto Humanoid del modelo base.
- Editor: `CharacterStep` genera `Personaje_<Kit>.prefab` con Animator (Heroe.controller) y `CharacterAnimator`.
- Utilidades `ModelLocator`, `ModelFitting`, `PrefabSaver` y punto de extensión `ICharacterDecorator` (armas en AR-04).
## Decisiones
- Explorador usa el atuendo completo de Ranger (trae cabeza); Campesino va sobre el cuerpo base (puede haber algo de clipping).
- Paleta por variante de textura del pack (Ranger 3, Peasant 2) en vez de recolorear.
## Calidad
- 6 tests NUnit nuevos (957 en total) en verde.
## Siguiente sesión
**AR-04** Armas y equipo por clase.
