#!/usr/bin/env bash
# Instala el SDK de .NET 8 si falta, en silencio. Imprime UNA línea; el detalle va al log
# y solo se mira si falla. Idempotente: si dotnet ya está, no hace nada.
set -uo pipefail
LOG="${DOTNET_SETUP_LOG:-${TMPDIR:-/tmp}/specimen-dotnet-setup.log}"
export DOTNET_NOLOGO=1 DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DEBIAN_FRONTEND=noninteractive
SUDO=""; [ "$(id -u)" = "0" ] || SUDO="sudo"

ok() { echo "✅ dotnet $(dotnet --version 2>/dev/null) listo"; exit 0; }

command -v dotnet >/dev/null 2>&1 && ok
: > "$LOG"

# 1) Paquete del sistema (rápido si el índice de apt lo tiene).
{ $SUDO apt-get update -qq && $SUDO apt-get install -y -qq dotnet-sdk-8.0; } >>"$LOG" 2>&1
command -v dotnet >/dev/null 2>&1 && ok

# 2) Script oficial en ~/.dotnet (si la red lo permite).
if curl -fsSL https://dot.net/v1/dotnet-install.sh -o "${TMPDIR:-/tmp}/dotnet-install.sh" >>"$LOG" 2>&1 \
   && bash "${TMPDIR:-/tmp}/dotnet-install.sh" --channel 8.0 --install-dir "$HOME/.dotnet" >>"$LOG" 2>&1; then
  $SUDO ln -sf "$HOME/.dotnet/dotnet" /usr/local/bin/dotnet >>"$LOG" 2>&1 \
    || { mkdir -p "$HOME/.local/bin"; ln -sf "$HOME/.dotnet/dotnet" "$HOME/.local/bin/dotnet"; }
  command -v dotnet >/dev/null 2>&1 && ok
fi

echo "❌ no se pudo instalar dotnet; últimas líneas de $LOG:"
tail -5 "$LOG"
exit 1
