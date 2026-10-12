<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Índice de módulos (C4 nivel 3: componentes)

Un módulo = un namespace C#. Abre solo la página del módulo que vayas a tocar.

| Módulo | Ficheros | Tipos principales | Depende de |
|---|---|---|---|
| [Core.Abilities](modulos/Core.Abilities.md) | 7 | `AbilityCooldown`, `AbilityDefinition`, `AbilityResult`, `AbilitySlot`, `DefaultClassAbilities` … | Core.Classes, Core.Common, Core.Vitals |
| [Core.Abilities.Mobility](modulos/Core.Abilities.Mobility.md) | 4 | `AirJumpCounter`, `MobilityConfig`, `MobilityProfile`, `ShortTeleport` | Core.Classes, Core.Common, Core.Movement |
| [Core.Animation](modulos/Core.Animation.md) | 10 | `AnimParams`, `AnimSlot`, `AnimationSet`, `Candidates`, `ClipLoopRule` … | Core.Appearance, Core.Classes, Core.Combat.Attacks |
| [Core.Appearance](modulos/Core.Appearance.md) | 9 | `AppearanceModels`, `BodyType`, `CharacterAppearance`, `ClassAppearances`, `ClassLoadout` … | Core.Classes, Core.Combat.Attacks, Core.Common |
| [Core.Armory](modulos/Core.Armory.md) | 3 | `ArmoryStock`, `ClassKitArmory`, `IArmoryStock` | Core.Classes, Core.Common |
| [Core.Art](modulos/Core.Art.md) | 6 | `ColorMood`, `MoodColor`, `MoodProfile`, `MoodProfiles`, `MoodSelector` … | Core.Common, Core.Composition, Core.Iteration.Presence, Core.Time |
| [Core.Bestiary](modulos/Core.Bestiary.md) | 3 | `BestiaryEntry`, `DefaultBestiary`, `MonsterModel` | Core.Combat, Core.Combat.Bosses, Core.Combat.Enemies, Core.Common |
| [Core.Classes](modulos/Core.Classes.md) | 8 | `ClassKit`, `ClassKitCatalog`, `ClassKitId`, `ClassResourceType`, `ClassRole` … | Core.Common, Core.Stats |
| [Core.Combat](modulos/Core.Combat.md) | 9 | `ArmorMitigation`, `ArmorRules`, `CritRules`, `DamageCalculator`, `DamageResult` … | Core.Common, Core.Randomness |
| [Core.Combat.Attacks](modulos/Core.Combat.Attacks.md) | 6 | `AttackProfile`, `AttackStartResult`, `AttackStyle`, `ClassAttack`, `ClassAttackProfiles` … | Core.Abilities, Core.Classes, Core.Common, Core.Movement, Core.Vitals |
| [Core.Combat.Bosses](modulos/Core.Combat.Bosses.md) | 3 | `ActOneBoss`, `BossEncounter`, `BossPhase` | Core.Combat.Enemies, Core.Common |
| [Core.Combat.Enemies](modulos/Core.Combat.Enemies.md) | 12 | `DepthScalingConfig`, `Enemy`, `EnemyAction`, `EnemyArchetype`, `EnemyBrain` … | Core.Abilities, Core.Common, Core.Loot |
| [Core.Combat.Perception](modulos/Core.Combat.Perception.md) | 7 | `EnemyPerception`, `IPerceptionModifiers`, `Observer`, `PerceivedTarget`, `PerceptionConfig` … | Core.Iteration.Presence, Core.Movement |
| [Core.Combat.Targeting](modulos/Core.Combat.Targeting.md) | 1 | `TabTargeting` | Core.Common |
| [Core.Common](modulos/Core.Common.md) | 1 | `Guard` | — |
| [Core.Composition](modulos/Core.Composition.md) | 6 | `GameComposition`, `ISceneLoader`, `IServiceRegistry`, `IServiceResolver`, `SceneChanged` … | Core.Armory, Core.Classes, Core.Common, Core.Elevator, Core.Events, Core.Iteration, Core.Loot, Core.Profile |
| [Core.Crafting](modulos/Core.Crafting.md) | 12 | `CraftResult`, `CraftTransaction`, `Crafter`, `DefaultRecipes`, `IRecipeBook` … | Core.Common, Core.Items, Core.Items.Catalog, Core.Professions |
| [Core.Dna](modulos/Core.Dna.md) | 12 | `DefaultDnaEffects`, `DnaAllocation`, `DnaBranch`, `DnaEffect`, `DnaMutationApplier` … | Core.Iteration.Ending, Core.Stats |
| [Core.Echoes](modulos/Core.Echoes.md) | 2 | `EchoDecision`, `EchoRegistry` | Core.Common |
| [Core.Echoes.Consequences](modulos/Core.Echoes.Consequences.md) | 3 | `EchoConsequence`, `EchoConsequenceTable`, `EchoOmenAdjuster` | Core.Common, Core.Floors.Omens |
| [Core.Echoes.Secrets](modulos/Core.Echoes.Secrets.md) | 2 | `SecretDecision`, `SecretEchoLedger` | Core.Common |
| [Core.Echoes.Voting](modulos/Core.Echoes.Voting.md) | 3 | `EchoVote`, `EchoVoteResult`, `IFragmentSource` | Core.Common |
| [Core.Elevator](modulos/Core.Elevator.md) | 18 | `BlueprintCatalog`, `BonfireConfig`, `BonfireRest`, `BuildResult`, `Bunk` … | Core.Common, Core.Crafting, Core.Items, Core.Iteration, Core.Life, Core.Stats, Core.Time, Core.Vitals |
| [Core.Equipment](modulos/Core.Equipment.md) | 9 | `CharacterEquipment`, `EquipSlot`, `GearBonuses`, `GearCatalog`, `GearDelta` … | Core.Classes, Core.Common, Core.Items, Core.Items.Catalog, Core.Randomness, Core.Stats |
| [Core.Events](modulos/Core.Events.md) | 2 | `EventBus`, `IEventPublisher`, `IEventSubscriber`, `IEventBus` | Core.Common |
| [Core.Extraction](modulos/Core.Extraction.md) | 3 | `BloodDrawSequence`, `BloodDrawStep`, `BloodDrawTone` | Core.Common |
| [Core.Floors](modulos/Core.Floors.md) | 9 | `Corridor`, `FloorConnectivity`, `FloorGreyboxLayout`, `FloorLayout`, `FloorRoom` … | Core.Common, Core.Loot, Core.Prototype, Core.Randomness, Core.Visuals |
| [Core.Floors.Omens](modulos/Core.Floors.Omens.md) | 5 | `FloorModifier`, `FloorModifierCatalog`, `FloorModifierKind`, `Omen`, `OmenRoller` | Core.Common, Core.Floors.Types, Core.Randomness |
| [Core.Floors.Paths](modulos/Core.Floors.Paths.md) | 4 | `AlternativePathPlanner`, `FloorPaths`, `SealMethod`, `SealedDoorRules` | Core.Common, Core.Professions, Core.Randomness |
| [Core.Floors.Population](modulos/Core.Floors.Population.md) | 5 | `FloorPopulation`, `FloorPopulator`, `RingPopulationRules`, `RingQuota`, `RoomPopulation` | Core.Common, Core.Loot, Core.Randomness |
| [Core.Floors.Remnants](modulos/Core.Floors.Remnants.md) | 8 | `FloorRemnants`, `FragmentPlacement`, `MemoryFragmentLog`, `Remnant`, `RemnantKind` … | Core.Common, Core.Loot, Core.Randomness |
| [Core.Floors.Types](modulos/Core.Floors.Types.md) | 9 | `PuzzleFloor`, `DecisionFloor`, `SanctuaryFloor`, `ProfessionFloor`, `LootFloor` … | Core.Common |
| [Core.Gathering](modulos/Core.Gathering.md) | 2 | `GatherKind`, `GatheringRules` | Core.Common, Core.Items.Catalog, Core.Loot, Core.Randomness |
| [Core.Hubs](modulos/Core.Hubs.md) | 5 | `ArmoryPresenter`, `ElevatorPresenter`, `HubRoomLayout`, `KitAvailability`, `KitEntry` … | Core.Armory, Core.Classes, Core.Common, Core.Elevator, Core.Iteration, Core.Iteration.Presence, Core.Prototype, Core.Visuals |
| [Core.Hud](modulos/Core.Hud.md) | 5 | `GameHudPresenter`, `HudBar`, `NoticeSeverity`, `HudNotice`, `HudNotices` … | Core.Common, Core.Events, Core.Iteration, Core.Prototype, Core.Time, Core.Vitals |
| [Core.Items](modulos/Core.Items.md) | 11 | `Durability`, `IDurability`, `IInventory`, `Inventory`, `ItemCategory` … | Core.Common |
| [Core.Items.Catalog](modulos/Core.Items.Catalog.md) | 6 | `ConsumableEffect`, `ConsumableUse`, `GearIds`, `ItemCatalog`, `ItemIds` | Core.Common, Core.Items, Core.Vitals |
| [Core.Iteration](modulos/Core.Iteration.md) | 9 | `IterationConfig`, `IterationConfigBuilder`, `IterationContext`, `IterationPhaseChanged`, `FloorReached` … | Core.Common, Core.Events, Core.Iteration.Ending, Core.Iteration.Presence, Core.Iteration.Readiness, Core.Iteration.States, Core.Iteration.Voting, Core.StateMachines, Core.Time |
| [Core.Iteration.Ending](modulos/Core.Iteration.Ending.md) | 3 | `FateResolver`, `IterationEnded`, `IterationOutcome`, `PlayerFate` | Core.Iteration.Presence |
| [Core.Iteration.Presence](modulos/Core.Iteration.Presence.md) | 4 | `PlayerLocation`, `PlayersTrapped`, `PlayerRescued`, `PresenceTracker`, `TrappedModifiers` | Core.Common |
| [Core.Iteration.Readiness](modulos/Core.Iteration.Readiness.md) | 4 | `HasteBonusEarned`, `IHasteBonusPolicy`, `LinearHasteBonus`, `ReadyCheck` | Core.Common |
| [Core.Iteration.States](modulos/Core.Iteration.States.md) | 5 | `CalmState`, `EndedState`, `IncursionState`, `SignalState`, `TimedPhaseState` | Core.Iteration.Ending, Core.Iteration.Presence, Core.Iteration.Readiness, Core.StateMachines, Core.Time |
| [Core.Iteration.Voting](modulos/Core.Iteration.Voting.md) | 2 | `EarlyCloseVote`, `EarlyCloseApproved`, `MajorityVote` | Core.Common |
| [Core.Lab](modulos/Core.Lab.md) | 3 | `LabMessages`, `LabPresenter`, `NodeState`, `LabNodeView`, `LabBranchView` | Core.Common, Core.Dna |
| [Core.Life](modulos/Core.Life.md) | 7 | `CharacterLife`, `IReviveCost`, `LifeState`, `PartyLife`, `ReviveMethod` … | Core.Common, Core.Iteration, Core.Vitals |
| [Core.Loot](modulos/Core.Loot.md) | 7 | `DepthRings`, `IRingWeighting`, `LootDrop`, `LootEntry`, `LootTable` … | Core.Common, Core.Items, Core.Randomness |
| [Core.Lore](modulos/Core.Lore.md) | 3 | `FragmentLedger`, `LoreEntry`, `MemoryArchive` | Core.Echoes.Voting |
| [Core.Movement](modulos/Core.Movement.md) | 9 | `CameraPoint`, `CameraZoom`, `DodgeConfig`, `DodgeController`, `MovementConfig` … | Core.Common, Core.Vitals |
| [Core.Physics](modulos/Core.Physics.md) | 4 | `ColliderBox`, `CollisionLayer`, `CollisionMatrix`, `DefaultCollisionMatrix` | Core.Common |
| [Core.Professions](modulos/Core.Professions.md) | 8 | `DefaultProfessionSynergies`, `ISynergyCatalog`, `LearnedProfessions`, `ProfessionBench`, `ProfessionFurniture` … | Core.Common |
| [Core.Profile](modulos/Core.Profile.md) | 7 | `IProfileStore`, `InMemoryProfileStore`, `PlayerProfile`, `ProfileData`, `ProfileFormatException` … | Core.Common, Core.Dna, Core.Lore, Core.Stats |
| [Core.Progression](modulos/Core.Progression.md) | 14 | `CharacterProgress`, `HeroProgression`, `KitSpellbooks`, `LevelCurve`, `Difficulty` … | Core.Bestiary, Core.Classes, Core.Combat, Core.Common, Core.Equipment, Core.Professions, Core.Stats, Core.Talents, Core.Vitals |
| [Core.Prototype](modulos/Core.Prototype.md) | 7 | `DebugCommands`, `DebugHudPresenter`, `GreyboxRoomLayout`, `HudTime`, `ILevelLayout` … | Core.Common, Core.Physics, Core.Time, Core.Visuals, Core.Vitals |
| [Core.Randomness](modulos/Core.Randomness.md) | 2 | `IRandom`, `Pcg32Random` | Core.Common |
| [Core.Repair](modulos/Core.Repair.md) | 6 | `IRepairCatalog`, `RepairCatalog`, `RepairReport`, `RepairResult`, `RepairRule` … | Core.Common, Core.Items |
| [Core.Sandbox](modulos/Core.Sandbox.md) | 11 | `CreatureLevels`, `RespawnTimer`, `SandboxAggro`, `SandboxGearLoot`, `SandboxLayout` … | Core.Bestiary, Core.Classes, Core.Combat.Bosses, Core.Combat.Enemies, Core.Common, Core.Items.Catalog, Core.Loot, Core.Professions, Core.Prototype, Core.Randomness, Core.Visuals, Core.Visuals.Packs |
| [Core.Siege](modulos/Core.Siege.md) | 3 | `WaveConfig`, `WaveDirector`, `WaveState` | Core.Common |
| [Core.Siege.Defenses](modulos/Core.Siege.Defenses.md) | 2 | `Fortification`, `Turret` | Core.Common |
| [Core.Siege.Extraction](modulos/Core.Siege.Extraction.md) | 3 | `DnaConversion`, `ExtractionReport`, `OspreyExtraction` | Core.Common, Core.Siege.Scoring |
| [Core.Siege.Scoring](modulos/Core.Siege.Scoring.md) | 3 | `IterationScore`, `ScoreConfig`, `ScoreResult` | Core.Common |
| [Core.StateMachines](modulos/Core.StateMachines.md) | 3 | `IState`, `StateBase`, `StateMachine`, `Transition` | Core.Common, Core.Time |
| [Core.Stats](modulos/Core.Stats.md) | 7 | `IStatFormula`, `ModifierKind`, `StandardStatFormula`, `Stat`, `StatModifier` … | Core.Common |
| [Core.Talents](modulos/Core.Talents.md) | 10 | `KitTalentTrees`, `TalentAllocation`, `TalentBonuses`, `TalentBranch`, `TalentBuilder` … | Core.Classes, Core.Common, Core.Stats |
| [Core.Time](modulos/Core.Time.md) | 4 | `IClock`, `ITickable`, `ManualClock`, `PhaseTimer` | Core.Common |
| [Core.Visuals](modulos/Core.Visuals.md) | 14 | `DefaultGreyboxStyles`, `GreyboxColor`, `GreyboxOffset`, `GreyboxPrimitive`, `GreyboxStyle` … | Core.Common |
| [Core.Visuals.Packs](modulos/Core.Visuals.Packs.md) | 2 | `EnvironmentKit`, `MaterialTextureNames` | — |
| [Core.Vitals](modulos/Core.Vitals.md) | 7 | `AcceleratingRegen`, `CharacterVitals`, `Fatigue`, `FatigueConfig`, `ResourceKind` … | Core.Common, Core.Stats, Core.Time |
| [Core.World](modulos/Core.World.md) | 11 | `HouseBlueprint`, `HousePiece`, `HousePieceKind`, `HouseSpot`, `HouseStyle` … | Core.Common, Core.Gathering, Core.Randomness |
| [Runtime.Art](modulos/Runtime.Art.md) | 1 | `MoodVolumeDriver` | Core.Art |
| [Runtime.Atico](modulos/Runtime.Atico.md) | 3 | `AticoBootstrap`, `OspreyPad`, `SiegeController` | Core.Armory, Core.Classes, Core.Composition, Core.Events, Core.Hud, Core.Iteration, Core.Iteration.Ending, Core.Prototype, Core.Siege, Core.Siege.Extraction, Core.Visuals, Core.Vitals, Runtime.Bootstrap, Runtime.Combat, Runtime.Incursion, Runtime.Player, Runtime.Prototype, Runtime.Visuals |
| [Runtime.Bootstrap](modulos/Runtime.Bootstrap.md) | 4 | `GameBootstrap`, `PlayerPrefsProfileStore`, `SceneBootstrapper`, `UnitySceneLoader` | Core.Composition, Core.Profile, Runtime.Atico, Runtime.Extraction, Runtime.Hubs, Runtime.Incursion, Runtime.Lab, Runtime.Prototype, Runtime.Sandbox |
| [Runtime.Characters](modulos/Runtime.Characters.md) | 1 | `CharacterAnimator` | Core.Animation |
| [Runtime.Combat](modulos/Runtime.Combat.md) | 4 | `EnemyActor`, `EnemySpawner`, `PlayerAttack`, `SpellBolt` | Core.Classes, Core.Combat, Core.Combat.Attacks, Core.Combat.Enemies, Core.Floors, Core.Movement, Core.Randomness, Core.Stats, Core.Vitals, Runtime.Player |
| [Runtime.Extraction](modulos/Runtime.Extraction.md) | 1 | `ExtraccionBootstrap` | Core.Composition, Core.Dna, Core.Extraction, Core.Iteration, Core.Iteration.Ending, Core.Loot, Core.Profile, Runtime.Bootstrap |
| [Runtime.Hubs](modulos/Runtime.Hubs.md) | 6 | `ArmoryRoom`, `ArmoryScreen`, `ElevatorRoom`, `ElevatorScreen`, `HubBootstrap` … | Core.Armory, Core.Classes, Core.Composition, Core.Elevator, Core.Events, Core.Hubs, Core.Hud, Core.Iteration, Core.Visuals, Runtime.Bootstrap, Runtime.Combat, Runtime.Incursion, Runtime.Player, Runtime.Prototype, Runtime.Visuals |
| [Runtime.Hud](modulos/Runtime.Hud.md) | 1 | `GameHud` | Core.Hud |
| [Runtime.Incursion](modulos/Runtime.Incursion.md) | 4 | `IncursionBootstrap`, `IncursionPlayer`, `LootPickups` | Core.Armory, Core.Classes, Core.Composition, Core.Events, Core.Floors, Core.Hud, Core.Iteration, Core.Iteration.Presence, Core.Loot, Core.Stats, Core.Visuals, Core.Vitals, Runtime.Bootstrap, Runtime.Combat, Runtime.Hud, Runtime.Player, Runtime.Prototype, Runtime.Visuals |
| [Runtime.Lab](modulos/Runtime.Lab.md) | 3 | `LabActions`, `LabBootstrap`, `LabScreen` | Core.Composition, Core.Dna, Core.Iteration, Core.Lab, Core.Profile, Runtime.Bootstrap |
| [Runtime.Player](modulos/Runtime.Player.md) | 5 | `GameCamera`, `KitBadge`, `KitStats`, `PlayerController`, `PlayerInput` | Core.Armory, Core.Classes, Core.Composition, Core.Movement, Core.Profile, Core.Stats, Core.Vitals |
| [Runtime.Prototype](modulos/Runtime.Prototype.md) | 7 | `DebugHud`, `PlayerMotor`, `PrototypeBootstrap`, `PrototypeInput`, `PrototypePlayer` … | Core.Movement, Core.Prototype, Core.Stats, Core.Time, Core.Visuals, Core.Vitals, Runtime.Visuals |
| [Runtime.Sandbox](modulos/Runtime.Sandbox.md) | 33 | `AutoAttack`, `BenchStation`, `CombatEvents`, `CombatFeed`, `DamageColors` … | Core.Abilities, Core.Animation, Core.Bestiary, Core.Classes, Core.Combat, Core.Combat.Attacks, Core.Combat.Enemies, Core.Combat.Targeting, Core.Crafting, Core.Equipment, Core.Items, Core.Items.Catalog, Core.Loot, Core.Professions, Core.Progression, Core.Randomness, Core.Sandbox, Core.Stats, Core.Talents, Core.Visuals, Core.Vitals, Core.World, Runtime.Characters, Runtime.Player, Runtime.Sandbox.Ui, Runtime.Sandbox.World, Runtime.UI, Runtime.Visuals |
| [Runtime.Sandbox.Ui](modulos/Runtime.Sandbox.Ui.md) | 22 | `CharacterStats`, `CharacterWindow`, `CombatText`, `Gear`, `HudActionBar` … | Core.Classes, Core.Combat, Core.Crafting, Core.Equipment, Core.Gathering, Core.Items, Core.Items.Catalog, Core.Professions, Core.Progression, Core.Stats, Core.Talents, Core.Vitals, Core.World, Runtime.UI |
| [Runtime.Sandbox.World](modulos/Runtime.Sandbox.World.md) | 5 | `GatherNode`, `NatureFactory`, `VillageBuilder`, `WorldMaterials` | Core.Gathering, Core.Items, Core.Progression, Core.World, Runtime.Visuals |
| [Runtime.UI](modulos/Runtime.UI.md) | 9 | `UiDragHandle`, `UiFactory`, `UiHoverTip`, `UiRoot`, `UiSlot` … | — |
| [Runtime.Visuals](modulos/Runtime.Visuals.md) | 5 | `CatalogoArte`, `CatalogoVisual`, `GreyboxFactory`, `VisualSpawner` | Core.Classes, Core.Professions, Core.Prototype, Core.Visuals |

Grafo de dependencias entre módulos: [GRAFO.md](GRAFO.md).
