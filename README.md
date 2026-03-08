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

