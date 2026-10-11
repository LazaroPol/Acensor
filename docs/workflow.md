# Flujo de trabajo

## Una tarea = una sesión
1. Se toma la siguiente tarea del backlog (`python3 -m tools.progress --next`). Siempre en orden.
2. Se marca en curso (`--start <ID>`) en una rama `feature/<ID>-<slug>`.
3. Se implementa con tests (TDD cuando aplica). Principios SOLID; núcleo sin `UnityEngine`.
4. `tools/git/open_pr.sh` abre la PR en GitHub solo si `tools/verify.sh` pasa.
5. Se cierra la sesión **dentro de la misma PR**: `--done <ID> --pr '#N' --session S`,
   `docs/sesiones/SESION-SS.md` y su fila en `docs/sesiones/README.md`.
6. `tools/git/merge_pr.sh N` mergea solo si la verificación local y **todos los checks de CI** están en verde,
   y vuelve a verificar `main` tras el merge.
7. Se abre la siguiente sesión con la siguiente tarea, sin programarla.

## Reglas que imponen los tests
- Ningún fichero de texto supera 120 líneas: muchos ficheros pequeños.
- `Specimen.Core` no usa `UnityEngine` ni depende de `Specimen.Runtime`.
- Las fases se completan en orden y solo hay una tarea en curso.
- Cada sesión cerrada tiene informe y aparece en el historial; desde la sesión 2, una sola tarea por sesión.
- Las páginas generadas del backlog están al día.

## Verificación
`tools/verify.sh` = tests Python (arquitectura, proceso, herramientas) + compilación del núcleo con
warnings como errores + tests NUnit. En GitHub Actions se ejecuta lo mismo en cada PR; los tests EditMode
dentro de Unity se activan con la variable `UNITY_CI_ENABLED` y los secretos de licencia.
