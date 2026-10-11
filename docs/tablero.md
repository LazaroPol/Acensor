# Tablero kanban del backlog

Web local tipo Jira para ver, priorizar y añadir tareas. Escribe directamente en los ficheros del
backlog del repo, así que las sesiones automáticas leen la misma prioridad que ves tú.

## Arrancar
```bash
git checkout main && git pull
python3 -m tools.board            # abre http://127.0.0.1:8765  (--port, --no-browser, --no-git)
```
Solo necesita Python 3 (biblioteca estándar). Ciérralo con Ctrl+C.

### Sin Python, con Docker
```bash
git checkout main && git pull
docker compose up board           # abre tú http://localhost:8765 ; parar: Ctrl+C
```
Monta el repo en el contenedor, así los cambios se guardan en tus ficheros. Dentro de Docker no hay
credenciales de Git, por eso los botones «Traer» y «Guardar y subir» se ocultan: haz commit y push de
`docs/backlog` con tu Git habitual (o GitHub Desktop) antes de lanzar la siguiente sesión.

## Columnas
| Columna | Estado | La coge una sesión |
|---|---|---|
| 💡 Ideas | `idea` | No: bandeja de entrada sin refinar (no cuenta en el %) |
| Listo | `todo` | **Sí: la de arriba del todo es la siguiente** |
| En curso | `in_progress` | La retoma si se quedó a medias (máximo una) |
| ⏸ En pausa | `paused` | Sí, justo después de las urgentes (interrumpida por una urgente) |
| Revisión | `review` | No: para probarla tú en Unity |
| Bloqueado | `blocked` | No |
| Hecho | `done` | Las cierran las sesiones con PR (las añadidas en el tablero puedes cerrarlas a mano) |

## Uso
- **Tarea rápida:** escribe el título arriba, elige fase, tipo, columna y si va arriba o abajo de la cola; Enter.
  Recibe el siguiente ID libre de su fase (p. ej. `PG-07`) y cuenta en el progreso de esa fase.
- **Prioridad:** arrastra las tarjetas. El orden de «Listo» es el orden en que se trabajan (`#1`, `#2`…).
- **Editar:** clic en la tarjeta → título, tipo, etiquetas, descripción y criterios de aceptación
  (un criterio por línea; la sesión los convierte en tests).
- **Urgente:** ponle la etiqueta `urgente` a una tarea de «Listo» y súbela. La sesión lo comprueba en sus
  puntos de control: hace commit WIP de lo que lleva en su rama, mueve esa tarea a «⏸ En pausa» (conserva
  su prioridad y su rama) y empieza la urgente. Al acabar las urgentes, la pausada es la siguiente.
- **Filtros:** texto, fase, etiqueta/tipo y «Ver hechas».
- **Sincronizar:** «⟳ Traer» hace `git pull --rebase`; «⇪ Guardar y subir» hace commit de `docs/backlog`
  y push a la rama actual. Sube tus cambios antes de lanzar la siguiente sesión.

## Ficheros (fuente de verdad)
| Fichero | Qué guarda |
|---|---|
| `docs/backlog/tareas/<ID>.json` | Una tarjeta por tarea: fase, título, estado, tipo, etiquetas, descripción, criterios, PR, sesión |
| `docs/backlog/prioridad.json` | La cola: orden de las tareas pendientes. Solo la toca el tablero |
| `docs/backlog/etiquetas.json` | Catálogo de tipos y etiquetas con su color (edítalo para añadir más) |
| `docs/backlog/fases/<F>.json` | Nombre y objetivo de cada fase |

Las sesiones solo modifican la tarjeta de su tarea, y el tablero solo la cola y las tarjetas que edites.
Así se evitan conflictos al mergear.
