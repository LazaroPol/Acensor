# Runbook de una sesión automática

Cada sesión es un chat nuevo que hace **una tarea** y programa el siguiente. Pasos:

## 1. Preparar (sin leer de más)
```bash
git clone https://github.com/LazaroPol/Acensor /home/claude/acensor && cd /home/claude/acensor
git config user.name "Lazaro (via Claude)"; git config user.email "pol.lazaro.rosas@gmail.com"
command -v dotnet || sudo apt-get install -y dotnet-sdk-8.0 >/dev/null
tools/session/should_continue.sh   # si falla: no hay tarea o la cadena está en pausa → terminar
```
`should_continue.sh` imprime la tarea (`F2-01`), el número de sesión y la fase.

## 2. Implementar
```bash
git checkout -b feature/<TAREA>-<slug>
python3 -m tools.progress --start <TAREA>
```
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

- **Fecha:** AAAA-MM-DD · **PR:** [#{PR}](https://github.com/LazaroPol/Acensor/pull/{PR}) · **Estado:** ✅ cerrada

## Qué se hizo
- …
## Decisiones
- …
## Calidad
- N tests NUnit nuevos (T en total) en verde.
## Siguiente sesión
**<SIGUIENTE TAREA>** <título>.
```
