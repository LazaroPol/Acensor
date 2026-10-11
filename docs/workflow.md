# Flujo de trabajo

## Una tarea = una sesión
1. Se toma la siguiente tarea (`python3 -m tools.progress --next`): la primera de «Listo» en la cola de
   prioridad del [tablero](tablero.md). Se leen su tarjeta (`docs/backlog/tareas/<ID>.json`) y sus criterios.
2. Se marca en curso (`--start <ID>`) en una rama `feature/<ID>-<slug>`.
   Si la siguiente está «En pausa», se retoma desde su rama con `tools/session/resume.sh <ID>`.
3. Se implementa con tests (TDD cuando aplica). Principios SOLID; núcleo sin `UnityEngine`.
   Antes de cada commit y de entregar, `tools/session/check_urgent.sh <ID>` mira si en `main` hay una
   «Listo» con etiqueta `urgente`; si la hay, `tools/session/pause.sh <ID> <URGENTE>` deja la tarea en pausa
   y se empieza la urgente.
4. `tools/git/open_pr.sh` abre la PR en GitHub solo si `tools/verify.sh` pasa.
5. Se cierra la sesión **dentro de la misma PR** con `tools/session/ship.sh` (marca la tarea, historial y resumen),
   que genera `docs/sesiones/SESION-SS.md` y su fila en `docs/sesiones/README.md`.
6. `tools/git/merge_pr.sh N` mergea solo si la verificación local y **todos los checks de CI** están en verde,
   y vuelve a verificar `main` tras el merge.
7. La sesión programa el chat siguiente (una tarea programada de una sola ejecución) según [el runbook](sesiones/RUNBOOK.md).

## Reglas que imponen los tests
- Ningún fichero de texto supera 120 líneas: muchos ficheros pequeños.
- `Specimen.Core` no usa `UnityEngine` ni depende de `Specimen.Runtime`.
- La cola `prioridad.json` contiene todas las tareas pendientes y solo hay una tarea en curso.
- Cada sesión cerrada tiene informe y aparece en el historial; desde la sesión 2, una sola tarea por sesión.
- Las páginas generadas del backlog están al día.

## Verificación
`tools/verify.sh` = tests Python (arquitectura, proceso, herramientas) + compilación del núcleo con
warnings como errores + tests NUnit. En GitHub Actions se ejecuta lo mismo en cada PR; los tests EditMode
dentro de Unity se activan con la variable `UNITY_CI_ENABLED` y los secretos de licencia.
