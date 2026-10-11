<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Visuals`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Visuals`
Tests: `Assets/_Project/Tests/EditMode/Core/Visuals`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Prototype](Core.Prototype.md), [Runtime.Prototype](Runtime.Prototype.md), [Runtime.Visuals](Runtime.Visuals.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DefaultGreyboxStyles.cs` | class `DefaultGreyboxStyles` | Estilos greybox por defecto (GDD 16): personajes en cápsula, entorno en cubos de 1 m, colores planos distinguibles para leer la escena sin a |
| `GreyboxColor.cs` | struct `GreyboxColor` | Color RGB con componentes en [0, 1], sin depender de UnityEngine. |
| `GreyboxOffset.cs` | class `GreyboxOffset` | Dónde colocar el centro de la primitiva greybox (pivote centrado) para que respete el pivote del asset real: así cubos y prefabs ocupan el m |
| `GreyboxPrimitive.cs` | enum `GreyboxPrimitive` | Primitiva con la que se dibuja un ID sin asset asignado. |
| `GreyboxStyle.cs` | class `GreyboxStyle` | Cómo se dibuja un ID en greybox: primitiva, color y escala por defecto. |
| `IGreyboxDefaults.cs` | interface `IGreyboxDefaults` | Estrategia que da el estilo greybox de un ID lógico. |
| `IVisualCatalog.cs` | interface `IVisualCatalog` | Resuelve un ID lógico a su asset asignado o a su fallback greybox. |
| `VisualAnchor.cs` | enum `VisualAnchor` | Dónde está el pivote de un visual (docs/arte/ASSETS.md). |
| `VisualAnchors.cs` | class `VisualAnchors` | Pivote de cada ID lógico: el entorno modular va por esquina, el resto por la base. |
| `VisualCatalog.cs` | class `VisualCatalog` | Catálogo ID → asset. |
| `VisualId.cs` | enum `VisualId` | IDs lógicos de todo lo visible. |
| `VisualOffset.cs` | struct `VisualOffset` | Desplazamiento local en metros, sin depender de UnityEngine. |
| `VisualResolution.cs` | class `VisualResolution` | Resultado de resolver un ID: el asset asignado o el fallback greybox. |
| `VisualScale.cs` | struct `VisualScale` | Escala en metros (1 unidad = 1 m) con componentes estrictamente positivas. |
