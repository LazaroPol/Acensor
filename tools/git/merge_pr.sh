#!/usr/bin/env bash
# Mergea una PR solo si: la verificación local pasa, todos los checks de CI terminan en verde,
# y main sigue en verde tras el merge.  Uso: tools/git/merge_pr.sh <número>
source "$(dirname "$0")/common.sh"
PR="${1:?Falta el número de PR}"
TIMEOUT="${CI_TIMEOUT:-900}"

require_feature_branch
verify_or_die "rama $(current_branch)"
git -C "$ROOT" push -q origin "$(current_branch)"
SHA="$(git -C "$ROOT" rev-parse HEAD)"

echo "⏳ Esperando a la CI de $SHA…"
for ((t = 0; t < TIMEOUT; t += 15)); do
  STATE="$(python3 "$ROOT/tools/git/ci_state.py" "$REPO" "$SHA")"
  [ "$STATE" != "pending" ] && break
  sleep 15
done
[ "$STATE" = "success" ] || die "CI no está en verde ($STATE). La PR #$PR no se mergea."
echo "✅ CI en verde"

gh api -X PUT "repos/$REPO/pulls/$PR/merge" -f merge_method=merge -f sha="$SHA" \
  -f commit_title="Merge PR #$PR: $(gh api "repos/$REPO/pulls/$PR" --jq .title)" \
  -f commit_message="$TRAILER" --jq '.message'

BRANCH="$(current_branch)"
git -C "$ROOT" checkout -q main
git -C "$ROOT" pull -q --ff-only origin main
git -C "$ROOT" branch -q -D "$BRANCH"
verify_or_die "main tras mergear #$PR"
