<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Índice de módulos (C4 nivel 3: componentes)

Un módulo = un namespace C#. Abre solo la página del módulo que vayas a tocar.

| Módulo | Ficheros | Tipos principales | Depende de |
|---|---|---|---|
| [Core.Armory](modulos/Core.Armory.md) | 3 | `ArmoryStock`, `ClassKitArmory`, `IArmoryStock` | Core.Classes, Core.Common |
| [Core.Classes](modulos/Core.Classes.md) | 8 | `ClassKit`, `ClassKitCatalog`, `ClassKitId`, `ClassResourceType`, `ClassRole` … | Core.Common, Core.Stats |
| [Core.Common](modulos/Core.Common.md) | 1 | `Guard` | — |
| [Core.Crafting](modulos/Core.Crafting.md) | 11 | `CraftResult`, `CraftTransaction`, `Crafter`, `IRecipeBook`, `ISynergyRecipeBook` … | Core.Common, Core.Items, Core.Professions |
| [Core.Elevator](modulos/Core.Elevator.md) | 18 | `BlueprintCatalog`, `BonfireConfig`, `BonfireRest`, `BuildResult`, `Bunk` … | Core.Common, Core.Crafting, Core.Items, Core.Iteration, Core.Life, Core.Stats, Core.Time, Core.Vitals |
| [Core.Events](modulos/Core.Events.md) | 2 | `EventBus`, `IEventPublisher`, `IEventSubscriber`, `IEventBus` | Core.Common |
| [Core.Floors](modulos/Core.Floors.md) | 8 | `Corridor`, `FloorConnectivity`, `FloorLayout`, `FloorRoom`, `IFloorGenerator` … | Core.Common, Core.Loot, Core.Randomness |
| [Core.Floors.Omens](modulos/Core.Floors.Omens.md) | 5 | `FloorModifier`, `FloorModifierCatalog`, `FloorModifierKind`, `Omen`, `OmenRoller` | Core.Common, Core.Floors.Types, Core.Randomness |
| [Core.Floors.Paths](modulos/Core.Floors.Paths.md) | 4 | `AlternativePathPlanner`, `FloorPaths`, `SealMethod`, `SealedDoorRules` | Core.Common, Core.Professions, Core.Randomness |
| [Core.Floors.Population](modulos/Core.Floors.Population.md) | 5 | `FloorPopulation`, `FloorPopulator`, `RingPopulationRules`, `RingQuota`, `RoomPopulation` | Core.Common, Core.Loot, Core.Randomness |
| [Core.Floors.Types](modulos/Core.Floors.Types.md) | 9 | `PuzzleFloor`, `DecisionFloor`, `SanctuaryFloor`, `ProfessionFloor`, `LootFloor` … | Core.Common |
| [Core.Items](modulos/Core.Items.md) | 11 | `Durability`, `IDurability`, `IInventory`, `Inventory`, `ItemCategory` … | Core.Common |
| [Core.Iteration](modulos/Core.Iteration.md) | 9 | `IterationConfig`, `IterationConfigBuilder`, `IterationContext`, `IterationPhaseChanged`, `FloorReached` … | Core.Common, Core.Events, Core.Iteration.Ending, Core.Iteration.Presence, Core.Iteration.Readiness, Core.Iteration.States, Core.Iteration.Voting, Core.StateMachines, Core.Time |
| [Core.Iteration.Ending](modulos/Core.Iteration.Ending.md) | 3 | `FateResolver`, `IterationEnded`, `IterationOutcome`, `PlayerFate` | Core.Iteration.Presence |
| [Core.Iteration.Presence](modulos/Core.Iteration.Presence.md) | 4 | `PlayerLocation`, `PlayersTrapped`, `PlayerRescued`, `PresenceTracker`, `TrappedModifiers` | Core.Common |
| [Core.Iteration.Readiness](modulos/Core.Iteration.Readiness.md) | 4 | `HasteBonusEarned`, `IHasteBonusPolicy`, `LinearHasteBonus`, `ReadyCheck` | Core.Common |
| [Core.Iteration.States](modulos/Core.Iteration.States.md) | 5 | `CalmState`, `EndedState`, `IncursionState`, `SignalState`, `TimedPhaseState` | Core.Iteration.Ending, Core.Iteration.Presence, Core.Iteration.Readiness, Core.StateMachines, Core.Time |
| [Core.Iteration.Voting](modulos/Core.Iteration.Voting.md) | 2 | `EarlyCloseVote`, `EarlyCloseApproved`, `MajorityVote` | Core.Common |
| [Core.Life](modulos/Core.Life.md) | 7 | `CharacterLife`, `IReviveCost`, `LifeState`, `PartyLife`, `ReviveMethod` … | Core.Common, Core.Iteration, Core.Vitals |
| [Core.Loot](modulos/Core.Loot.md) | 6 | `DepthRings`, `IRingWeighting`, `LootDrop`, `LootEntry`, `LootTable` … | Core.Common, Core.Items, Core.Randomness |
| [Core.Movement](modulos/Core.Movement.md) | 6 | `CameraPoint`, `MovementConfig`, `PlanarMover`, `PlanarVector`, `SprintController` … | Core.Common, Core.Vitals |
| [Core.Physics](modulos/Core.Physics.md) | 4 | `ColliderBox`, `CollisionLayer`, `CollisionMatrix`, `DefaultCollisionMatrix` | Core.Common |
| [Core.Professions](modulos/Core.Professions.md) | 6 | `DefaultProfessionSynergies`, `ISynergyCatalog`, `ProfessionBench`, `ProfessionId`, `ProfessionSynergy` … | Core.Common |
| [Core.Prototype](modulos/Core.Prototype.md) | 7 | `DebugCommands`, `DebugHudPresenter`, `GreyboxRoomLayout`, `HudTime`, `ILevelLayout` … | Core.Common, Core.Physics, Core.Time, Core.Visuals, Core.Vitals |
| [Core.Randomness](modulos/Core.Randomness.md) | 2 | `IRandom`, `Pcg32Random` | Core.Common |
| [Core.Repair](modulos/Core.Repair.md) | 6 | `IRepairCatalog`, `RepairCatalog`, `RepairReport`, `RepairResult`, `RepairRule` … | Core.Common, Core.Items |
| [Core.StateMachines](modulos/Core.StateMachines.md) | 3 | `IState`, `StateBase`, `StateMachine`, `Transition` | Core.Common, Core.Time |
| [Core.Stats](modulos/Core.Stats.md) | 7 | `IStatFormula`, `ModifierKind`, `StandardStatFormula`, `Stat`, `StatModifier` … | Core.Common |
| [Core.Time](modulos/Core.Time.md) | 4 | `IClock`, `ITickable`, `ManualClock`, `PhaseTimer` | Core.Common |
| [Core.Visuals](modulos/Core.Visuals.md) | 14 | `DefaultGreyboxStyles`, `GreyboxColor`, `GreyboxOffset`, `GreyboxPrimitive`, `GreyboxStyle` … | Core.Common |
| [Core.Vitals](modulos/Core.Vitals.md) | 6 | `CharacterVitals`, `Fatigue`, `FatigueConfig`, `ResourceKind`, `ResourcePool` … | Core.Common, Core.Stats, Core.Time |
| [Runtime.Prototype](modulos/Runtime.Prototype.md) | 7 | `DebugHud`, `PlayerMotor`, `PrototypeBootstrap`, `PrototypeInput`, `PrototypePlayer` … | Core.Movement, Core.Prototype, Core.Stats, Core.Time, Core.Visuals, Core.Vitals, Runtime.Visuals |
| [Runtime.Visuals](modulos/Runtime.Visuals.md) | 3 | `CatalogoVisual`, `GreyboxFactory`, `VisualSpawner` | Core.Prototype, Core.Visuals |

## Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Armory"]
  n1["Core.Classes"]
  n2["Core.Common"]
  n3["Core.Crafting"]
  n4["Core.Elevator"]
  n5["Core.Events"]
  n6["Core.Floors"]
  n7["Core.Floors.Omens"]
  n8["Core.Floors.Paths"]
  n9["Core.Floors.Population"]
  n10["Core.Floors.Types"]
  n11["Core.Items"]
  n12["Core.Iteration"]
  n13["Core.Iteration.Ending"]
  n14["Core.Iteration.Presence"]
  n15["Core.Iteration.Readiness"]
  n16["Core.Iteration.States"]
  n17["Core.Iteration.Voting"]
  n18["Core.Life"]
  n19["Core.Loot"]
  n20["Core.Movement"]
  n21["Core.Physics"]
  n22["Core.Professions"]
  n23["Core.Prototype"]
  n24["Core.Randomness"]
  n25["Core.Repair"]
  n26["Core.StateMachines"]
  n27["Core.Stats"]
  n28["Core.Time"]
  n29["Core.Visuals"]
  n30["Core.Vitals"]
  n31["Runtime.Prototype"]
  n32["Runtime.Visuals"]
  n0 --> n1 & n2
  n1 --> n2 & n27
  n3 --> n2 & n11 & n22
  n4 --> n2 & n3 & n11 & n12 & n18 & n27 & n28 & n30
  n5 --> n2
  n6 --> n2 & n19 & n24
  n7 --> n2 & n10 & n24
  n8 --> n2 & n22 & n24
  n9 --> n2 & n19 & n24
  n10 --> n2
  n11 --> n2
  n12 --> n2 & n5 & n13 & n14 & n15 & n16 & n17 & n26 & n28
  n13 --> n14
  n14 --> n2
  n15 --> n2
  n16 --> n13 & n14 & n15 & n26 & n28
  n17 --> n2
  n18 --> n2 & n12 & n30
  n19 --> n2 & n11 & n24
  n20 --> n2 & n30
  n21 --> n2
  n22 --> n2
  n23 --> n2 & n21 & n28 & n29 & n30
  n24 --> n2
  n25 --> n2 & n11
  n26 --> n2 & n28
  n27 --> n2
  n28 --> n2
  n29 --> n2
  n30 --> n2 & n27 & n28
  n31 --> n20 & n23 & n27 & n28 & n29 & n30 & n32
  n32 --> n23 & n29
```
