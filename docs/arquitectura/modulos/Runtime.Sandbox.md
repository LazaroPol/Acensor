<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Sandbox`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Sandbox`
Tests: (sin carpeta propia)

**Depende de:** [Core.Abilities](Core.Abilities.md), [Core.Animation](Core.Animation.md), [Core.Bestiary](Core.Bestiary.md), [Core.Classes](Core.Classes.md), [Core.Combat](Core.Combat.md), [Core.Combat.Attacks](Core.Combat.Attacks.md), [Core.Combat.Enemies](Core.Combat.Enemies.md), [Core.Items](Core.Items.md), [Core.Loot](Core.Loot.md), [Core.Movement](Core.Movement.md), [Core.Professions](Core.Professions.md), [Core.Randomness](Core.Randomness.md), [Core.Sandbox](Core.Sandbox.md), [Core.Stats](Core.Stats.md), [Core.Visuals](Core.Visuals.md), [Core.Vitals](Core.Vitals.md), [Runtime.Characters](Runtime.Characters.md), [Runtime.Player](Runtime.Player.md), [Runtime.Prototype](Runtime.Prototype.md), [Runtime.Visuals](Runtime.Visuals.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `BenchStation.cs` | class `BenchStation` | Banco de profesión: con E cerca se elige la profesión en el ProfessionBench y se ven sus sinergias. |
| `CombatFeed.cs` | class `CombatFeed` | Registro de los últimos sucesos del combate para el HUD (daño, botín, kit, avisos). |
| `DamageColors.cs` | class `DamageColors` | Color de cada tipo de daño (proyectiles, textos). |
| `EnemyRegistry.cs` | class `EnemyRegistry` | Enemigos del Campo de pruebas (vivos y caídos) para que ataques y HUD los recorran sin buscar en la escena. |
| `HitResolver.cs` | class `HitResolver` | Resuelve golpes del jugador contra enemigos con el DamageCalculator del núcleo y lo anota en el registro. |
| `LootPickup.cs` | class `LootPickup` | Botín en el suelo: gira y se recoge al pasar cerca (va al inventario del contexto). |
| `PlayerCombat.cs` | class `PlayerCombat` | Ataque básico del kit (clic izquierdo / J): melé en arco delante del jugador o proyectil de hechizo. |
| `Projectile.cs` | class `Projectile` | Proyectil de hechizo: avanza en línea recta y golpea al primer enemigo vivo que toca. |
| `SandboxBootstrap.cs` | class `SandboxBootstrap` | Arranque de la escena «CampoPruebas»: monta sala, bancos, jugador y bestiario con el arte generado (o greybox si falta) y atiende los atajos |
| `SandboxContext.cs` | class `SandboxContext` | Servicios compartidos del Campo de pruebas (se crean una vez en el bootstrap y se inyectan). |
| `SandboxEnemy.cs` | class `SandboxEnemy` | Criatura del bestiario en escena: la IA del núcleo decide (esperar/perseguir/atacar), aquí solo se mueve, se anima y aplica el golpe al juga |
| `SandboxHud.Panels.cs` | class `SandboxHud` | Paneles secundarios del HUD: registro de combate, ayuda, barras de enemigos y aviso de banco. |
| `SandboxHud.cs` | class `SandboxHud` | HUD de depuración del Campo de pruebas (IMGUI): estado del kit, registro, ayuda y vida de enemigos. |
| `SandboxInput.cs` | class `SandboxInput` | Entradas propias del Campo de pruebas (mover, esprintar y esquivar las lleva PlayerController): ataque, firma, interactuar, cambio de kit (1 |
| `SandboxPlayer.Life.cs` | class `SandboxPlayer` | Muerte, reaparición e impulsos del jugador del Campo de pruebas. |
| `SandboxPlayer.cs` | class `SandboxPlayer` | Jugador del Campo de pruebas sobre el PlayerController de producción: cambia de kit (stats + modelo), anima la locomoción, recibe golpes (co |
| `SandboxWorld.Loot.cs` | class `SandboxWorld` | Botín del Campo de pruebas: al morir una criatura se tira su tabla y aparece en el suelo. |
| `SandboxWorld.cs` | class `SandboxWorld` | Construye el contenido del Campo de pruebas: sala, decorado, bancos, jugador y bestiario. |
| `SignatureCaster.cs` | class `SignatureCaster` | Habilidad de firma del kit (Q): cobra con AbilitySlot del núcleo y aplica su SignatureEffect. |
| `VisualFactory.cs` | class `VisualFactory` | Instancia el prefab de arte o, si falta (arte sin generar), una cápsula greybox con el pivote en los pies. |
