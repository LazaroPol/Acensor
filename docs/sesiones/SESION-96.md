# Sesión 96 — AR-03 Kit de animaciones: locomoción, melé y hechizos

- **Fecha:** 2026-10-12 · **PR:** [#81](https://github.com/MenaDeFerro/Project-Especimen/pull/81) · **Estado:** ✅ cerrada

## Qué se hizo
- Núcleo `Core.Animation`: `AnimSlot`, `AnimParams`, `AnimationSet`, `DefaultAnimationSets` (Héroe y Monstruo) y `LocomotionBlend`.
- Editor: `ClipIndex` (clips de UAL1/2 y Kevin Iglesias) y `AnimatorBuilder` (blend de locomoción + acciones por trigger + muerte).
- Editor: `ArtGenerator` con menú **SPECIMEN › Arte › 2 · Generar prefabs y catálogo** (crea `Heroe.controller` y `Monstruo.controller`).
- Runtime: `CharacterAnimator` (velocidad → blend, ranura → trigger) y `CatalogoArte`.
## Decisiones
- Animaciones Humanoid sin root motion: el movimiento lo sigue mandando el núcleo.
- Si falta un clip, la ranura se omite con aviso y el resto del controlador sigue funcionando.
## Calidad
- 16 tests NUnit nuevos (951 en total) en verde.
## Siguiente sesión
**AR-02** Personajes del kit de clases.
