<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Elevator`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Elevator`
Tests: `Assets/_Project/Tests/EditMode/Core/Elevator`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting](Core.Crafting.md), [Core.Items](Core.Items.md), [Core.Iteration](Core.Iteration.md), [Core.Life](Core.Life.md), [Core.Stats](Core.Stats.md), [Core.Time](Core.Time.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Core.Composition](Core.Composition.md), [Core.Hubs](Core.Hubs.md), [Runtime.Hubs](Runtime.Hubs.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `BlueprintCatalog.cs` | class `BlueprintCatalog` | Catálogo de planos en memoria; como mucho un plano por tipo de módulo. |
| `BonfireConfig.cs` | class `BonfireConfig` | Parámetros de la Hoguera por nivel: vida y stamina regeneradas por segundo y segundos de descanso para recuperar un punto de fatiga (a nivel |
| `BonfireRest.cs` | class `BonfireRest` | Descanso de un personaje junto a la Hoguera (GDD §5-§6): regenera vida y stamina y recupera fatiga a un ritmo que escala con el nivel del mó |
| `BuildResult.cs` | enum `BuildResult` | Resultado de un intento de construir un módulo en una ranura. |
| `Bunk.cs` | class `Bunk` | Litera del Ascensor-Refugio (GDD §3.3, §6): revive Espectros durante la Calma cobrando materiales del alijo común. |
| `BunkConfig.cs` | class `BunkConfig` | Parámetros de la Litera (GDD §3.3, §6): materiales que cuesta revivir a un Espectro y cuántas unidades de cada material se rebajan por nivel |
| `ElevatorPlatform.Levels.cs` | class `ElevatorPlatform` |  |
| `ElevatorPlatform.Upgrade.cs` | class `ElevatorPlatform` |  |
| `ElevatorPlatform.cs` | class `ElevatorPlatform` | Plataforma del Ascensor-Refugio (GDD §6): ranuras donde se construyen módulos pagando materiales. |
| `FoodBuffs.cs` | class `FoodBuffs` | Buffs de comida activos de un personaje (GDD §6-§7). |
| `IBlueprintCatalog.cs` | interface `IBlueprintCatalog` | Catálogo de planos de módulo consultable por tipo. |
| `IModuleLevels.cs` | interface `IModuleLevels` | Consulta del nivel de cada módulo del Ascensor-Refugio (0 si no está construido). |
| `KitchenConfig.cs` | class `KitchenConfig` | Parámetros de la Cocina: fracción de duración extra por cada nivel por encima del primero (con 0,5 un plato dura ×1 a nivel 1, ×1,5 a nivel  |
| `Meal.cs` | class `Meal` | Plato cocinable en la Cocina (GDD §6-§7): otorga un modificador temporal a un atributo durante (antes de aplicar el nivel de la Cocina). |
| `ModuleBlueprint.cs` | class `ModuleBlueprint` | Plano inmutable de un módulo: qué materiales cuesta construirlo y, opcionalmente, cuánto cuesta cada mejora de nivel (GDD §6: algunos módulo |
| `ModuleSlot.cs` | class `ModuleSlot` | Ranura de la plataforma: vacía (nivel 0) o con un módulo construido y su nivel. |
| `ModuleType.cs` | enum `ModuleType` | Módulos construibles en las ranuras del Ascensor-Refugio (GDD §6). |
| `UpgradeResult.cs` | enum `UpgradeResult` | Resultado de un intento de mejorar el módulo de una ranura. |
