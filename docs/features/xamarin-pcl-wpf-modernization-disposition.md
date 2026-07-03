---
title: Xamarin PCL WPF Modernization Disposition
status: done
owner: repo steward
priority: high
complexity: 2
created: 2026-07-03
updated: 2026-07-03
tags: [documentation, migration, PCL, Xamarin, WPF, MAUI, Avalonia, net10]
---

# Xamarin PCL WPF Modernization Disposition

This repository preserves the legacy Combat Manager desktop, mobile, and utility
application surfaces. The modern Linux-safe lane is the API/test surface; the
old Xamarin, Mono, PCL, and WPF projects are archival until dedicated
modernization branches replace them.

## Current Legacy Surfaces

| Surface | Current project | Current framework shape | Disposition |
|---|---|---|---|
| iOS Mono app | `CombatManagerMono/CombatManagerMono.csproj` | Xamarin.iOS | Archive in place until a MAUI `net10` iOS target exists. |
| iOS Mono old app | `CombatManagerMono/CombatManagerMonoOld.csproj` | Xamarin.iOS-era project | Archive in place; do not patch except for evidence gathering. |
| Core Android app | `CombatManagerCore/CombatManagerCoreDroid.csproj` | Xamarin Android, `TargetFrameworkVersion` `v11.0` | Archive in place until a MAUI `net10` Android target exists. |
| Core iOS app | `CombatManagerCore/CombatManagerCoreMono.csproj` | Xamarin.iOS | Archive in place until a MAUI `net10` iOS target exists. |
| Core shared project | `CombatManagerCore/CombatManagerCore.csproj` | Legacy shared project and data/resources | Use as source evidence when extracting shared behavior. |
| Android app | `CombatManagerDroid/CombatManagerDroid.csproj` | Xamarin Android, `TargetFrameworkVersion` `v9.0` | Archive in place until a MAUI `net10` Android target exists. |
| WPF desktop app | `CombatManager/CombatManager.WPF.csproj` | WPF on .NET Framework `v4.6.1` | Archive in place; replace with Avalonia `net10` instead of patching WPF as the future desktop path. |
| Random item utility | `RandomItemWeightFixer/RandomItemWeightFixer.csproj` | .NET Framework/PCL-era utility | Archive until utility behavior is either retired or extracted into a tested CLI/library. |
| Details ripper utility | `DetailsRipper/DetailsRipper.csproj` | .NET Framework/PCL-era utility | Archive until extraction/import behavior is explicitly revalidated. |
| State viewer | `CombatStateViewer/CombatStateViewer.csproj` | .NET Framework v4.0 Client Profile | Archive; replace with modern diagnostic UI only if needed. |
| Property creator | `PropertyCreator/PropertyCreator.csproj` | .NET Framework v4.0 Client Profile | Archive; replace with generator/tooling only if needed. |
| Combat view service | `CombatViewService/CombatViewService.csproj` | .NET Framework v4.0 Client Profile | Archive; replace with a modern service only through a dedicated issue. |

## Current Modernizable Lane

The repo-owned validation lane remains:

- `CombatManager.Api.Core/CombatManager.Api.Core.csproj`
- `CombatManager.Api/CombatManager.Api.csproj`
- `CombatManager.Api.Test/CombatManagerApi.Test.csproj`
- `CombatManager.Websocket.Console/CombatManager.Websocket.Console.csproj`

These projects should stay buildable and tested while legacy UI/app surfaces are
inventoried or replaced.

## Target Framework Policy

- Mobile/native replacement apps should target the latest available MAUI
  `net10` platform TFMs.
- Desktop replacement work should use Avalonia on `net10`.
- Shared extracted code should target SDK-style `netstandard2.0` unless tests and
  consumers prove a newer shared target is appropriate.
- Legacy Xamarin, Mono, PCL, WPF `net461`, and .NET Framework v4.0 Client Profile
  projects should not be patched in place unless required for archival
  restore/build evidence.

## Follow-Up Issue Splits

Future modernization work should be split into narrow issues:

1. Inventory shared combat models, rules, persistence, XML data, view models,
   and platform bootstrap code.
2. Expand API/core tests around combat state, spells, monsters, feats, and
   remote DTO conversion before moving UI shells.
3. Extract reusable shared behavior into SDK-style `netstandard2.0` libraries.
4. Create a MAUI `net10` Android/iOS shell if mobile support is resumed.
5. Create an Avalonia `net10` desktop shell to replace WPF.
6. Decide whether each legacy utility is retired, converted to a CLI, or folded
   into the shared library/test lane.
7. Replace Xamarin, Mono, PCL, WPF, and legacy support package references in new
   targets.
8. Define parity checks before retiring legacy project files.

## Validation

For the current repo state, run:

```bash
bash scripts/validate.sh
```

That validation runs the Linux-safe API tests, builds the websocket console,
checks package vulnerabilities for `CombatAPI.sln`, and runs optional DevStudio
shape validation when available. Full legacy UI/mobile builds require the
matching Visual Studio, Xamarin, Mono, and Windows desktop workloads.
