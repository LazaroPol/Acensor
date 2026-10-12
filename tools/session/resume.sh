#!/usr/bin/env bash
# Retoma una tarea «En pausa» desde su rama guardada: la pasa a «En curso» en el tablero (main)
# y trae main encima de la rama.
# Uso: tools/session/resume.sh <TAREA>
source "$(dirname "$0")/../git/common.sh"
TASK="${1:?Falta la tarea a retomar}"
cd "$ROOT"

BRANCH="$(python3 -c 'import json, sys; print(json.load(open(sys.argv[1])).get("branch") or "")' \
  "docs/backlog/tareas/$TASK.json")"
[ -n "$BRANCH" ] || die "$TASK no tiene rama guardada: empiézala de cero (checkout -b + --start)"

git checkout -q main
"$ROOT/tools/session/board_status.sh" "$TASK" in_progress
DEPTH="${RESUME_FETCH_DEPTH-200}"  # clones superficiales: historia suficiente para el merge
git fetch -q ${DEPTH:+--depth=$DEPTH} origin "+refs/heads/main:refs/remotes/origin/main" \
  "+refs/heads/$BRANCH:refs/remotes/origin/$BRANCH"
git checkout -q -B "$BRANCH" "origin/$BRANCH"
git merge -q --no-edit -m "merge: main en $BRANCH al retomar $TASK" origin/main
echo "▶️  $TASK retomada en $BRANCH"
