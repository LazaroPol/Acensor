<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Visuals`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Visuals`
Tests: `Assets/_Project/Tests/EditMode/Core/Visuals`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `DefaultGreyboxStyles.cs` | class `DefaultGreyboxStyles` | Estilos greybox por defecto (GDD 16): personajes en cápsula, entorno en cubos de 1 m, colores planos distinguibles para leer la escena sin a |
| `GreyboxColor.cs` | struct `GreyboxColor` | Color RGB con componentes en [0, 1], sin depender de UnityEngine. |
| `GreyboxPrimitive.cs` | enum `GreyboxPrimitive` | Primitiva con la que se dibuja un ID sin asset asignado. |
| `GreyboxStyle.cs` | class `GreyboxStyle` | Cómo se dibuja un ID en greybox: primitiva, color y escala por defecto. |
| `IGreyboxDefaults.cs` | interface `IGreyboxDefaults` | Estrategia que da el estilo greybox de un ID lógico. |
| `IVisualCatalog.cs` | interface `IVisualCatalog` | Resuelve un ID lógico a su asset asignado o a su fallback greybox. |
| `VisualCatalog.cs` | class `VisualCatalog` | Catálogo ID → asset. |
| `VisualId.cs` | enum `VisualId` | IDs lógicos de todo lo visible. |
| `VisualResolution.cs` | class `VisualResolution` | Resultado de resolver un ID: el asset asignado o el fallback greybox. |
| `VisualScale.cs` | struct `VisualScale` | Escala en metros (1 unidad = 1 m) con componentes estrictamente positivas. |
