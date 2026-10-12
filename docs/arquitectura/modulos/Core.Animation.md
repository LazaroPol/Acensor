<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Animation`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Animation`
Tests: `Assets/_Project/Tests/EditMode/Core/Animation`

**Depende de:** nada
**Lo usan:** [Runtime.Characters](Runtime.Characters.md), [Runtime.Sandbox](Runtime.Sandbox.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `AnimParams.cs` | class `AnimParams` | Nombres de los parámetros del AnimatorController generado (contrato Editor ↔ Runtime). |
| `AnimSlot.cs` | enum `AnimSlot` | Ranuras lógicas de animación: el código pide «AtaqueMele», el conjunto decide qué clip suena. |
| `AnimationSet.cs` | class `AnimationSet` | Conjunto de animación: para cada ranura, nombres de clip candidatos en orden de preferencia (el generador usa el primero que exista en los p |
| `ClipLoopRule.cs` | class `ClipLoopRule` | Qué clips importados se reproducen en bucle: los que el pack marca como «_Loop» y las poses de reposo. |
| `DefaultAnimationSets.cs` | class `DefaultAnimationSets` | Conjuntos por defecto con los clips de Universal Animation Library 1/2 (Quaternius) y, como alternativa, los de Kevin Iglesias (prefijo «Hum |
| `LocomotionBlend.cs` | class `LocomotionBlend` | Convierte la velocidad real (m/s) en el parámetro 0…1 del blend de locomoción: 0 reposo, 1/3 andar, 2/3 trotar, 1 esprintar, interpolando en |
