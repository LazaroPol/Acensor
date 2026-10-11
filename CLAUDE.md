# SPECIMEN — instrucciones para Claude

Juego coop en Unity 6. Núcleo C# puro en `Assets/_Project/Scripts/Core` (sin `UnityEngine`), tests en
`Assets/_Project/Tests/EditMode/Core`. Docs en `docs/`. Idioma: español.

## Regla de oro: una sesión = una tarea
Cada chat hace **solo la siguiente tarea** del backlog y al terminar programa el chat siguiente.
Runbook completo: [`docs/sesiones/RUNBOOK.md`](docs/sesiones/RUNBOOK.md).

## Ahorro de tokens (obligatorio)
- No leas el repo entero. Empieza por `python3 -m tools.progress --next` y su tarjeta
  `docs/backlog/tareas/<ID>.json` (descripción y criterios); del GDD lee **solo** la sección que toque (`docs/gdd/README.md` es el índice).
- **Mapa del código**: [`docs/arquitectura/README.md`](docs/arquitectura/README.md) (C4) y [`MODULOS.md`](docs/arquitectura/MODULOS.md) (índice
  por módulo, generado). Localiza el módulo y lee solo su página en `modulos/`. Si añades/mueves tipos: `python3 -m tools.arch`.
- Lee solo los ficheros que vas a tocar o de los que dependes directamente (usa `grep -rl` para encontrarlos).
- No vuelvas a leer un fichero que acabas de escribir; no imprimas ficheros completos en la terminal.
- Filtra las salidas: `tools/verify.sh 2>&1 | grep -E "error|FAIL|Test Count|VERIF"`.
- Resúmenes de sesión y descripciones de PR: cortos (plantilla del runbook).
- No publiques artifacts ni generes informes extra salvo al cerrar una fase.
- No uses subagentes.

## Reglas de código (las imponen los tests)
- Ningún fichero de texto supera 120 líneas: divide en clases/ficheros pequeños (clases parciales si hace falta).
- SOLID: interfaces pequeñas, inyección por constructor, estados/estrategias en clases propias.
- Warnings = errores. Todo cambio de lógica lleva tests NUnit.
- Manda la cola de prioridad del tablero (`docs/tablero.md`); como mucho una tarea en curso.

## Git y PRs
- Antes de tocar una rama, comprueba si necesita rebase sobre `main`; al terminar, `checkout main` y `pull`.
- Si la CI pasa, mergea sin preguntar y activa el automerge en cada PR.
- Si cambian módulos, regenera `docs/arquitectura` con `python -m tools.arch` (la CI ejecuta `--check`).

## Flujo de trabajo con git y PRs
- Responde siempre en español.
- Antes de tocar una rama, `git fetch` y comprueba si necesita rebase sobre `main` (en ramas propias; en PRs ajenos ya publicados, merge).
- Si cambias módulos/tipos, regenera el índice: `python3 -m tools.arch` (CI falla si está obsoleto).
- Si la CI pasa, mergea sin preguntar; activa auto-merge en cada PR.
- Al terminar, vuelve a `main` y haz pull (`git checkout main && git pull --ff-only`).
