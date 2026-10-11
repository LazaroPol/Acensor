#!/usr/bin/env bash
# Abre una PR en GitHub desde la rama actual, solo si la verificación local pasa.
# Uso: tools/git/open_pr.sh "Título" fichero_descripcion.md   -> imprime el número de PR
source "$(dirname "$0")/common.sh"
TITLE="${1:?Falta el título}"
BODY_FILE="${2:?Falta el fichero de descripción}"

require_feature_branch
verify_or_die "rama $(current_branch)" >&2
git -C "$ROOT" push -q -u origin "$(current_branch)" >&2

python3 - "$TITLE" "$BODY_FILE" "$(current_branch)" "$PR_FOOTER" > /tmp/specimen-pr.json <<'PY'
import json, sys
title, body_file, head, footer = sys.argv[1:]
body = open(body_file, encoding="utf-8").read().rstrip() + footer
print(json.dumps({"title": title, "head": head, "base": "main", "body": body}))
PY
gh api -X POST "repos/$REPO/pulls" --input /tmp/specimen-pr.json --jq '.number'
