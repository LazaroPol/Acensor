<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Stations`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Stations`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Stations`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md)
**Lo usan:** [Core.Crafting.Authoring](Core.Crafting.Authoring.md), [Core.Crafting.Content](Core.Crafting.Content.md), [Core.Crafting.Queue](Core.Crafting.Queue.md), [Runtime.Crafting.Authoring](Runtime.Crafting.Authoring.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DefaultStationParts.Sections.cs` | class `DefaultStationParts` | Tabla de piezas de §17.5 por estación (bonus: calidad, fracción de tiempo y de combustible ahorrada). |
| `DefaultStationParts.cs` | class `DefaultStationParts` | Las 19 piezas de estación de §17.5. |
| `PartPlaceResult.cs` | enum `PartPlaceResult` | Resultado de colocar una pieza en una estación. |
| `PartSlots.cs` | class `PartSlots` | Ranuras de pieza de una estación: solo admite piezas compatibles y sin repetir. |
| `RecipeAvailability.cs` | class `RecipeAvailability` | Qué recetas ofrece una estación (GDD §17.3). |
| `RecipeBlockReason.cs` | enum `RecipeBlockReason` | Por qué una receta visible en la estación no se puede fabricar (la UI lo traduce). |
| `RecipeOption.cs` | class `RecipeOption` | Receta que ofrece una estación y, si está en gris, el motivo. |
| `StationPart.cs` | class `StationPart` | Pieza física colocable: se gasta 1 punto por uso; rota deja de contar hasta repararla. |
| `StationPartDefinition.cs` | class `StationPartDefinition` | Pieza de estación (GDD §17.3): crisol, fuelle, olla de hierro… Su es el que citan las RequiredParts de las recetas. |
| `StationPartEntry.cs` | class `StationPartEntry` | Ficha de contenido de una pieza de estación (GDD §17.5): su definición, el nombre visible, qué recetas habilita (texto para la UI) y el obje |
| `StationUpgrade.cs` | class `StationUpgrade` | Mejora de estación por datos (GDD §17.3): sube una estación al (2-3) gastando ; fija el tamaño de cola y los puntos de calidad del nuevo niv |
| `UpgradeResult.cs` | enum `UpgradeResult` | Resultado de intentar mejorar una estación. |
| `Workstation.Upgrade.cs` | class `Workstation` | Mejora de nivel de la : atómica (o gasta todo o nada). |
| `Workstation.cs` | class `Workstation` | Estación de trabajo (GDD §17.3): tipo + nivel (1-3) + ranuras de entrada, salida, combustible y piezas. |
