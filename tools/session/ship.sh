#!/usr/bin/env bash
# Entrega una tarea ya commiteada en su rama y cierra su sesión, todo en la misma PR:
#   1) abre la PR (verificación local)  2) marca la tarea y añade la fila al historial
#   3) escribe docs/sesiones/SESION-SS.md desde una plantilla ({PR} = número de PR)
#   4) mergea solo con CI en verde y re-verifica main.
# Uso: tools/session/ship.sh <TAREA> <SESIÓN> "<título PR>" <descripcion_pr.md> <plantilla_sesion.md>
source "$(dirname "$0")/../git/common.sh"
TASK="$1"; SESSION="$2"; TITLE="$3"; BODY="$4"; TEMPLATE="$5"

PR="$("$ROOT/tools/git/open_pr.sh" "$TITLE" "$BODY" | tail -1)"
[[ "$PR" =~ ^[0-9]+$ ]] || die "No se pudo abrir la PR"
echo "📬 PR #$PR abierta"

cd "$ROOT"
python3 -m tools.session.close "$TASK" "$PR" "$SESSION"
REPORT="docs/sesiones/$(printf 'SESION-%02d.md' "$SESSION")"
sed "s/{PR}/$PR/g" "$TEMPLATE" > "$REPORT"
git add -A
git commit -q -m "docs(sesión $SESSION): cierre de la sesión de $TASK" -m "$TRAILER"
"$ROOT/tools/git/merge_pr.sh" "$PR"
echo "🏁 Sesión $SESSION cerrada ($TASK, PR #$PR)"
