# SPECIMEN

Roguelite cooperativo de supervivencia y extracción en Unity. Sois clones que ascienden una torre-laberinto en un ascensor con temporizador: preparad en el refugio, saquead cada piso y volved antes de que las puertas se cierren.

- Diseño: [`docs/GDD.md`](docs/GDD.md)
- Backlog: [`docs/BACKLOG.md`](docs/BACKLOG.md) · Progreso: [`docs/PROGRESS.md`](docs/PROGRESS.md)
- Historial de sesiones: [`docs/HISTORIAL.md`](docs/HISTORIAL.md)

## Flujo de trabajo

1. Cada tarea del backlog se hace en una rama `feature/<ID>-<slug>`.
2. `tools/pr.sh "<título>"` abre la PR: ejecuta **todas** las verificaciones, registra la PR en `docs/pull-requests/` y solo mergea a `main` si todo está en verde. Tras el merge vuelve a verificar `main` y revierte si algo falla.
3. Una fase del backlog = una sesión de trabajo. Al cerrar la sesión se escribe su informe en `docs/sessions/`.
