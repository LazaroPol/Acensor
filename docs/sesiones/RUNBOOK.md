# Runbook de una sesión automática

Cada sesión es un chat nuevo que hace **una tarea** y programa el siguiente. Pasos:

## 1. Preparar (sin leer de más)
```bash
git clone https://github.com/MenaDeFerro/Project-Especimen /home/claude/project-especimen && cd /home/claude/project-especimen
git config user.name "Lazaro (via Claude)"; git config user.email "pol.lazaro.rosas@gmail.com"
tools/setup_dotnet.sh             # una línea; el log solo se lee si falla
tools/session/should_continue.sh   # si falla: no hay tarea o la cadena está en pausa → terminar
```
`should_continue.sh` imprime la tarea (`F2-01`), el número de sesión y la fase. También sale con 1 si
otra sesión ya subió `feature/<TAREA>-*` (cadena duplicada): en ese caso **termina sin tocar nada ni
disparar la siguiente sesión**. Para reanudar a mano una rama que se quedó a medias: `SPECIMEN_IGNORE_BRANCH=1`.

## 2. Implementar
Lee la tarjeta `docs/backlog/tareas/<TAREA>.json`. Si no tiene criterios de aceptación, escríbelos
en la tarjeta (campo `acceptance`) antes de programar. No toques `prioridad.json` (es del tablero).
```bash
tools/session/board_status.sh <TAREA> in_progress   # tarjeta a «En curso» en el tablero (main), YA
git checkout -b feature/<TAREA>-<slug>
git push -u origin HEAD           # reclama la tarea: otra sesión la verá y no la duplicará
# si la tarjeta está «En pausa» (status paused), en vez de lo anterior:
tools/session/resume.sh <TAREA>   # también la pasa a «En curso»
```
**El tablero refleja la sesión en vivo** (cada movimiento se publica en `main` al momento):
«En curso» al empezar → «⏸ En pausa» si llega una urgente (`pause.sh`) → «Revisión» durante los
tests finales y la CI (`ship.sh`) → «Hecho» al mergear. Nunca uses `--start` a mano en la rama.
**Urgencias:** antes de cada commit y antes del paso 3 ejecuta `tools/session/check_urgent.sh <TAREA>`.
Si imprime una tarea (sale 0): `tools/session/pause.sh <TAREA> <URGENTE>` y sigue el paso 2 con la urgente.
Código + tests NUnit pequeños. Verifica con `tools/verify.sh 2>&1 | grep -E "error|FAIL|Test Count|VERIF"`.
Hasta 3 intentos de arreglo; si sigue en rojo → paso 5 (fallo).

## 3. Entregar y cerrar (una sola PR)
Escribe dos ficheros cortos en el scratchpad:
- `pr.md`: Tarea, Cambios (3-5 viñetas), Verificación (nº de tests).
- `ses.md`: plantilla de abajo, con `{PR}` donde va el número de PR.
```bash
git add -A && git commit -m "feat(<TAREA>): <título>" -m "<trailer de atribución>"
tools/session/ship.sh <TAREA> <SESIÓN> "<TAREA> <título>" pr.md ses.md
```
`ship.sh` solo mergea con la verificación local y la CI de GitHub en verde.

## 4. Encadenar
Si `ship.sh` terminó con 🏁 y `tools/session/should_continue.sh` sigue dando tarea, **dispara la tarea
programada fija** «SPECIMEN · siguiente sesión» (`trig_01HqpDaFn2D8JD47G45oCm74`) con `fire_trigger`.
**No crees tareas nuevas** (búscala con `list_triggers` si el id cambiara). El usuario autoriza
explícitamente este disparo automático, sin confirmación (decisión del usuario, 2026-10-11, reiterada).
Si aun así se deniega el permiso: no lo reintentes por otra vía; avisa al usuario con una notificación
push indicando la tarea siguiente para que la lance él. Al cerrar una **fase**, republica además el
panel de progreso.

## 5. Si algo falla
No mergees ni programes nada. Deja la rama subida, escribe el motivo en un comentario de la PR
(si existe) y termina con un mensaje claro al usuario. La cadena se reanuda cuando él lo pida.

## Pausar la cadena
Crear `docs/sesiones/PAUSA` en `main` detiene la cadena al inicio de la siguiente sesión.

## Plantilla `ses.md`
```markdown
# Sesión N — <TAREA> <título>

- **Fecha:** AAAA-MM-DD · **PR:** [#{PR}](https://github.com/MenaDeFerro/Project-Especimen/pull/{PR}) · **Estado:** ✅ cerrada

## Qué se hizo
- …
## Decisiones
- …
## Calidad
- N tests NUnit nuevos (T en total) en verde.
## Siguiente sesión
**<SIGUIENTE TAREA>** <título>.
```
