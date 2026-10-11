<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Physics`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Physics`
Tests: `Assets/_Project/Tests/EditMode/Core/Physics`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Prototype](Core.Prototype.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ColliderBox.cs` | class `ColliderBox` | Caja de colisión alineada a ejes: centro y tamaño en metros, con su capa. |
| `CollisionLayer.cs` | enum `CollisionLayer` | Capas de física del juego (PG-06); en Unity se mapean a capas con el mismo nombre. |
| `CollisionMatrix.cs` | class `CollisionMatrix` | Matriz simétrica que decide qué pares de capas colisionan (o se detectan). |
| `DefaultCollisionMatrix.cs` | class `DefaultCollisionMatrix` | Matriz por defecto (PG-06): los muros bloquean a jugadores, enemigos y clones; los triggers solo detectan jugadores; los jugadores no se blo |
