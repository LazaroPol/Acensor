<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Elevator`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Elevator`
Tests: `Assets/_Project/Tests/EditMode/Core/Elevator`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting](Core.Crafting.md), [Core.Items](Core.Items.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `BlueprintCatalog.cs` | class `BlueprintCatalog` | Catálogo de planos en memoria; como mucho un plano por tipo de módulo. |
| `BuildResult.cs` | enum `BuildResult` | Resultado de un intento de construir un módulo en una ranura. |
| `ElevatorPlatform.cs` | class `ElevatorPlatform` | Plataforma del Ascensor-Refugio (GDD §6): ranuras donde se construyen módulos pagando materiales. |
| `IBlueprintCatalog.cs` | interface `IBlueprintCatalog` | Catálogo de planos de módulo consultable por tipo. |
| `ModuleBlueprint.cs` | class `ModuleBlueprint` | Plano inmutable de un módulo: qué materiales cuesta construirlo. |
| `ModuleSlot.cs` | class `ModuleSlot` | Ranura de la plataforma: vacía o con un módulo construido. |
| `ModuleType.cs` | enum `ModuleType` | Módulos construibles en las ranuras del Ascensor-Refugio (GDD §6). |
