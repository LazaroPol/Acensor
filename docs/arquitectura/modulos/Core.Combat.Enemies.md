<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Combat.Enemies`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Combat/Enemies`
Tests: `Assets/_Project/Tests/EditMode/Core/Combat/Enemies`

**Depende de:** [Core.Abilities](Core.Abilities.md), [Core.Common](Core.Common.md), [Core.Loot](Core.Loot.md)
**Lo usan:** [Core.Bestiary](Core.Bestiary.md), [Core.Combat.Bosses](Core.Combat.Bosses.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DepthScalingConfig.cs` | class `DepthScalingConfig` | Ritmos de crecimiento (fracción por paso): por piso para vida, daño y armadura, y un bonus por anillo de profundidad que se aplica a vida y  |
| `Enemy.cs` | class `Enemy` | Enemigo concreto ya escalado a su profundidad, listo para el combate. |
| `EnemyAction.cs` | enum `EnemyAction` | Decisión de la IA de un enemigo en un instante. |
| `EnemyArchetype.cs` | class `EnemyArchetype` | Plantilla de enemigo: identificador, estadísticas en piso 1 / anillo 1, tipo de daño y resistencias. |
| `EnemyBrain.cs` | class `EnemyBrain` | IA cuerpo a cuerpo: espera hasta ver al objetivo dentro del agro, lo persigue, ataca al alcance respetando el enfriamiento y lo suelta si se |
| `EnemyBrainConfig.cs` | class `EnemyBrainConfig` | Distancias (m) y ritmo de la IA cuerpo a cuerpo: agro, abandono, alcance y enfriamiento. |
| `EnemyDepth.cs` | class `EnemyDepth` | Dónde aparece un enemigo: número de piso (>= 1) y anillo de profundidad (1-3). |
| `EnemyFactory.cs` | class `EnemyFactory` | Crea enemigos aplicando la estrategia de escalado inyectada al arquetipo. |
| `EnemyHealth.cs` | class `EnemyHealth` | Vida de un enemigo en combate: nunca baja de 0 y avisa de su muerte una sola vez. |
| `EnemyStats.cs` | class `EnemyStats` | Estadísticas numéricas de un enemigo: vida máxima, daño por golpe y armadura. |
| `IDepthScaling.cs` | interface `IDepthScaling` | Estrategia que ajusta las estadísticas base de un enemigo según su profundidad. |
| `LinearDepthScaling.cs` | class `LinearDepthScaling` | Escalado lineal: stat = base × (1 + ritmo × pasos de piso) × (1 + bonus × pasos de anillo). |
