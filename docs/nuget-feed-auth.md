# Azure Artifacts NuGet Feed Authentication

This project references packages from the PolyhydraGames Azure DevOps Artifacts feed. Without authentication, `dotnet restore` will fail with a 401 Unauthorized error.

## Feed URL

```
https://pkgs.dev.azure.com/polyhydragames/_packaging/PolyhydraSoftware/nuget/v3/index.json
```

## Expected Failure Mode

Without authentication, restore fails with:

```
error NU1301: Package 'PolyhydraGames.CombatManager.Api.Core 1.0.0.6' is not compatible with 'net461'
error: Unable to load the service index for source 'https://pkgs.dev.azure.com/polyhydragames/_packaging/PolyhydraSoftware/nuget/v3/index.json'.
error: Response status code does not indicate success: 401 (Unauthorized).
```

This is expected — the feed requires Azure DevOps authentication.

## Authentication

### Option 1: Azure Artifacts Credential Provider (Recommended)

The simplest approach uses the Azure Artifacts Credential Provider, which automatically authenticates using your Azure DevOps credentials.

#### Windows

```powershell
# Install via .NET tool (run in PowerShell as Administrator)
dotnet tool install --global AzureArtifacts.CredentialProvider

# Alternatively, install the Visual Studio extension (VS2022+)
```

#### Linux

```bash
# Install via .NET tool
dotnet tool install --global AzureArtifacts.CredentialProvider

# Add the bin directory to your PATH (add to ~/.bashrc or ~/.zshrc)
export PATH="$HOME/.nuget/bin:$PATH"
```

After installation, `dotnet restore` should authenticate automatically using your cached Azure DevOps credentials.

### Option 2: Personal Access Token (PAT)

If the credential provider doesn't work (e.g., on CI/CD or headless environments), use a Personal Access Token.

#### Create a PAT

1. Navigate to https://dev.azure.com/polyhydragames/_usersSettings/tokens
2. Create a new token with scope: **Packaging** > **Read** (check the box)
3. Copy the token value immediately (shown only once)

#### Configure the Token

**Windows (PowerShell):**

```powershell
# Add to user-level NuGet config
dotnet nuget add source https://pkgs.dev.azure.com/polyhydragames/_packaging/PolyhydraSoftware/nuget/v3/index.json --name Polyhydra --store-password-in-clear-text --username your-email@domain.com --password YOUR_PAT_HERE
```

**Linux/macOS (bash):**

```bash
# Add to user-level NuGet config (~/.nuget/NuGet/NuGet.Config)
dotnet nuget add source https://pkgs.dev.azure.com/polyhydragames/_packaging/PolyhydraSoftware/nuget/v3/index.json --name Polyhydra --store-password-in-clear-text --username your-email@domain.com --password YOUR_PAT_HERE
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
             value="https://pkgs.dev.azure.com/polyhydragames/_packaging/PolyhydraSoftware/nuget/v3/index.json"/>
    </packageSources>
</configuration>
```

Then set credentials via environment variable or secure mechanism:

```bash
# For CI/CD, use environment variable (Azure DevOps Pipelines)
# Set VSS_NUGET_EXTERNAL_FEED_ENDPOINTS or use the NuGetAuthenticate task
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

These projects reference only nuget.org packages and don't require Azure Artifacts authentication.