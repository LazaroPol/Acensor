<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Networking`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Networking`
Tests: `Assets/_Project/Tests/EditMode/Core/Networking`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Runtime.Networking](Runtime.Networking.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `INetworkSession.cs` | interface `INetworkSession` | Estado de red visible para el juego: rol actual y autoridad. |
| `INetworkTransport.cs` | interface `INetworkTransport` | Puerto hacia la librería de red (NGO en Runtime). |
| `NetworkRole.cs` | enum `NetworkRole` | Papel de esta instancia en la sesión de red. |
| `NetworkRole.cs` | class `NetworkAuthority` | Reglas de autoridad: el servidor (o la partida en solitario) es quien muta el estado. |
| `NetworkSession.cs` | class `NetworkSession` | Ciclo de vida de la sesión sobre un : arranca como host, servidor o cliente una sola vez, vuelve a Offline si el transporte falla o al apaga |
| `SessionEndpoint.cs` | class `SessionEndpoint` | Dirección y puerto de la sesión (validados). |
| `SessionSettings.cs` | class `SessionSettings` | Ajustes de una sesión: dónde y para cuántos jugadores (coop 1-6). |
