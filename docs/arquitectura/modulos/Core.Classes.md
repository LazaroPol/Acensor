<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Classes`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Classes`
Tests: `Assets/_Project/Tests/EditMode/Core/Classes`

**Depende de:** [Core.Common](Core.Common.md), [Core.Stats](Core.Stats.md)
**Lo usan:** [Core.Abilities](Core.Abilities.md), [Core.Abilities.Mobility](Core.Abilities.Mobility.md), [Core.Armory](Core.Armory.md), [Core.Composition](Core.Composition.md), [Core.Hubs](Core.Hubs.md), [Runtime.Hubs](Runtime.Hubs.md), [Runtime.Visuals](Runtime.Visuals.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ClassKit.cs` | class `ClassKit` | Definición inmutable de un kit de clase: rol, recurso, rasgos y stats base. |
| `ClassKitCatalog.cs` | class `ClassKitCatalog` | Catálogo en memoria de kits; rechaza ids duplicados. |
| `ClassKitId.cs` | enum `ClassKitId` | Los 6 kits de clase de la Armería (GDD §4). |
| `ClassResourceType.cs` | enum `ClassResourceType` | Sabor del recurso de clase; todos usan el mismo pool ClassResource . |
| `ClassRole.cs` | enum `ClassRole` | Rol de combate de un kit de clase (GDD §4). |
| `DefaultClassKits.cs` | class `DefaultClassKits` | Datos de los 6 kits del GDD §4. |
| `ExplorationTraits.cs` | enum `ExplorationTraits` | Rasgos de exploración que concede un kit (combinables). |
| `IClassKitCatalog.cs` | interface `IClassKitCatalog` | Fuente de definiciones de kits de clase. |
