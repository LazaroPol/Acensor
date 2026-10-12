<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Equipment`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Equipment`
Tests: `Assets/_Project/Tests/EditMode/Core/Equipment`

**Depende de:** [Core.Classes](Core.Classes.md), [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Randomness](Core.Randomness.md), [Core.Stats](Core.Stats.md)
**Lo usan:** [Core.Progression](Core.Progression.md), [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CharacterEquipment.cs` | class `CharacterEquipment` | Piezas equipadas por hueco. |
| `EquipSlot.cs` | enum `EquipSlot` | Huecos del personaje (hoja de personaje estilo WoW). |
| `GearBonuses.cs` | class `GearBonuses` | Vuelca el equipo en la hoja de stats: armadura total y bonos planos de cada pieza (idempotente). |
| `GearCatalog.cs` | class `GearCatalog` | Estadísticas del equipo, con velocidades típicas de WoW (daga 1,8 · espada 2,6 · hacha 2,7 · maza 2,8 · bastón 3,0 · lanza 3,4 · mandoble 3, |
| `GearDelta.cs` | class `GearDelta` | Diferencia entre una pieza y la equipada en su hueco (lo que WoW muestra en verde/rojo al comparar): armadura, daño por segundo del arma y b |
| `GearPiece.cs` | class `GearPiece` | Datos de equipo de un objeto: hueco, armadura, arma (si lo es) y bonos planos de atributos. |
| `StarterGear.cs` | class `StarterGear` | Equipo con el que empieza cada kit (arma con su velocidad propia, pieza de pecho y pantalones). |
| `WeaponKind.cs` | enum `WeaponKind` |  |
| `WeaponSpec.cs` | class `WeaponSpec` | Daño y velocidad fijos de un arma, como en WoW: cada golpe hace entre y más la aportación del poder de ataque, una vez cada segundos. |
