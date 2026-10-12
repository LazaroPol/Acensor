<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Queue`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Queue`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Queue`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Stations](Core.Crafting.Stations.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Professions](Core.Professions.md), [Core.Time](Core.Time.md)
**Lo usan:** [Core.Crafting.Session](Core.Crafting.Session.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftJob.cs` | class `CraftJob` | Trabajo encolado: receta, materiales ya retirados, de dónde salieron, quién lo encoló y los datos de calidad congelados al encolar (GDD §17. |
| `CraftQueue.Tick.cs` | class `CraftQueue` | Avance de la cola: combustible, temporizador y entrega (GDD §17.3). |
| `CraftQueue.cs` | class `CraftQueue` | Cola de fabricación de una (GDD §17.3): al encolar reserva y retira los materiales (entrada de la estación y, si faltan, bolsa del jugador); |
| `CraftQueueState.cs` | enum `CraftQueueState` | Estado de la cola tras el último avance: pausada no es cancelada (no se pierde nada). |
| `CraftTiming.cs` | class `CraftTiming` | Tiempo real de un trabajo (GDD §17.3): CraftSeconds × (1 − bonusOficio) × (1 − bonusPiezas) , mínimo . |
| `EnqueueResult.cs` | enum `EnqueueResult` | Resultado de intentar encolar una receta en una estación. |
| `FuelTable.Default.cs` | class `FuelTable` | Combustibles por defecto de §17.5, derivados del catálogo de objetos. |
| `FuelTable.cs` | class `FuelTable` | Combustibles (datos): segundos que arde cada unidad y, si procede, única estación donde arde (GDD §17.5). |
| `InventoryChain.cs` | class `InventoryChain` | Vista de dos inventarios como uno (GDD §17.3: la estación tira de su entrada y, si falta, de la bolsa del jugador). |
