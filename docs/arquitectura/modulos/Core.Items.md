<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Items`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Items`
Tests: `Assets/_Project/Tests/EditMode/Core/Items`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Crafting](Core.Crafting.md), [Core.Elevator](Core.Elevator.md), [Core.Equipment](Core.Equipment.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Loot](Core.Loot.md), [Core.Repair](Core.Repair.md), [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md), [Runtime.Sandbox.World](Runtime.Sandbox.World.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `Durability.cs` | class `Durability` | Durabilidad acotada a [0, Max]. |
| `IDurability.cs` | interface `IDurability` | Desgaste de un objeto. |
| `IInventory.cs` | interface `IInventory` | Inventario de ranuras con capacidad fija y apilado por objeto. |
| `Inventory.Transfer.cs` | class `Inventory` |  |
| `Inventory.cs` | class `Inventory` | Inventario por ranuras: cada ranura guarda una pila. |
| `ItemCategory.cs` | enum `ItemCategory` | Categoría funcional de un objeto. |
| `ItemDefinition.cs` | class `ItemDefinition` | Plantilla inmutable de un objeto. |
| `ItemInstance.cs` | class `ItemInstance` | Objeto concreto en el mundo o en un inventario: su plantilla más su propio desgaste. |
| `ItemStack.cs` | class `ItemStack` | Contenido de una ranura: un objeto y cuántas unidades hay. |
| `MaterialTags.cs` | enum `MaterialTags` | Etiquetas de material (combinables). |
| `Rarity.cs` | enum `Rarity` | Rareza de un objeto, de menor a mayor. |
| `RarityRules.cs` | class `RarityRules` | Reglas por rareza: cuanto más rara, más aguanta el equipo antes de romperse. |
