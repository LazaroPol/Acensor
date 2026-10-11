# Documentación de SPECIMEN

| Sección | Contenido |
|---|---|
| [Diseño (GDD)](gdd/README.md) | El juego, dividido en una página por sección |
| [Progreso](backlog/PROGRESO.md) | Barra de % total y por fase, siguiente tarea |
| [Backlog por fases](backlog/PROGRESO.md) | Una página por fase en `backlog/fases/` |
| [Tablero kanban](tablero.md) | Web local para priorizar y añadir tareas (`python3 -m tools.board`) |
| [Historial de sesiones](sesiones/README.md) | Resumen de cada sesión finalizada |
| [Assets y arte](arte/ASSETS.md) | Dónde dejar modelos/texturas y cómo se asignan al juego |
| [Flujo de trabajo](workflow.md) | Ramas, PRs, tests y cierre de sesión |
| [Runbook de sesión automática](sesiones/RUNBOOK.md) | Lo que hace cada chat encadenado |
| [Histórico](historico/) | Registros de la etapa previa a GitHub |

Los ficheros de `backlog/` terminados en `.md` se generan con `python3 -m tools.progress`; la fuente de verdad son las tarjetas `backlog/tareas/*.json` y la cola `backlog/prioridad.json`.
