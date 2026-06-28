# rpg-combat-manager Project Atlas

## Purpose

`rpg-combat-manager` preserves the older Combat Manager codebase and its API
surface for tabletop combat workflows. The repo mixes legacy desktop/mobile
projects with a smaller API/test lane that can be validated on Linux.

## Primary Surfaces

- `CombatManager.Api.Core/` - shared API contracts and request/data models.
- `CombatManager.Api/` - API client/service implementation.
- `CombatManager.Api.Test/` - NUnit smoke and dependency-boundary tests.
- `CombatManager.Websocket.Console/` - console bridge for websocket behavior.
- `CombatManager.sln` and related solution files - legacy Windows/Xamarin
  application surfaces.

## Commands

```bash
bash scripts/validate.sh
DOTNET_ROLL_FORWARD=Major dotnet test CombatManager.Api.Test/CombatManagerApi.Test.csproj --configuration Release --verbosity minimal
DOTNET_ROLL_FORWARD=Major dotnet test CombatManager.Api.Test/CombatManagerApi.Test.csproj --configuration Release --filter "TestCategory=LiveCombatManager"
```

## Runtime Boundary

Default validation is fixture/local only. Live GM/player workflow validation
requires the legacy application runtime, seed data, and a Windows-capable
environment for the desktop/mobile surfaces.
