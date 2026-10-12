<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Prototype`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Prototype`
Tests: `Assets/_Project/Tests/EditMode/Core/Prototype`

**Depende de:** [Core.Common](Core.Common.md), [Core.Physics](Core.Physics.md), [Core.Time](Core.Time.md), [Core.Visuals](Core.Visuals.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Core.Floors](Core.Floors.md), [Core.Hud](Core.Hud.md), [Runtime.Atico](Runtime.Atico.md), [Runtime.Prototype](Runtime.Prototype.md), [Runtime.Visuals](Runtime.Visuals.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DebugCommands.cs` | class `DebugCommands` | Acciones de las teclas de depuración del prototipo: dañar, curar y avanzar el tiempo de fase. |
| `DebugHudPresenter.cs` | class `DebugHudPresenter` | Texto del HUD de depuración (PG-04): vida, recurso de clase, Aguante, estado vital y fase con su tiempo restante. |
| `GreyboxRoomLayout.cs` | class `GreyboxRoomLayout` | Sala simple en cuadrícula de 1 m (GDD 16, PG-02): suelo en cada celda, muros en el perímetro, una puerta en el centro del muro sur y el juga |
| `HudTime.cs` | class `HudTime` | Formato de tiempo del HUD de depuración: mm:ss, redondeando hacia arriba (00:01 hasta expirar). |
| `ILevelLayout.cs` | interface `ILevelLayout` | Estrategia que describe un nivel como lista de visuales a instanciar. |
| `LevelColliders.cs` | class `LevelColliders` | Genera los colliders de muro a partir del layout (PG-06), independientes del modelo visual: un bloque de 1 m de largo por muro, con grosor y |
| `LevelPlacement.cs` | class `LevelPlacement` | Un visual del nivel: ID lógico, posición del pivote en metros y giro en Y (grados). |
