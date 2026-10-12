<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Art`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Art`
Tests: `Assets/_Project/Tests/EditMode/Core/Art`

**Depende de:** [Core.Common](Core.Common.md), [Core.Composition](Core.Composition.md), [Core.Iteration.Presence](Core.Iteration.Presence.md), [Core.Time](Core.Time.md)
**Lo usan:** [Runtime.Art](Runtime.Art.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ColorMood.cs` | enum `ColorMood` | Ambientes de la dualidad cromática (GDD 13). |
| `MoodColor.cs` | struct `MoodColor` | Color RGB lineal 0..1 independiente de Unity. |
| `MoodProfile.cs` | struct `MoodProfile` | Parámetros de ambiente que un Volume URP y el shader toon aplican: saturación y temperatura (-100..100), exposición (EV), niebla, luz ambien |
| `MoodProfiles.cs` | class `MoodProfiles` | Perfiles de ambiente por defecto (GDD 13): refugio cálido, pisos fríos, atrapado deslumbrante, laboratorio clínico. |
| `MoodSelector.cs` | class `MoodSelector` | Decide el ambiente según dónde está el jugador o qué escena se muestra. |
| `MoodTransition.cs` | class `MoodTransition` | Fundido lineal entre ambientes (el «blend por zona» de los Volumes): al cambiar el objetivo parte de lo que se ve ahora. |
