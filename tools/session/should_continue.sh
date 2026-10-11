#!/usr/bin/env bash
# ¿Debe la cadena de sesiones continuar? Sale con 0 e imprime "TAREA SESIÓN FASE" si sí.
# Sale con 1 si la cadena está en pausa o no quedan tareas.
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/../.." && pwd)"
cd "$ROOT"

if [ -f docs/sesiones/PAUSA ]; then
  echo "⏸️  Cadena en pausa (docs/sesiones/PAUSA)" >&2
  exit 1
fi

NEXT="$(python3 - <<'PY'
import sys
from tools.progress import model

backlog = model.load()
summary = model.summarize(backlog)
task = summary["next_task"]
if not task:
    print("🏁 No quedan tareas", file=sys.stderr)
    sys.exit(1)
print(task, model.last_session(backlog) + 1, task.split("-")[0])
PY
)"
read -r TASK _ _ <<<"$NEXT"

# Evita cadenas duplicadas: si otra sesión ya subió la rama de esta tarea, esta no la repite.
# (Tareas en pausa o reanudaciones a mano: SPECIMEN_IGNORE_BRANCH=1.)
STATUS="$(python3 -c "import json;print(json.load(open('docs/backlog/tareas/$TASK.json'))['status'])")"
if [ "${SPECIMEN_IGNORE_BRANCH:-0}" != 1 ] && [ "$STATUS" != paused ] \
   && git ls-remote --exit-code --heads origin "feature/$TASK-*" >/dev/null 2>&1; then
  echo "👥 Otra sesión ya trabaja en $TASK (rama remota feature/$TASK-*): no se duplica" >&2
  exit 1
fi
echo "$NEXT"
