# SPECIMEN — instrucciones para Claude

Juego coop en Unity 6. Núcleo C# puro en `Assets/_Project/Scripts/Core` (sin `UnityEngine`), tests en
`Assets/_Project/Tests/EditMode/Core`. Docs en `docs/`. Idioma: español.

## Regla de oro: una sesión = una tarea
Cada chat hace **solo la siguiente tarea** del backlog y al terminar programa el chat siguiente.
Runbook completo: [`docs/sesiones/RUNBOOK.md`](docs/sesiones/RUNBOOK.md).

## Ahorro de tokens (obligatorio)
- No leas el repo entero. Empieza por `python3 -m tools.progress --next` y la página de su fase en
  `docs/backlog/fases/<FASE>.md`; del GDD lee **solo** la sección que toque (`docs/gdd/README.md` es el índice).
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
- Fases en orden; como mucho una tarea en curso.
