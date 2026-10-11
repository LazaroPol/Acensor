#!/usr/bin/env bash
# Pausa la tarea en curso porque ha llegado una urgente:
#   1) commit WIP de todo lo hecho en su rama (sin tocar el backlog) y push
#   2) PR corta a main con la tarjeta «En pausa» (+ rama guardada) para que se vea en el tablero
#   3) deja la sesión en main, lista para empezar la urgente
# Uso: tools/session/pause.sh <TAREA> <URGENTE>
source "$(dirname "$0")/../git/common.sh"
TASK="${1:?Falta la tarea a pausar}"
URGENT="${2:?Falta la tarea urgente}"
BRANCH="$(current_branch)"
[ "$BRANCH" != "main" ] || die "La tarea a pausar debe estar en su rama, no en main"
cd "$ROOT"

CARD="docs/backlog/tareas/$TASK.json"
SAVED="$(mktemp)"
BODY="$(mktemp)"
python3 -m tools.progress --pause "$TASK" --branch "$BRANCH" >/dev/null
cp "$CARD" "$SAVED"

# 1) WIP en la rama: el backlog vuelve a como estaba, así retomarla no da conflictos con main
git fetch -q origin "+refs/heads/main:refs/remotes/origin/main"
git checkout -q "$(git merge-base HEAD origin/main)" -- docs/backlog
git add -A
git commit -q --allow-empty -m "wip($TASK): en pausa por la urgente $URGENT" -m "$TRAILER"
git push -q -u origin "$BRANCH"

# 2) La pausa se publica en main con su propia PR
git checkout -q -B "chore/pausa-$TASK" origin/main
cp "$SAVED" "$CARD"
python3 -m tools.progress >/dev/null
git add docs/backlog
git commit -q -m "chore($TASK): en pausa por la urgente $URGENT" -m "$TRAILER"
printf '**Pausa:** %s pasa a «En pausa» porque llega la urgente %s.\n\nTrabajo a medias en la rama `%s`; se retoma justo después de la urgente.\n' \
  "$TASK" "$URGENT" "$BRANCH" > "$BODY"
PR="$("$ROOT/tools/git/open_pr.sh" "chore($TASK): en pausa por la urgente $URGENT" "$BODY" | tail -1)"
[[ "$PR" =~ ^[0-9]+$ ]] || die "No se pudo abrir la PR de la pausa"
"$ROOT/tools/git/merge_pr.sh" "$PR"
echo "⏸️  $TASK en pausa (rama $BRANCH, PR #$PR). Sigue con $URGENT."
