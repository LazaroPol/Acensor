# 16. Prototipo greybox (fase PG)

Objetivo: poder pulsar **Play en Unity** y mover un personaje por un nivel de cubos con un HUD de texto,
usando la lógica del núcleo ya hecha (F1–F3). Se hace antes de F4 para validar pronto que el juego se
siente bien. F10 construirá encima de esto (no lo tira).

Principio clave: **nada visual se referencia directamente**. Todo pasa por el catálogo visual, para que
sustituir cubos por los assets reales del usuario sea arrastrar prefabs, sin tocar código.
Convenciones de assets: [`docs/arte/ASSETS.md`](../arte/ASSETS.md).

## PG-01 · Catálogo visual (núcleo puro)
- `Core/Visuals/VisualId.cs`: IDs lógicos estables (`Jugador`, `Suelo`, `Muro`, `Puerta`, `Ascensor`,
  `Loot`, `Enemigo`…). Son el contrato con los assets: no se renombran.
- `Core/Visuals/IVisualCatalog` + `VisualResolution`: dado un ID devuelve "asset asignado" o
  "fallback greybox" (primitiva + color + escala por defecto). Tests NUnit de la resolución.

## PG-02 · Bootstrap del prototipo
- Escena `Assets/_Project/Scenes/Prototipo/Prototipo.unity` con un único `PrototypeBootstrap`.
  Si crear la escena a mano es frágil, el bootstrap puede construirlo todo por código
  (`[RuntimeInitializeOnLoadMethod]` filtrado por nombre de escena) y la escena queda casi vacía.
- `CatalogoVisual` (ScriptableObject) en `Assets/_Project/Data/Visuals/`: lista ID → prefab.
  Vacío por defecto ⇒ todo sale en greybox.
- `VisualSpawner` (MonoBehaviour fino) instancia por ID usando el catálogo. Nivel: suelo + muros de
  una sala simple en cuadrícula de 1 m.

## PG-03 · Jugador y cámara
- `PlayerMotor` con `CharacterController` + Input System (WASD/stick, sprint consume Aguante del
  núcleo). Cámara tercera persona sencilla. Visual del jugador por catálogo (`VisualId.Jugador`).

## PG-04 · HUD de depuración
- Presentador **puro C#** (testeado) que formatea vida, recursos y temporizador de fase del núcleo.
- `DebugHud` (uGUI o IMGUI) solo pinta ese texto. Teclas de debug: dañar, curar, avanzar tiempo.

## PG-05 · Guía de prueba manual
- Añadir a `docs/arte/ASSETS.md` la sección "Cómo probar": abrir el proyecto, escena Prototipo, Play,
  qué debe verse. Checklist para cuando el usuario entregue assets.

## Limitación conocida
El CI solo compila el núcleo (dotnet). Los MonoBehaviours de PG-02…04 no se compilan en CI: deben ser
mínimos, toda la lógica va al núcleo con tests, y el usuario confirma en Unity al terminar la fase.
