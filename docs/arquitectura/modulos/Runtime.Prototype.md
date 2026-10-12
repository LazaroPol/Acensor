<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Prototype`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Prototype`
Tests: (sin carpeta propia)

**Depende de:** [Core.Movement](Core.Movement.md), [Core.Prototype](Core.Prototype.md), [Core.Stats](Core.Stats.md), [Core.Time](Core.Time.md), [Core.Visuals](Core.Visuals.md), [Core.Vitals](Core.Vitals.md), [Runtime.Visuals](Runtime.Visuals.md)
**Lo usan:** [Runtime.Incursion](Runtime.Incursion.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DebugHud.cs` | class `DebugHud` | HUD de depuración (IMGUI): pinta el texto de , avanza el temporizador de fase y atiende las teclas de depuración (F1 dañar, F2 curar, F3 ava |
| `PlayerMotor.cs` | class `PlayerMotor` | Mueve al jugador con un CharacterController según del núcleo. |
| `PrototypeBootstrap.cs` | class `PrototypeBootstrap` | Arranque de la escena Prototipo: construye la sala greybox desde el catálogo visual y monta el jugador con su cámara en tercera persona (PG- |
| `PrototypeInput.cs` | class `PrototypeInput` | Acciones del Input System creadas por código: mover (WASD/flechas/stick) y esprintar (Shift/L3). |
| `PrototypePlayer.cs` | class `PrototypePlayer` | Monta el jugador (visual del catálogo + CharacterController + motor) y su cámara. |
| `PrototypeScenery.cs` | class `PrototypeScenery` | Luz mínima si la escena no la trae (la cámara la pone ). |
| `ThirdPersonCamera.cs` | class `ThirdPersonCamera` | Coloca la cámara según del núcleo y mira al objetivo. |
