# Sesión 3 — F1-01 Configuración de iteración

- **Fecha:** 2026-10-11 · **PR:** [#2](https://github.com/LazaroPol/Acensor/pull/2) · **Estado:** ✅ cerrada
- **Progreso al cierre:** 11/73 · 15,1 %

## Qué se hizo
- `IterationConfig` (núcleo): pisos por acto, duración de Calma e Incursión, avisos de cierre de puertas,
  oleadas del asedio y máximo de jugadores. Inmutable y validada al construirse.
- `IterationConfigBuilder`: valores por defecto del GDD §3.1 y sobrescritura fluida para tests y modos de juego.
- `tools/session/close.py`: el cierre de sesión (backlog + fila del historial) es un solo comando.

## Decisiones
- La validación vive en el constructor y el builder solo acumula valores (SRP); así una config inválida no puede existir.
- Los avisos se normalizan (orden descendente, sin duplicados) para que el temporizador de F1-02 los recorra en orden.

## Calidad
- 17 tests NUnit nuevos (57 en total) y 44 tests Python. Un test mal planteado (Incursión de 20 s con aviso a 60 s) lo detectó la propia validación antes de abrir la PR.

## Siguiente sesión
**F1-02** Temporizador de fase con avisos (60 s / 15 s).
