# CombatManager

[![Build Status](https://img.shields.io/github/actions/workflow/user/lancer1977/CombatManager/.github/workflows/main.yml/badge.svg)](https://github.com/lancer1977/CombatManager/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Tags

- rpg
- rpg-combat-manager
- tabletop
- dotnet
- combat
- manager

## Related Repos

- [`rpg-pf-assistant`](../rpg-pf-assistant/)
- [`rpg-gm-tools`](../rpg-gm-tools/)
- [`rpg-d20`](../rpg-d20/)
- [`rpg-StarFinder`](../rpg-StarFinder/)
- [`rpg-PathfinderLoot`](../rpg-PathfinderLoot/)

## Overview

CombatManager is a legacy tabletop combat manager workspace. The repo contains
older WPF, Xamarin, Mono, and .NET Framework application surfaces plus a smaller
API/test lane that can be validated on a modern Linux workstation.

## Key Features

- Combat state API contracts and client service.
- Remote character, monster, spell, feat, and initiative DTOs.
- Websocket console bridge.
- Legacy desktop/mobile solution files for the original Combat Manager app.

## Architecture

- `CombatManager.Api.Core/` contains shared request and data contracts.
- `CombatManager.Api/` contains API service/client behavior.
- `CombatManager.Api.Test/` contains fixture-backed NUnit smoke and dependency-boundary tests.
- `CombatManager.Websocket.Console/` contains the console bridge.
- `CombatManager.sln`, `CombatManagerWin.sln`, and mobile/Mono solution files are legacy Windows-capable surfaces.

## Technology Stack

- C# / .NET.
- NUnit for the Linux-safe API test lane.
- WPF, Xamarin, Mono, and .NET Framework for legacy application surfaces.

## Getting Started

### Prerequisites
- .NET SDK 10 for the default validation lane.
- Windows with Visual Studio/MSBuild for the full legacy desktop/mobile solution lane.

### Validation
```bash
bash scripts/validate.sh
```

The default validation lane runs API tests, builds the websocket console, checks
package vulnerabilities for `CombatAPI.sln`, and runs DevStudio validation when
available.

## Documentation

- [Project Atlas](./docs/project-atlas/README.md)
- [Code Health](./code_health.md)
- [Architecture Notes](./ARCHITECTURE.md)

## Legacy Validation

The full `CombatManager.sln` includes Windows desktop/mobile projects and is not
the Linux CI default. Validate it on a Windows runner or workstation with the
matching Visual Studio/MSBuild workloads.

## 🔗 Related Projects
*   [CombatAPI](../CombatAPI) (Inferred from solution files)
