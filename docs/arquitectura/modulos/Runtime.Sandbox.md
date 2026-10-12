<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Sandbox`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Sandbox`
Tests: (sin carpeta propia)

**Depende de:** [Core.Abilities](Core.Abilities.md), [Core.Animation](Core.Animation.md), [Core.Bestiary](Core.Bestiary.md), [Core.Classes](Core.Classes.md), [Core.Combat](Core.Combat.md), [Core.Combat.Attacks](Core.Combat.Attacks.md), [Core.Combat.Enemies](Core.Combat.Enemies.md), [Core.Crafting](Core.Crafting.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Loot](Core.Loot.md), [Core.Movement](Core.Movement.md), [Core.Professions](Core.Professions.md), [Core.Progression](Core.Progression.md), [Core.Randomness](Core.Randomness.md), [Core.Sandbox](Core.Sandbox.md), [Core.Stats](Core.Stats.md), [Core.Talents](Core.Talents.md), [Core.Visuals](Core.Visuals.md), [Core.Vitals](Core.Vitals.md), [Core.World](Core.World.md), [Runtime.Characters](Runtime.Characters.md), [Runtime.Player](Runtime.Player.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md), [Runtime.Sandbox.World](Runtime.Sandbox.World.md), [Runtime.UI](Runtime.UI.md), [Runtime.Visuals](Runtime.Visuals.md)
**Lo usan:** [Runtime.Bootstrap](Runtime.Bootstrap.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `BenchStation.cs` | class `BenchStation` | Banco de profesión: con E cerca abre la ventana de recetas de su profesión (estilo WoW). |
| `CombatFeed.cs` | class `CombatFeed` | Registro de los últimos sucesos del combate para el HUD (daño, botín, kit, avisos). |
| `DamageColors.cs` | class `DamageColors` | Color de cada tipo de daño (proyectiles, textos). |
| `EnemyRegistry.cs` | class `EnemyRegistry` | Enemigos del Campo de pruebas (vivos y caídos) para que ataques y HUD los recorran sin buscar en la escena. |
| `HitResolver.cs` | class `HitResolver` | Resuelve golpes del jugador contra enemigos con el DamageCalculator del núcleo y lo anota en el registro. |
| `IInteractable.cs` | interface `IInteractable` | Algo que se usa con E estando cerca: banco de profesión, árbol, roca, hierba… |
| `InteractionHub.cs` | class `InteractionHub` | Elige el interactuable más cercano al jugador (para el aviso «E · …») y lo usa al pulsar E. |
| `LootPickup.cs` | class `LootPickup` | Botín en el suelo: gira y se recoge al pasar cerca (va al inventario del contexto). |
| `Nameplates.cs` | class `Nameplates` | Placas de nombre y vida sobre las criaturas, y cuenta atrás de reaparición de las caídas. |
| `PlayerCombat.cs` | class `PlayerCombat` | Ataque básico del kit (clic izquierdo / J): melé en arco delante del jugador o proyectil de hechizo. |
| `Projectile.cs` | class `Projectile` | Proyectil de hechizo: avanza en línea recta y golpea al primer enemigo vivo que toca. |
| `SandboxBootstrap.Cheats.cs` | class `SandboxBootstrap` | Atajos de prueba para la progresión: F3 da experiencia y F4 un lote de materiales básicos. |
| `SandboxBootstrap.cs` | class `SandboxBootstrap` | Arranque de la escena «CampoPruebas»: monta la Armería, el poblado, jugador, bestiario e interfaz con el arte generado (o greybox si falta)  |
| `SandboxContext.cs` | class `SandboxContext` | Servicios compartidos del Campo de pruebas (se crean una vez en el bootstrap y se inyectan). |
| `SandboxEnemy.Respawn.cs` | class `SandboxEnemy` | Reaparición del bestiario: tras morir, la criatura vuelve a su sitio con la vida llena. |
| `SandboxEnemy.cs` | class `SandboxEnemy` | Criatura del bestiario en escena: la IA del núcleo decide (esperar/perseguir/atacar), aquí solo se mueve, se anima y aplica el golpe al juga |
| `SandboxInput.cs` | class `SandboxInput` | Entradas propias del Campo de pruebas (mover, esprintar y esquivar las lleva PlayerController): ataque, firma, hechizos 1-5, interactuar, ve |
| `SandboxLighting.cs` | class `SandboxLighting` | Iluminación del Campo de pruebas: sol cálido con sombras suaves, ambiente en tres tonos y postproceso. |
| `SandboxPlayer.Life.cs` | class `SandboxPlayer` | Muerte, reaparición e impulsos del jugador del Campo de pruebas. |
| `SandboxPlayer.cs` | class `SandboxPlayer` | Jugador del Campo de pruebas sobre el PlayerController de producción: cambia de kit (stats + modelo), anima la locomoción, recibe golpes (co |
| `SandboxWorld.Bestiary.cs` | class `SandboxWorld` | Criaturas del Campo de pruebas, repartidas por los campamentos del poblado. |
| `SandboxWorld.Loot.cs` | class `SandboxWorld` | Botín del Campo de pruebas: al morir una criatura se tira su tabla y aparece en el suelo. |
| `SandboxWorld.cs` | class `SandboxWorld` | Construye el contenido del Campo de pruebas: sala, decorado, bancos, jugador y bestiario. |
| `SignatureCaster.cs` | class `SignatureCaster` | Habilidad de firma del kit (Q): cobra con AbilitySlot del núcleo y aplica su SignatureEffect. |
| `SpellCaster.Effects.cs` | class `SpellCaster` | Efectos de cada tipo de hechizo en el Campo de pruebas. |
| `SpellCaster.cs` | class `SpellCaster` | Barra de hechizos (teclas 1-5): un hueco por hechizo del kit; se desbloquean al subir de nivel. |
| `VisualFactory.cs` | class `VisualFactory` | Instancia el prefab de arte o, si falta (arte sin generar), una cápsula greybox con el pivote en los pies. |
