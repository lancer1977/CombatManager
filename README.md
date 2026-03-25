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

### Api.Core Dependency Model

`CombatManager.Api.Core` is a **local project**, not a NuGet package. All development uses ProjectReference to the local source.

| Project | Reference Type | Status |
|---------|-----------------|--------|
| CombatManager.Api | ProjectReference | ✓ Correct |
| CombatManagerCore | PackageReference (v1.0.0.6) | Deprecated - convert when modernizing |

**Why local reference:**
- API Core and API are versioned together in the same repo
- ProjectReference enables instant rebuild propagation
- No NuGet publish friction during development

**Decision record:** [ADR-0001: Api.Core Dependency Model](./docs/decisions/0001-api-core-dependency-model.md)

> ⚠️ The `PolyhydraGames.CombatManager.Api.Core` NuGet package is deprecated and no longer updated. Use the local project reference instead.



## NuGet Authentication

This repo references packages from Azure DevOps Artifacts (PolyhydraGames feed). Some projects require authentication to restore.

See [docs/nuget-feed-auth.md](docs/nuget-feed-auth.md) for:
- Expected failure mode without auth (401 Unauthorized)
- Azure Artifacts Credential Provider setup
- PAT-based authentication for CI/CD

## 📖 Documentation
Detailed documentation can be found in the following sections:
- [Feature Index](./docs/features/README.md)
- [Core Capabilities](./docs/features/core-capabilities.md)
