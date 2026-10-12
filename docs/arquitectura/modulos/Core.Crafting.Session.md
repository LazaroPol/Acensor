<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Session`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Session`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Session`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting.Authoring](Core.Crafting.Authoring.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Queue](Core.Crafting.Queue.md), [Core.Crafting.Stations](Core.Crafting.Stations.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md), [Core.Randomness](Core.Randomness.md), [Core.Time](Core.Time.md)
**Lo usan:** [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftCompletion.cs` | class `CraftCompletion` | Aviso de trabajo terminado para el HUD: qué se fabricó y cuánta XP de oficio dio. |
| `CraftingProfile.cs` | class `CraftingProfile` | Lo que el jugador lleva a una estación (GDD §17.3): su bolsa, lo que conoce y su nivel en cada oficio. |
| `DefaultStationUpgrades.cs` | class `DefaultStationUpgrades` | Mejoras por defecto de cualquier estación (tabla orientativa de la Fragua, GDD §17.3): nivel 2 con piedra y metal, cola de 4 y +2 de calidad |
| `IStationUpgrades.cs` | interface `IStationUpgrades` | Fuente de datos de las mejoras de estación: la del nivel indicado o null si no hay. |
| `StationCraftResult.cs` | enum `StationCraftResult` | Resultado de pedir una receta en la ventana de una estación. |
| `StationLabels.cs` | class `StationLabels` | Textos de la ventana de estación (GDD §17 «Nombre visible»): oficios, estaciones y motivos. |
| `StationServices.cs` | class `StationServices` | Datos que una sesión de estación consulta: piezas colocables, combustibles y mejoras. |
| `StationSession.Parts.cs` | class `StationSession` | Piezas de la estación: se colocan desde la bolsa y vuelven a ella conservando su desgaste. |
| `StationSession.Upkeep.cs` | class `StationSession` | Combustible y mejora de nivel de la estación en uso. |
| `StationSession.cs` | class `StationSession` | Una estación en uso (GDD §17.3, §17.7): une la , su y el del jugador. |
| `StationSessionFactory.cs` | class `StationSessionFactory` | Crea estaciones listas para usar a partir del catálogo de oficios y el perfil del jugador. |
