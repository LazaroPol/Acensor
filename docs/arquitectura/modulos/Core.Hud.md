<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Hud`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Hud`
Tests: `Assets/_Project/Tests/EditMode/Core/Hud`

**Depende de:** [Core.Common](Core.Common.md), [Core.Events](Core.Events.md), [Core.Iteration](Core.Iteration.md), [Core.Prototype](Core.Prototype.md), [Core.Time](Core.Time.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Runtime.Atico](Runtime.Atico.md), [Runtime.Hubs](Runtime.Hubs.md), [Runtime.Hud](Runtime.Hud.md), [Runtime.Incursion](Runtime.Incursion.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `GameHudPresenter.cs` | class `GameHudPresenter` | Modelo de vista del HUD de partida: temporizador de la fase, piso, barras de recursos del jugador local y avisos. |
| `HudBar.cs` | struct `HudBar` | Barra de un recurso del HUD: etiqueta, valores y fracción llena (0..1). |
| `HudNotice.cs` | enum `NoticeSeverity` |  |
| `HudNotice.cs` | struct `HudNotice` | Aviso temporal del HUD (texto ya listo para mostrar). |
| `HudNotices.cs` | class `HudNotices` | Cola de avisos del HUD: se alimenta de eventos de la iteración (puertas cerrándose, cambio de fase, nuevo piso) y los hace caducar con el ti |
| `PhaseLabels.cs` | class `PhaseLabels` | Nombres en español de las fases de la iteración para el HUD. |
