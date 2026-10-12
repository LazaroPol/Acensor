<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Sandbox.Ui`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Sandbox/Ui`
Tests: (sin carpeta propia)

**Depende de:** [Core.Classes](Core.Classes.md), [Core.Combat](Core.Combat.md), [Core.Crafting](Core.Crafting.md), [Core.Equipment](Core.Equipment.md), [Core.Gathering](Core.Gathering.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Professions](Core.Professions.md), [Core.Progression](Core.Progression.md), [Core.Stats](Core.Stats.md), [Core.Talents](Core.Talents.md), [Core.Vitals](Core.Vitals.md), [Core.World](Core.World.md), [Runtime.UI](Runtime.UI.md)
**Lo usan:** [Runtime.Sandbox](Runtime.Sandbox.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CharacterStats.cs` | class `CharacterStats` | Texto de estadísticas de la hoja de personaje: generales, cuerpo a cuerpo (arma) y defensa (armadura). |
| `CharacterWindow.Slots.cs` | class `CharacterWindow` | Huecos de equipo de la hoja de personaje: icono con color de rareza, nombre y tooltip con stats. |
| `CharacterWindow.cs` | class `CharacterWindow` | Personaje (C), como la hoja de WoW: huecos de equipo a la izquierda (clic derecho para quitar), estadísticas de combate y defensa a la derec |
| `CombatText.cs` | class `CombatText` | Texto de combate flotante estilo WoW: tu daño con el arma en blanco, el de habilidades en amarillo, el que recibes en rojo, curas en verde;  |
| `Gear.cs` | class `Gear` | Equipar y quitar piezas moviéndolas entre la bolsa y el personaje; recalcula los stats al momento. |
| `HudActionBar.cs` | class `HudActionBar` | Barra de acción inferior: Atacar con el arma (F), firma (Q) y hechizos 1-5 con enfriamiento radial y tooltip. |
| `HudFrames.cs` | class `HudFrames` | Marco del jugador (arriba a la izquierda: kit, nivel, vida, recurso y aguante) y barra de experiencia. |
| `HudMessages.cs` | class `HudMessages` | Textos del HUD: registro de combate, aviso de interacción (E), anuncio central (subida de nivel) y ayuda. |
| `HudMinimap.cs` | class `HudMinimap` | Minimapa (arriba a la derecha): cámara cenital ortográfica sobre el jugador, norte arriba, y nombre de la zona. |
| `HudTargetFrame.cs` | class `HudTargetFrame` | Marco del objetivo (junto al del jugador): nombre con el color de dificultad, nivel y vida en números. |
| `InventoryWindow.cs` | class `InventoryWindow` | Bolsa (B / I): 6 × 4 huecos con marco del color de rareza y tooltip; clic derecho consume o equipa. |
| `ItemView.Gear.cs` | class `ItemView` | Líneas de equipo de los tooltips, al estilo WoW: hueco, tipo y velocidad del arma, daño, DPS, armadura y bonos; y la comparación con lo que  |
| `ItemView.cs` | class `ItemView` | Presentación de objetos en la interfaz: «icono» (iniciales sobre color de categoría) y tooltip. |
| `MapDrawing.cs` | class `MapDrawing` | Dibuja el plano del poblado con rectángulos uGUI: zonas, caminos, casas, recursos y campamentos. |
| `MapWindow.cs` | class `MapWindow` | Mapa del mundo (M): el poblado dibujado desde el layout, con la posición y orientación del jugador. |
| `ProfessionWindow.Detail.cs` | class `ProfessionWindow` | Panel derecho de la ventana de profesión: objeto resultante, componentes y botón Fabricar. |
| `ProfessionWindow.cs` | class `ProfessionWindow` | Ventana de profesión (E en un banco), al estilo de WoW: lista de recetas a la izquierda (verde = puedes fabricarla), detalle con componentes |
| `SandboxUi.cs` | class `SandboxUi` | Interfaz del Campo de pruebas estilo MMO: HUD (marco, barra de acción, PX, minimapa, registro) y ventanas (bolsa, personaje, hechizos, talen |
| `SpellView.cs` | class `SpellView` | Presentación de hechizos: icono (iniciales sobre el color de su tipo de daño) y tooltip. |
| `SpellbookWindow.cs` | class `SpellbookWindow` | Libro de hechizos (P): los cinco hechizos del kit, con nivel de aprendizaje, tecla y descripción. |
| `TalentCell.cs` | class `TalentCell` | Un talento del árbol: icono, rango «x/y» y tooltip con el efecto actual y el del siguiente rango. |
| `TalentWindow.cs` | class `TalentWindow` | Talentos (N), al estilo de los árboles clásicos de WoW: tres ramas por kit, cinco filas; cada fila pide 5 puntos gastados en su rama. |
