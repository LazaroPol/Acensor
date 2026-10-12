<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Echoes.Voting`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Echoes/Voting`
Tests: `Assets/_Project/Tests/EditMode/Core/Echoes/Voting`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Lore](Core.Lore.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `EchoVote.cs` | class `EchoVote` | Voto de un Eco entre varias opciones (GDD §9): gana la más votada; en empate decide, entre los votantes de las opciones empatadas, el que te |
| `EchoVoteResult.cs` | struct `EchoVoteResult` | Resultado de un voto de Eco: opción ganadora y si hizo falta desempatar. |
| `IFragmentSource.cs` | interface `IFragmentSource` | Consulta los Fragmentos de Memoria de un jugador (criterio de desempate). |
