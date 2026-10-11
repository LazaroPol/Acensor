#!/usr/bin/env bash
# Ejecuta TODAS las verificaciones del proyecto. Código de salida != 0 si algo falla.
set -uo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$ROOT"
status=0
export DOTNET_NOLOGO=1 DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

echo "▶ [1/2] Tests de arquitectura y herramientas (Python)"
if ! python3 -m unittest discover -s tools/tests -t . -p "test_*.py" 2>&1; then
  status=1
fi

if [ -f dotnet/Specimen.Core.Tests/Specimen.Core.Tests.csproj ]; then
  echo "▶ [2/2] Compilación del núcleo (warnings = error) y tests NUnit (C#)"
  command -v dotnet >/dev/null 2>&1 || tools/setup_dotnet.sh || status=1
  dotnet build dotnet/Specimen.Core/Specimen.Core.csproj -c Release -warnaserror -nologo -v q 2>&1 | tail -5
  [ "${PIPESTATUS[0]}" = "0" ] || status=1
  dotnet run --project dotnet/Specimen.Core.Tests/Specimen.Core.Tests.csproj -c Release -- --noresult --labels=Off 2>&1 | tail -25
  [ "${PIPESTATUS[0]}" = "0" ] || status=1
else
  echo "▶ [2/2] (sin proyecto de tests C# todavía)"
fi

if [ $status -eq 0 ]; then echo "✅ VERIFICACIÓN OK"; else echo "❌ VERIFICACIÓN FALLIDA"; fi
exit $status
