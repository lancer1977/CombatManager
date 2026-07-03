#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

required_paths=(
  "README.md"
  "docs/features/xamarin-pcl-wpf-modernization-disposition.md"
  "CombatManagerMono/CombatManagerMono.csproj"
  "CombatManagerCore/CombatManagerCoreDroid.csproj"
  "CombatManagerCore/CombatManagerCoreMono.csproj"
  "CombatManagerDroid/CombatManagerDroid.csproj"
  "RandomItemWeightFixer/RandomItemWeightFixer.csproj"
  "DetailsRipper/DetailsRipper.csproj"
  "CombatStateViewer/CombatStateViewer.csproj"
  "PropertyCreator/PropertyCreator.csproj"
  "CombatViewService/CombatViewService.csproj"
  "CombatManager/CombatManager.WPF.csproj"
)

for path in "${required_paths[@]}"; do
  if [[ ! -e "$path" ]]; then
    echo "Missing required path: $path" >&2
    exit 1
  fi
done

echo "== rpg-combat-manager: API tests =="
DOTNET_ROLL_FORWARD=Major dotnet test CombatManager.Api.Test/CombatManagerApi.Test.csproj --configuration Release --verbosity minimal --filter "TestCategory!=LiveCombatManager"

echo "== rpg-combat-manager: websocket console build =="
DOTNET_ROLL_FORWARD=Major dotnet build CombatManager.Websocket.Console/CombatManager.Websocket.Console.csproj --configuration Release --verbosity minimal

echo "== rpg-combat-manager: dependency vulnerability check =="
DOTNET_ROLL_FORWARD=Major dotnet list CombatAPI.sln package --vulnerable --include-transitive

echo "== rpg-combat-manager: devstudio shape =="
if command -v devstudio >/dev/null 2>&1; then
  devstudio validate --repo "$repo_root"
else
  echo "devstudio not installed; skipping repo-shape validation."
fi

echo "rpg-combat-manager validation complete."
