<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Items.Catalog`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Items/Catalog`
Tests: (sin carpeta propia)

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Core.Crafting](Core.Crafting.md), [Core.Crafting.Content](Core.Crafting.Content.md), [Core.Crafting.Queue](Core.Crafting.Queue.md), [Core.Equipment](Core.Equipment.md), [Core.Gathering](Core.Gathering.md), [Core.Sandbox](Core.Sandbox.md), [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ConsumableEffect.cs` | class `ConsumableEffect` | Qué hace un consumible al usarlo: vida y recurso de clase que devuelve, y descripción para el tooltip. |
| `ConsumableUse.cs` | class `ConsumableUse` | Consumir un objeto de la bolsa (clic derecho): aplica su efecto a las vitales y gasta una unidad. |
| `FuelIds.cs` | class `FuelIds` | Ids estables de los combustibles de §17.5 (sus segundos de quemado están en FuelTable). |
| `GearIds.cs` | class `GearIds` | Ids del equipo nuevo: equipo inicial de cada kit y botín de las criaturas del poblado. |
| `ItemCatalog.Gear.cs` | class `ItemCatalog` | Equipo inicial de los kits y equipo que sueltan las criaturas (sus stats están en GearCatalog). |
| `ItemCatalog.Materials.Crafted.cs` | class `ItemCatalog` | Intermedios sin calidad (un oficio los fabrica para otro) y combustibles de §17.5. |
| `ItemCatalog.Materials.Mineral.cs` | class `ItemCatalog` | Materiales de §17.5: metal, mineral y vidrio (reutiliza hierro, lingote, chatarra y cristal). |
| `ItemCatalog.Materials.Natural.cs` | class `ItemCatalog` | Materiales de §17.5: madera, piedra, cuero, tela y hueso (reutiliza madera, piedra y cuero). |
| `ItemCatalog.Materials.Organic.cs` | class `ItemCatalog` | Materiales de §17.5: hierbas y comida (reutiliza carne y hierba de ceniza). |
| `ItemCatalog.Products.Gear.cs` | class `ItemCatalog` | Equipo y herramientas del recetario de oficios (§17.6), sinergias incluidas. |
| `ItemCatalog.Products.Supplies.cs` | class `ItemCatalog` | Comida, preparados e intermedios del recetario de oficios (§17.6). |
| `ItemCatalog.cs` | class `ItemCatalog` | Catálogo de objetos del juego: materiales (recolección y botín), consumibles y equipo fabricado. |
| `ItemIds.cs` | class `ItemIds` | Ids estables de los objetos del catálogo (se guardan en inventarios y recetas). |
