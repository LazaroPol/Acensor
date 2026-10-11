#!/usr/bin/env bash
# Retoma una tarea «En pausa» desde su rama guardada: trae main encima y la marca en curso.
# Uso: tools/session/resume.sh <TAREA>
source "$(dirname "$0")/../git/common.sh"
TASK="${1:?Falta la tarea a retomar}"
cd "$ROOT"

BRANCH="$(python3 -c 'import json, sys; print(json.load(open(sys.argv[1])).get("branch") or "")' \
  "docs/backlog/tareas/$TASK.json")"
[ -n "$BRANCH" ] || die "$TASK no tiene rama guardada: empiézala de cero (checkout -b + --start)"

git fetch -q --depth=200 origin "+refs/heads/main:refs/remotes/origin/main" \
  "+refs/heads/$BRANCH:refs/remotes/origin/$BRANCH"
git checkout -q -B "$BRANCH" "origin/$BRANCH"
git merge -q --no-edit -m "merge: main en $BRANCH al retomar $TASK" origin/main
python3 -m tools.progress --start "$TASK" >/dev/null
echo "▶️  $TASK retomada en $BRANCH"
