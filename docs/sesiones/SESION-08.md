# Sesión 8 — F1-06 Estado Atrapado y Hueco de Emergencia

- **Fecha:** 2026-10-11 · **PR:** [#7](https://github.com/LazaroPol/Acensor/pull/7) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 16/73 · 21,9 %

## Qué se hizo
- Seguimiento de la ubicación de cada jugador durante la Incursión.
- Quien no vuelve a tiempo queda Atrapado; alcanzando el Hueco de Emergencia trepa hasta el ascensor (GDD §3.2).
- Los atrapados no bloquean el "Listo" de la Calma siguiente.

## Decisiones
- El rescate es inmediato al alcanzar el hueco: el ascensor ya está en el piso siguiente cuando se queda atrapado.
- Los modificadores de enemigos se definen ya como constantes de dominio; los aplicará la IA en F6-03.
- La fatiga extra del rescate se deja para F2-03, cuando exista el modelo de recursos del jugador.
- `IterationRun` pasa a clase parcial para respetar el límite de tamaño de ficheros.

## Calidad
- 12 tests NUnit nuevos (121 en total) y 44 tests Python en verde.

## Siguiente sesión
**F1-07** Fin de iteración (victoria, todos caídos, abandono).
