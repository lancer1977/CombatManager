# Code Health

## Current Baseline

`rpg-combat-manager` is a legacy tabletop combat workspace. It contains old
Windows, Xamarin, and .NET Framework projects plus a smaller Linux-safe API
surface that can be validated with the current workstation SDK.

The repo-native validation command is:

```bash
bash scripts/validate.sh
```

## Validation Boundary

Automated validation covers:

- `CombatManager.Api.Test` in Release mode with `DOTNET_ROLL_FORWARD=Major`,
  excluding the `LiveCombatManager` category by default.
- `CombatManager.Websocket.Console` build in Release mode.
- `dotnet list CombatAPI.sln package --vulnerable --include-transitive`.
- DevStudio repo-shape validation when the CLI is available.

The full `CombatManager.sln` remains a Windows/MSBuild legacy surface because it
includes WPF, Xamarin, and older .NET Framework projects. Treat that as a manual
or Windows-runner validation lane, not the default Linux CI lane.

The `LiveCombatManager` NUnit category requires the legacy Combat Manager runtime
listening on `localhost:12457` with expected seed data. Run it explicitly during
manual smoke testing, not in default CI.

## Dependency Notes

- The API restore cycle was caused by unused references from local
  `CombatManager.Api*` projects to `PolyhydraGames.Rpg.Contracts`, while that
  sibling contracts package references `CombatManager.Api`.
- The test project pins `SQLitePCLRaw.lib.e_sqlite3` to override the vulnerable
  transitive native SQLite package pulled by the older `Microsoft.Data.Sqlite`
  test dependency.

## Follow-Ups

- Decide whether to modernize or archive the WPF/Xamarin solution surfaces.
- Move API projects off out-of-support target frameworks when touching runtime
  behavior.
- Add fixture-backed tests around combat-state transformations before feature
  work resumes.
