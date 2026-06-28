# GitHub Packages NuGet Feed Authentication

This project references packages from the PolyhydraGames GitHub Packages feed. Without authentication, `dotnet restore` will fail with a 401 Unauthorized error.

## Feed URL

```
https://nuget.pkg.github.com/lancer1977/index.json
```

## Expected Failure Mode

Without authentication, restore fails with:

```
error NU1301: Package 'PolyhydraGames.CombatManager.Api.Core 1.0.0.6' is not compatible with 'net461'
error: Unable to load the service index for source 'https://nuget.pkg.github.com/lancer1977/index.json'.
error: Response status code does not indicate success: 401 (Unauthorized).
```

This is expected — the feed requires GitHub Packages authentication.

## Authentication

### Option 1: GitHub Packages auth via `gh` or PAT (Recommended)

The simplest approach uses a GitHub token or `gh` auth state, which authenticates against GitHub Packages.

#### Windows

```powershell
# Use your existing GitHub auth token or PAT

# Alternatively, authenticate with the GitHub CLI (`gh auth login`)
```

#### Linux

```bash
# Ensure `gh` is logged in with `write:packages`/`read:packages`
```

After authentication, `dotnet restore` should use the configured GitHub Packages source.

### Option 2: Personal Access Token (PAT)

If the credential provider doesn't work (e.g., on CI/CD or headless environments), use a Personal Access Token.

#### Create a PAT

1. Navigate to https://github.com/settings/tokens
2. Create a new token with scope: **read:packages** (and `write:packages` if you publish)
3. Copy the token value immediately (shown only once)

#### Configure the Token

**Windows (PowerShell):**

```powershell
# Add to user-level NuGet config
dotnet nuget add source https://nuget.pkg.github.com/lancer1977/index.json --name Polyhydra --store-password-in-clear-text --username your-github-username --password GHCR_TOKEN
```

**Linux/macOS (bash):**

```bash
# Add to user-level NuGet config (~/.nuget/NuGet/NuGet.Config)
dotnet nuget add source https://nuget.pkg.github.com/lancer1977/index.json --name Polyhydra --store-password-in-clear-text --username your-github-username --password GHCR_TOKEN
```

**Project-level NuGet.Config (for CI/CD):**

Create `NuGet.Config` in the repo root:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3"/>
        <add key="Polyhydra"
             value="https://nuget.pkg.github.com/lancer1977/index.json"/>
    </packageSources>
</configuration>
```

Then set credentials via environment variable or secure mechanism:

```bash
# For CI/CD, use a GitHub Actions secret and `GITHUB_TOKEN`
```

> ⚠️ **Never commit PATs to version control.** Add `NuGet.Config` to `.gitignore` if it contains credentials.

## Verify

### Before Auth (Expected Failure)

```bash
dotnet restore CombatManager.sln
# Expected: NU1301 + 401 (Unauthorized) for the Polyhydra feed
```

### After Auth (Expected Success)

```bash
dotnet restore CombatManager.sln
```

Output should complete without NU1301/401 errors from the Polyhydra feed.

## Troubleshooting

| Problem | Solution |
|---------|----------|
| **401 after using PAT** | Ensure PAT has "Packaging" > "Read" scope |
| **Credential provider not found** | Add `~/.nuget/bin` to PATH after installing the tool |
| **Cached bad credentials** | Clear NuGet caches: `dotnet nuget locals http-cache --clear` |
| **Linux: credential prompt hangs** | Use PAT method instead; credential provider may not work in all terminals |

## Alternative: Build Without Auth

If you're only developing against the API and don't need `CombatManager.Base` or `CombatManager.WPF7`:

```bash
# In CombatManager.APIs repo (modern SDK projects)
dotnet build pf-combat-manager/CombatManager.Api/
```

These projects reference only nuget.org packages and don't require GitHub Packages authentication.
