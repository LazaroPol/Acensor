#!/usr/bin/env bash
# Utilidades compartidas por los scripts de PR.
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
REPO="${SPECIMEN_REPO:-MenaDeFerro/Project-Especimen}"
TRAILER=$'Co-Authored-By: Claude Opus 5.5 <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01TAZqtMuKi59Z6RaTqSmwBL'
PR_FOOTER=$'\n\n🤖 Generated with [Claude Code](https://claude.com/claude-code)\n\nhttps://claude.ai/code/session_01TAZqtMuKi59Z6RaTqSmwBL'

current_branch() { git -C "$ROOT" rev-parse --abbrev-ref HEAD; }

die() { echo "❌ $*" >&2; exit 1; }

require_feature_branch() {
  [ "$(current_branch)" != "main" ] || die "No se trabaja directamente en main"
  [ -z "$(git -C "$ROOT" status --porcelain --untracked-files=no)" ] || die "Hay cambios sin commitear"
}

verify_or_die() {
  local log; log="$(mktemp)"
  if ! "$ROOT/tools/verify.sh" >"$log" 2>&1; then
    tail -40 "$log"; die "La verificación local falla: $1"
  fi
  echo "✅ Verificación local OK ($1)"
}
