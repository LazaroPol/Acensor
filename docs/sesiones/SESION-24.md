# Sesión 24 — PG-02 Bootstrap del prototipo: escena Prototipo y nivel greybox desde el catálogo

- **Fecha:** 2026-10-11 · **PR:** [#32](https://github.com/MenaDeFerro/Project-Especimen/pull/32) · **Estado:** ✅ cerrada

## Qué se hizo
- `Core.Prototype`: sala greybox en cuadrícula de 1 m (suelo, muros, puerta, jugador) como lista de colocaciones.
- `Core.Visuals`: pivote por ID y desplazamiento de la primitiva greybox.
- Runtime/Editor: catálogo ScriptableObject, spawner, bootstrap autoinstalable y menú para crear la escena.
## Decisiones
- La escena no se versiona a mano: la crea el menú `SPECIMEN › Crear escena Prototipo`.
- Muros con giro 0 → +X y 270 → +Z, pivote en esquina inferior (ASSETS.md).
- Sin `gh` válido ni repo nuevo en la sesión: se entregó por el alias `lazaropol/acensor` y la MCP de GitHub.
## Calidad
- 20 tests NUnit nuevos (369 en total) en verde.
## Siguiente sesión
**PG-03** Jugador y cámara en tercera persona (Input System).
