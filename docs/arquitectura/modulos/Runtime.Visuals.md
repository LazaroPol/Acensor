<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Visuals`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Visuals`
Tests: (sin carpeta propia)

**Depende de:** [Core.Classes](Core.Classes.md), [Core.Professions](Core.Professions.md), [Core.Prototype](Core.Prototype.md), [Core.Visuals](Core.Visuals.md)
**Lo usan:** [Runtime.Atico](Runtime.Atico.md), [Runtime.Hubs](Runtime.Hubs.md), [Runtime.Incursion](Runtime.Incursion.md), [Runtime.Prototype](Runtime.Prototype.md), [Runtime.Sandbox](Runtime.Sandbox.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CatalogoArte.Lookup.cs` | class `CatalogoArte` | Búsquedas del catálogo de arte; devuelven null si no hay prefab (→ greybox). |
| `CatalogoArte.cs` | class `CatalogoArte` | Prefabs generados desde los packs de arte (SPECIMEN › Arte › Generar): personaje por kit, enemigo por id de bestiario, banco por profesión,  |
| `CatalogoVisual.cs` | class `CatalogoVisual` | Catálogo editable ID → prefab (GDD 16). |
| `GreyboxFactory.cs` | class `GreyboxFactory` | Crea la primitiva greybox de un estilo, respetando el pivote del asset real. |
| `VisualSpawner.cs` | class `VisualSpawner` | Instancia visuales por ID lógico: prefab del catálogo o, si falta, greybox. |
