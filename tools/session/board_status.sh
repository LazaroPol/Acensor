#!/usr/bin/env bash
# Mueve la tarjeta de una tarea en el tablero publicándolo YA en main (solo docs/backlog),
# para que el tablero refleje el trabajo mientras ocurre y no solo al mergear la PR:
#   in_progress al empezar · review antes de los tests finales · (paused lo publica pause.sh)
# No toca la copia de trabajo: usa un worktree temporal sobre origin/main. Después sincroniza
# la rama actual con main (main: pull --ff-only; rama de tarea: merge de origin/main).
# Uso: tools/session/board_status.sh <TAREA> <estado>
source "$(dirname "$0")/../git/common.sh"
TASK="${1:?Falta la tarea}"
STATUS="${2:?Falta el estado}"
REMOTE="${SPECIMEN_REMOTE:-origin}"
cd "$ROOT"
python3 -c 'import sys; from tools.progress.rules import VALID_STATUS as V; sys.exit(sys.argv[1] not in V)' \
  "$STATUS" || die "Estado inválido: $STATUS"

publish() {
  local wt; wt="$(mktemp -d)"
  git fetch -q "$REMOTE" "+refs/heads/main:refs/remotes/$REMOTE/main"
  git worktree add -q --detach "$wt" "$REMOTE/main"
  local ok=1
  if (cd "$wt" && python3 -m tools.progress --status "$TASK" "$STATUS" >/dev/null && git add docs/backlog \
      && { git diff --cached --quiet \
           || git commit -q -m "chore(tablero): $TASK → $STATUS" -m "$TRAILER"; } \
      && git push -q "$REMOTE" HEAD:main); then ok=0; fi
  git worktree remove --force "$wt"
  return $ok
}

for attempt in 1 2 3; do
  publish && break
  [ "$attempt" -lt 3 ] || die "No se pudo publicar $TASK → $STATUS en main"
  sleep 3   # alguien empujó a main a la vez (p. ej. el tablero): reintento sobre el main nuevo
done

git fetch -q "$REMOTE" "+refs/heads/main:refs/remotes/$REMOTE/main"
if [ "$(current_branch)" = "main" ]; then
  git merge -q --ff-only "$REMOTE/main"
else
  git merge -q --no-edit -m "merge: tablero ($TASK → $STATUS)" "$REMOTE/main"
fi
echo "🗂️  $TASK → $STATUS (publicado en main)"
