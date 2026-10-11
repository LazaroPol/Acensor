#!/usr/bin/env bash
# Punto de control: ¿hay en main (lo que ves en el tablero) una urgente que interrumpa a la tarea en curso?
# Uso: tools/session/check_urgent.sh <TAREA_EN_CURSO>
#   imprime la urgente y sale 0 → hay que pausar con tools/session/pause.sh
#   sale 1 → no hay urgentes, se sigue con la tarea
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/../.." && pwd)"
TASK="${1:?Falta la tarea en curso}"
TMP="$(mktemp -d)"
trap 'rm -rf "$TMP"' EXIT

git -C "$ROOT" fetch -q origin "+refs/heads/main:refs/remotes/origin/main"
git -C "$ROOT" archive origin/main docs/backlog | tar -x -C "$TMP"
cd "$ROOT"
python3 -m tools.progress --urgent "$TASK" --dir "$TMP/docs/backlog"
