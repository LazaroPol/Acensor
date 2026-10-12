<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Vitals`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Vitals`
Tests: `Assets/_Project/Tests/EditMode/Core/Vitals`

**Depende de:** [Core.Common](Core.Common.md), [Core.Stats](Core.Stats.md), [Core.Time](Core.Time.md)
**Lo usan:** [Core.Abilities](Core.Abilities.md), [Core.Combat.Attacks](Core.Combat.Attacks.md), [Core.Elevator](Core.Elevator.md), [Core.Hud](Core.Hud.md), [Core.Life](Core.Life.md), [Core.Movement](Core.Movement.md), [Core.Prototype](Core.Prototype.md), [Runtime.Atico](Runtime.Atico.md), [Runtime.Combat](Runtime.Combat.md), [Runtime.Incursion](Runtime.Incursion.md), [Runtime.Player](Runtime.Player.md), [Runtime.Prototype](Runtime.Prototype.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `AcceleratingRegen.cs` | class `AcceleratingRegen` | Regeneración que se acelera con la calma: recupera a ritmo base mientras el recurso se gasta y, pasados sin gastar, el ritmo sube linealment |
| `CharacterVitals.cs` | class `CharacterVitals` | Vida, recurso de clase y stamina de un personaje, con sus máximos ligados a la . |
| `Fatigue.cs` | class `Fatigue` | Fatiga acumulada entre pisos. |
| `FatigueConfig.cs` | class `FatigueConfig` | Parámetros de la fatiga: puntos por piso, extra si el jugador quedó atrapado, penalización de stamina máxima por punto (fracción) y tope de  |
| `ResourceKind.cs` | enum `ResourceKind` | Recursos consumibles del jugador (GDD §5). |
| `ResourcePool.cs` | class `ResourcePool` | Recurso consumible (vida, recurso de clase, stamina) cuyo máximo lo dicta un . |
| `ResourceRegen.cs` | class `ResourceRegen` | Regenera un a ritmo constante, tras una espera desde el último gasto (p. |
