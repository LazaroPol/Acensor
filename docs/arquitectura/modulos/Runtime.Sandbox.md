<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Sandbox`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Sandbox`
Tests: (sin carpeta propia)

**Depende de:** [Core.Abilities](Core.Abilities.md), [Core.Animation](Core.Animation.md), [Core.Bestiary](Core.Bestiary.md), [Core.Classes](Core.Classes.md), [Core.Combat](Core.Combat.md), [Core.Combat.Attacks](Core.Combat.Attacks.md), [Core.Combat.Enemies](Core.Combat.Enemies.md), [Core.Combat.Targeting](Core.Combat.Targeting.md), [Core.Crafting](Core.Crafting.md), [Core.Crafting.Authoring](Core.Crafting.Authoring.md), [Core.Crafting.Content](Core.Crafting.Content.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Mods](Core.Crafting.Mods.md), [Core.Crafting.Play](Core.Crafting.Play.md), [Core.Crafting.Save](Core.Crafting.Save.md), [Core.Crafting.Session](Core.Crafting.Session.md), [Core.Equipment](Core.Equipment.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Loot](Core.Loot.md), [Core.Professions](Core.Professions.md), [Core.Progression](Core.Progression.md), [Core.Randomness](Core.Randomness.md), [Core.Sandbox](Core.Sandbox.md), [Core.Stats](Core.Stats.md), [Core.Talents](Core.Talents.md), [Core.Visuals](Core.Visuals.md), [Core.Vitals](Core.Vitals.md), [Core.World](Core.World.md), [Runtime.Characters](Runtime.Characters.md), [Runtime.Player](Runtime.Player.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md), [Runtime.Sandbox.World](Runtime.Sandbox.World.md), [Runtime.UI](Runtime.UI.md), [Runtime.Visuals](Runtime.Visuals.md)
**Lo usan:** [Runtime.Bootstrap](Runtime.Bootstrap.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `AutoAttack.cs` | class `AutoAttack` | «Atacar» (F o clic derecho sobre un enemigo): ataque automático con el arma equipada, como en WoW. |
| `BenchStation.cs` | class `BenchStation` | Estación de oficio del poblado (GDD §17.7): envuelve una (estación, cola, combustible y piezas) que avanza en tiempo real; con E cerca abre  |
| `CombatEvents.cs` | class `CombatEvents` | Sucesos de combate y progresión que pinta la interfaz (texto flotante, errores, marco de objetivo). |
| `CombatFeed.cs` | class `CombatFeed` | Registro de los últimos sucesos del combate para el HUD (daño, botín, kit, avisos). |
| `DamageColors.cs` | class `DamageColors` | Color de cada tipo de daño (proyectiles, textos). |
| `EnemyRegistry.cs` | class `EnemyRegistry` | Enemigos del Campo de pruebas (vivos y caídos) para que ataques y HUD los recorran sin buscar en la escena. |
| `HitResolver.cs` | class `HitResolver` | Resuelve golpes con el DamageCalculator del núcleo y la armadura al estilo WoW (depende del nivel del atacante), lo anota en el registro y l |
| `HitSource.cs` | enum `HitSource` | De dónde sale un golpe del jugador (el texto flotante lo pinta blanco o amarillo). |
| `IInteractable.cs` | interface `IInteractable` | Algo que se usa con E estando cerca: banco de profesión, árbol, roca, hierba… |
| `InteractionHub.cs` | class `InteractionHub` | Elige el interactuable más cercano al jugador (para el aviso «E · …») y lo usa al pulsar E. |
| `LootPickup.cs` | class `LootPickup` | Botín en el suelo: gira y se recoge al pasar cerca (va al inventario del contexto). |
| `MouseGestures.cs` | class `MouseGestures` | Distingue clic de arrastre con cada botón del ratón (como en WoW): arrastrar mueve la cámara y un clic sin arrastre selecciona/ataca. |
| `Nameplates.cs` | class `Nameplates` | Placas sobre las criaturas (nivel y nombre con color de dificultad, vida; «» marca el objetivo) y reaparición. |
| `PlayerPrefsCraftingSaveStore.cs` | class `PlayerPrefsCraftingSaveStore` | Guardado de oficios del Campo de pruebas en PlayerPrefs (GDD §17.7), con copia .bak si se corrompe. |
| `PlayerTarget.cs` | class `PlayerTarget` | Objetivo del jugador al estilo WoW: Tab recorre los enemigos cercanos, clic izquierdo sobre uno lo selecciona y clic derecho lo selecciona y |
| `Projectile.cs` | class `Projectile` | Proyectil: avanza en línea recta (o persigue a su objetivo, si lo tiene) y golpea al enemigo que toca. |
| `SandboxBootstrap.Cheats.cs` | class `SandboxBootstrap` | Atajos de prueba para la progresión: F3 da experiencia y F4 un lote de materiales básicos y de oficio (cobre, leña y un crisol para probar l |
| `SandboxBootstrap.cs` | class `SandboxBootstrap` | Arranque de la escena «CampoPruebas»: monta la Armería, el poblado, jugador, bestiario e interfaz con el arte generado (o greybox si falta)  |
| `SandboxContext.Crafting.cs` | class `SandboxContext` | Oficios del Campo de pruebas (GDD §17.7): leer conocimientos de la bolsa, mods de objetos, botín de oficio y guardado automático (carga al e |
| `SandboxContext.cs` | class `SandboxContext` | Servicios compartidos del Campo de pruebas (se crean una vez en el bootstrap y se inyectan). |
| `SandboxEnemy.Respawn.cs` | class `SandboxEnemy` | Reaparición del bestiario: tras morir, la criatura vuelve a su sitio con la vida llena. |
| `SandboxEnemy.Stats.cs` | class `SandboxEnemy` | Nivel de la criatura y sus stats escalados (vida, daño y armadura), como los «mobs» de WoW. |
| `SandboxEnemy.cs` | class `SandboxEnemy` | Criatura del bestiario en escena: la IA del núcleo decide (esperar/perseguir/atacar), aquí solo se mueve, se anima y aplica el golpe al juga |
| `SandboxInput.cs` | class `SandboxInput` | Entradas propias del Campo de pruebas (mover, esprintar y esquivar las lleva PlayerController): objetivo (Tab / clic), ataque automático con |
| `SandboxLighting.cs` | class `SandboxLighting` | Iluminación del Campo de pruebas: sol cálido con sombras suaves, ambiente en tres tonos y postproceso. |
| `SandboxPlayer.Life.cs` | class `SandboxPlayer` | Muerte, reaparición e impulsos del jugador del Campo de pruebas. |
| `SandboxPlayer.cs` | class `SandboxPlayer` | Jugador del Campo de pruebas sobre el PlayerController de producción: cambia de kit (stats + modelo), anima la locomoción, recibe golpes (co |
| `SandboxWorld.Bestiary.cs` | class `SandboxWorld` | Criaturas del Campo de pruebas, repartidas por los campamentos del poblado. |
| `SandboxWorld.Loot.cs` | class `SandboxWorld` | Botín del Campo de pruebas: al morir una criatura se tira su tabla y aparece en el suelo. |
| `SandboxWorld.cs` | class `SandboxWorld` | Construye el contenido del Campo de pruebas: sala, decorado, bancos, jugador y bestiario. |
| `SignatureCaster.cs` | class `SignatureCaster` | Habilidad de firma del kit (Q): cobra con AbilitySlot del núcleo y aplica su SignatureEffect. |
| `SpellCaster.Effects.cs` | class `SpellCaster` | Efectos de cada tipo de hechizo. |
| `SpellCaster.cs` | class `SpellCaster` | Barra de hechizos (teclas 1-5): un hueco por hechizo del kit; se desbloquean al subir de nivel. |
| `VisualFactory.cs` | class `VisualFactory` | Instancia el prefab de arte o, si falta (arte sin generar), una cápsula greybox con el pivote en los pies. |
| `WowCamera.cs` | class `WowCamera` | Cámara al estilo WoW sobre : arrastrar con el botón izquierdo gira la cámara alrededor del personaje, con el derecho la gira y el personaje  |
