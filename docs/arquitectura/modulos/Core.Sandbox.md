<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Sandbox`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Sandbox`
Tests: `Assets/_Project/Tests/EditMode/Core/Sandbox`

**Depende de:** [Core.Bestiary](Core.Bestiary.md), [Core.Classes](Core.Classes.md), [Core.Combat.Bosses](Core.Combat.Bosses.md), [Core.Common](Core.Common.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Loot](Core.Loot.md), [Core.Professions](Core.Professions.md), [Core.Prototype](Core.Prototype.md), [Core.Randomness](Core.Randomness.md), [Core.Visuals](Core.Visuals.md), [Core.Visuals.Packs](Core.Visuals.Packs.md)
**Lo usan:** [Runtime.Sandbox](Runtime.Sandbox.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CreatureLevels.cs` | class `CreatureLevels` | Nivel de cada criatura del poblado y su escalado (vida +25 %, daño +15 % y 40 de armadura por nivel), para que el nivel del jugador, su arma |
| `RespawnTimer.cs` | class `RespawnTimer` | Cuenta atrás de reaparición: se arma al morir y avisa una sola vez cuando se cumple. |
| `SandboxGearLoot.cs` | class `SandboxGearLoot` | Equipo que sueltan las criaturas, aparte de los materiales: los diablillos dejan guantes o dagas, los puglins yelmos, botas o (raro) su hach |
| `SandboxLayout.cs` | class `SandboxLayout` | Campo de pruebas: sala cerrada con el jugador al sur, el bestiario al norte, los bancos de profesión en los muros este y oeste y algo de dec |
| `SandboxLoot.cs` | class `SandboxLoot` | Botín del bestiario con los materiales de las profesiones: los diablillos sueltan tela, chatarra y cristal; los puglins, cuero y carne. |
| `SandboxRespawn.cs` | class `SandboxRespawn` | Cuánto tarda en reaparecer cada criatura del Campo de pruebas (el jefe, más). |
| `SandboxSpot.cs` | struct `SandboxSpot` | Punto del Campo de pruebas: qué va (id), dónde (m) y hacia dónde mira (grados, 0 = +Z). |
| `SignatureEffect.cs` | class `SignatureEffect` | Efecto de una habilidad de firma: tipo, magnitud (multiplicador de daño, vida curada, metros o reducción de daño según el tipo), radio (m) y |
| `SignatureEffects.cs` | class `SignatureEffects` | Efecto de la habilidad de firma de cada kit (DefaultClassAbilities) en el Campo de pruebas. |
| `SignatureKind.cs` | enum `SignatureKind` | Qué hace la habilidad de firma de un kit al activarse (versión jugable de prueba). |
