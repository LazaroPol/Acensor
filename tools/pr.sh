#!/usr/bin/env bash
# Abre una "pull request" local de la rama actual contra main.
# Solo mergea si tools/verify.sh pasa en la rama Y de nuevo en main tras el merge.
# Uso: tools/pr.sh "Título de la PR" "Descripción breve"
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$ROOT"

TITLE="${1:?Falta el título}"
BODY="${2:-}"
BRANCH="$(git rev-parse --abbrev-ref HEAD)"
TRAILER=$'Co-Authored-By: Claude Opus 5.5 <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01TAZqtMuKi59Z6RaTqSmwBL'

[ "$BRANCH" = "main" ] && { echo "❌ No se abren PRs desde main"; exit 1; }
[ -n "$(git status --porcelain --untracked-files=no)" ] && { echo "❌ Hay cambios sin commitear"; exit 1; }

mkdir -p docs/pull-requests
NUM=$(printf "%03d" $(( $(ls docs/pull-requests 2>/dev/null | grep -c '^PR-') + 1 )))
LOG="$(mktemp)"

echo "🔎 PR-$NUM ($BRANCH): ejecutando verificación en la rama…"
if ! tools/verify.sh >"$LOG" 2>&1; then
  tail -40 "$LOG"
  echo "❌ PR-$NUM rechazada: la verificación falla. No se mergea."
  exit 1
fi

COMMITS="$(git log main..HEAD --pretty='- %h %s')"
FILES="$(git diff --stat main...HEAD | tail -1)"
SUMMARY="$(grep -E 'Ran [0-9]+ tests|^OK|Test Count|Overall result|VERIFICACIÓN' "$LOG" || true)"

cat > "docs/pull-requests/PR-$NUM.md" <<EOF
# PR-$NUM — $TITLE

- **Rama:** \`$BRANCH\` → \`main\`
- **Fecha:** $(date -u +%Y-%m-%dT%H:%MZ)
- **Estado:** ✅ Mergeada (verificación en verde)

## Descripción
$BODY

## Commits
$COMMITS

## Cambios
$FILES

## Resultado de la verificación
\`\`\`
$SUMMARY
\`\`\`

🤖 Generated with [Claude Code](https://claude.com/claude-code)

https://claude.ai/code/session_01TAZqtMuKi59Z6RaTqSmwBL
EOF
git add "docs/pull-requests/PR-$NUM.md"
git commit -q -m "docs(pr): registro de PR-$NUM" -m "$TRAILER"

git checkout -q main
git merge -q --no-ff "$BRANCH" -m "Merge PR-$NUM: $TITLE" -m "$TRAILER"

echo "🔎 Re-verificando main tras el merge…"
if ! tools/verify.sh >"$LOG" 2>&1; then
  tail -40 "$LOG"
  git reset -q --hard HEAD~1
  echo "❌ main falla tras el merge: merge revertido."
  exit 1
fi
git branch -q -d "$BRANCH"
echo "✅ PR-$NUM mergeada en main."
