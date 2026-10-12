<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Save`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Save`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Save`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftingSave.cs` | class `CraftingSave` | Guarda y carga el progreso de oficios en un . |
| `CraftingSaveData.cs` | class `CraftingSaveData` | Instantánea del progreso de oficios e inventario del personaje (GDD §17.7). |
| `CraftingSaveFormatException.cs` | class `CraftingSaveFormatException` | El texto guardado no es un guardado de oficios válido (GDD §17.7). |
| `CraftingSaveReader.cs` | class `CraftingSaveReader` | Lee el texto oficios v1 ; cualquier error lanza . |
| `CraftingSaveWriter.cs` | class `CraftingSaveWriter` | Escribe como texto oficios v1 , una clave por línea. |
| `ICraftingSaveStore.cs` | interface `ICraftingSaveStore` | Almacén de texto del guardado de oficios. |
| `InMemoryCraftingSaveStore.cs` | class `InMemoryCraftingSaveStore` | Almacén en memoria, para tests y para el prototipo. |
| `ItemEntries.cs` | class `ItemEntry` | Instancia con calidad, durabilidad y mods: item=ab12:espada_hierro:4:120:mod.a,mod.b . |
| `ItemEntries.cs` | class `InventoryEntry` | Ranura de inventario: instancia ( inv=bolsa:3:ab12 ) o pila ( inv=bolsa:4:madera_pino:12 ). |
| `JobEntry.cs` | class `JobEntry` | Trabajo en cola con sus segundos restantes y el oficio/nivel del artesano: job=fragua_aldea:herrero.espada_acero:12.5:Herrero:7 . |
| `SaveFields.cs` | class `SaveFields` | Conversión de campos de una línea clave=a:b:c con errores de formato claros. |
| `SaveLineParsers.cs` | class `SaveLineParsers` | Convierte el valor de cada clave en su entrada del guardado. |
| `SkillEntry.cs` | class `SkillEntry` | Nivel y XP de un oficio: skill=Herrero:7:340 . |
| `StationEntries.cs` | class `StationEntry` | Estación y su nivel: station=fragua_aldea:2 . |
| `StationEntries.cs` | class `PartEntry` | Pieza colocada: part=fragua_aldea:0:pieza.crisol:37 (estación:ranura:ítem:durabilidad). |
| `StationEntries.cs` | class `SlotEntry` | Contenido de una ranura: slot=fragua_aldea:fuel:0:carbon_vegetal:4 . |
