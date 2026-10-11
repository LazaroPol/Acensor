<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Índice de módulos (C4 nivel 3: componentes)

Un módulo = un namespace C#. Abre solo la página del módulo que vayas a tocar.

| Módulo | Ficheros | Tipos principales | Depende de |
|---|---|---|---|
| [Core.Armory](modulos/Core.Armory.md) | 3 | `ArmoryStock`, `ClassKitArmory`, `IArmoryStock` | Core.Classes, Core.Common |
| [Core.Classes](modulos/Core.Classes.md) | 8 | `ClassKit`, `ClassKitCatalog`, `ClassKitId`, `ClassResourceType`, `ClassRole` … | Core.Common, Core.Stats |
| [Core.Common](modulos/Core.Common.md) | 1 | `Guard` | — |
| [Core.Crafting](modulos/Core.Crafting.md) | 11 | `CraftResult`, `CraftTransaction`, `Crafter`, `IRecipeBook`, `ISynergyRecipeBook` … | Core.Common, Core.Items, Core.Professions |
| [Core.Elevator](modulos/Core.Elevator.md) | 13 | `BlueprintCatalog`, `BonfireConfig`, `BonfireRest`, `BuildResult`, `ElevatorPlatform` … | Core.Common, Core.Crafting, Core.Items, Core.Time, Core.Vitals |
| [Core.Events](modulos/Core.Events.md) | 2 | `EventBus`, `IEventPublisher`, `IEventSubscriber`, `IEventBus` | Core.Common |
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
  n6["Core.Items"]
  n7["Core.Iteration"]
  n8["Core.Iteration.Ending"]
  n9["Core.Iteration.Presence"]
  n10["Core.Iteration.Readiness"]
  n11["Core.Iteration.States"]
  n12["Core.Iteration.Voting"]
  n13["Core.Life"]
  n14["Core.Loot"]
  n15["Core.Movement"]
  n16["Core.Physics"]
  n17["Core.Professions"]
  n18["Core.Prototype"]
  n19["Core.Randomness"]
  n20["Core.Repair"]
  n21["Core.StateMachines"]
  n22["Core.Stats"]
  n23["Core.Time"]
  n24["Core.Visuals"]
  n25["Core.Vitals"]
  n26["Runtime.Prototype"]
  n27["Runtime.Visuals"]
  n0 --> n1 & n2
  n1 --> n2 & n22
  n3 --> n2 & n6 & n17
  n4 --> n2 & n3 & n6 & n23 & n25
  n5 --> n2
  n6 --> n2
  n7 --> n2 & n5 & n8 & n9 & n10 & n11 & n12 & n21 & n23
  n8 --> n9
  n9 --> n2
  n10 --> n2
  n11 --> n8 & n9 & n10 & n21 & n23
  n12 --> n2
  n13 --> n2 & n7 & n25
  n14 --> n2 & n6 & n19
  n15 --> n2 & n25
  n16 --> n2
  n17 --> n2
  n18 --> n2 & n16 & n23 & n24 & n25
  n19 --> n2
  n20 --> n2 & n6
  n21 --> n2 & n23
  n22 --> n2
  n23 --> n2
  n24 --> n2
  n25 --> n2 & n22 & n23
  n26 --> n15 & n18 & n22 & n23 & n24 & n25 & n27
  n27 --> n18 & n24
```
