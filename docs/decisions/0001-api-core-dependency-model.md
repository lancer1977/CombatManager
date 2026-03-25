# ADR 0001: CombatManager.Api.Core Dependency Model

**Status:** Accepted

## Context

`CombatManager.Api.Core` is referenced in multiple ways across the codebase:

1. **Local ProjectReference** (CombatManager.Api.csproj):
   ```xml
   <ProjectReference Include="..\CombatManager.Api.Core\CombatManager.Api.Core.csproj" />
   ```

2. **NuGet PackageReference** (CombatManagerCore.csproj):
   ```xml
   <PackageReference Include="PolyhydraGames.CombatManager.Api.Core" Version="1.0.0.6" />
   ```

3. **Legacy DLL Reference** (CombatManagerCoreDroid.csproj):
   ```xml
   <Reference Include="CombatManager.Api.Core, Version=1.0.0.0...">
   ```

This creates confusion about the "source of truth" for the API contract.

## Decision

**CombatManager.Api.Core is a local project, not a NuGet package.**

Both repositories (`rpg-combat-manager` and `rpg-combat-manager.apis`) contain the source code for `CombatManager.Api.Core` and should use ProjectReference, not PackageReference.

### Rationale

1. **Same repository versioning**: `CombatManager.Api.Core` and `CombatManager.Api` are developed together and should stay in sync
2. **No publish friction**: ProjectReference allows instant rebuild propagation without publishing to NuGet
3. **Single source of truth**: The local project defines the API contract

### Implementation

| Project | Reference Type | Action |
|---------|-----------------|--------|
| CombatManager.Api | ProjectReference | Correct - keep |
| CombatManagerCore | PackageReference | Convert to ProjectReference when modernizing |
| CombatManagerCoreDroid | DLL Reference | Legacy - remove or convert when modernizing |

### NuGet Package Status

The `PolyhydraGames.CombatManager.Api.Core` NuGet package (v1.0.0.6) is **deprecated**. It was previously published to Azure Artifacts but is no longer updated. New development should use the local project reference.

## Consequences

### Positive
- Clear dependency model - always build from source
- No version drift between package and source
- Faster development cycle (no NuGet publish step)
- Works offline without Azure DevOps authentication for API projects

### Negative
- Breaking change for external consumers (if any)
- Package version 1.0.0.6 is now stale

### Mitigation
If external projects need `CombatManager.Api.Core` as a NuGet package:
1. Extract it to a separate repository
2. Set up automated NuGet publishing
3. Consume from the public feed

For now, all internal development uses ProjectReference.

## References

- CombatManager.sln - includes CombatManager.Api.Core project
- CombatManager.APIs.sln - includes CombatManager.Api.Core project
- README.md in both repos (documenting local project reference)

## History

- 2026-03-25: ADR accepted and documented