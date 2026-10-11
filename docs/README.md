# Documentación de SPECIMEN

| Sección | Contenido |
|---|---|
| [Diseño (GDD)](gdd/README.md) | El juego, dividido en una página por sección |
| [Arquitectura (C4 + índice de módulos)](arquitectura/README.md) | Mapa del código para leer solo lo necesario |
| [Progreso](backlog/PROGRESO.md) | Barra de % total y por fase, siguiente tarea |
| [Backlog por fases](backlog/PROGRESO.md) | Una página por fase en `backlog/fases/` |
| [Historial de sesiones](sesiones/README.md) | Resumen de cada sesión finalizada |
| [Assets y arte](arte/ASSETS.md) | Dónde dejar modelos/texturas y cómo se asignan al juego |
| [Flujo de trabajo](workflow.md) | Ramas, PRs, tests y cierre de sesión |
| [Runbook de sesión automática](sesiones/RUNBOOK.md) | Lo que hace cada chat encadenado |
| [Histórico](historico/) | Registros de la etapa previa a GitHub |

Los ficheros de `backlog/` terminados en `.md` se generan con `python3 -m tools.progress`; `arquitectura/MODULOS.md` y `arquitectura/modulos/` con `python3 -m tools.arch`; la fuente de verdad son los `.json`.
