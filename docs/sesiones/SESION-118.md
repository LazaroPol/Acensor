# Sesión 118 — OF-16 Mods, conocimiento y guardado en el Campo de pruebas + guía de prueba

- **Fecha:** 2026-10-12 · **PR:** [#120](https://github.com/MenaDeFerro/Project-Especimen/pull/120) · **Estado:** ✅ cerrada

## Qué se hizo
- Módulo `Core.Crafting.Play`: lectura de conocimientos desde la bolsa, banco de mods por instancia, botín de oficio y foto/carga del progreso.
- Restauración sin coste en oficios, estaciones, cola y piezas; la factoría da una estación por tipo.
- Campo de pruebas: clic derecho lee, clic izquierdo abre la ventana de mods, botín de oficio, autoguardado en PlayerPrefs.
- Guía manual `docs/oficios/PRUEBA.md`. Cierra la fase OF.
## Decisiones
- Las ranuras de estación se guardan como inventarios (`Fragua.in/out/fuel`) para conservar la calidad de la salida.
- Los trabajos restaurados cuentan como empezados (no se pueden cancelar: ya se pagaron).
- Un guardado vacío no toca la bolsa; solo se guarda la bolsa (no lo equipado).
## Calidad
- 19 tests NUnit nuevos (1424 en total) en verde.
## Siguiente sesión
**F11-01** Integración de Netcode for GameObjects.
