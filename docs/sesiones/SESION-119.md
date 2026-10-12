# Sesión 119 — F11-01 Integración de Netcode for GameObjects

- **Fecha:** 2026-10-12 · **PR:** [#121](https://github.com/MenaDeFerro/Project-Especimen/pull/121) · **Estado:** ✅ cerrada

## Qué se hizo
- Paquete NGO 2.4.4 en el manifiesto y referencias de ensamblado en Runtime.
- Módulo `Core.Networking` (roles, autoridad, endpoint, ajustes, `NetworkSession`).
- Adaptador `NgoNetworkTransport` con límite de jugadores por aprobación de conexión.
## Decisiones
- El núcleo no conoce NGO: la sesión habla con un puerto `INetworkTransport`; Offline cuenta como autoridad (partida en solitario).
- Aún no se registra en `GameComposition` ni se monta NetworkManager en escenas: llega con F11-03 (lobby).
## Calidad
- 23 tests NUnit nuevos (1447 en total) en verde.
## Siguiente sesión
**F11-02** Sincronización del estado de iteración (servidor autoritativo).
