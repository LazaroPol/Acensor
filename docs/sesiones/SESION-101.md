# Sesión 101 — AR-07 Mobiliario de profesiones

- **Fecha:** 2026-10-12 · **PR:** [#89](https://github.com/MenaDeFerro/Project-Especimen/pull/89) · **Estado:** ✅ cerrada

## Qué se hizo
- Núcleo `ProfessionFurniture`: herramientas KayKit para cada una de las 7 profesiones y base del banco.
- Editor: `FurnitureStep` genera `Banco_<Profesión>.prefab` (caja del pueblo como mesa + herramientas encima + BoxCollider) y lo registra en CatalogoArte.
## Decisiones
- Ningún pack trae mesas de trabajo: la base es la caja del pueblo escalada a 1,4 × 0,8 × 0,7 m.
## Calidad
- 3 tests NUnit nuevos (1011 en total) en verde.
## Siguiente sesión
**AR-08** Campo de pruebas jugable.
