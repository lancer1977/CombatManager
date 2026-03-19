# CombatManager
Combat Management software for Windows, iPad, and Android
Forked from KyleADOlson/CombatManager repo.

Extracted and packaged up the WebSocket APIS to integrate some behavior into other pet projects.

## CI

This repo includes legacy Windows-only WPF projects (old .NET Framework) and modern SDK-style projects.

### What CI builds

- Linux (SDK-style projects only):
  - `CombatManager.Api.Core`
  - `CombatManager.Api`
  - `CombatManager.Websocket.Console`
  - Tests: `CombatManager.Api.Test`
- Windows:
  - Full solution: `CombatManager.sln` (includes WPF projects)

### Notes

- WPF / .NET Framework projects are **intentionally excluded** from Linux builds.

### Api.Core Dependency

`CombatManager.Api` uses a **ProjectReference** to `CombatManager.Api.Core` (local project reference).

**Decision rationale (2026-03-11):**
- Both projects are in the same repository and versioned together
- ProjectReference allows for instant rebuild propagation during development (no NuGet publish step)
- Eliminates version synchronization issues between package and local project
- The package reference approach (used previously with `CombatManager.Api.Core` NuGet 1.0.0.8) required publishing to a NuGet feed, adding friction to development

If the API is ever extracted to a standaloneNuGet package for use in external projects, a separate publication workflow would be needed.

