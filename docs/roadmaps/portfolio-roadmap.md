# rpg-combat-manager portfolio roadmap

## 90-day evidence snapshot
- Commits (90 days): 11
- Files changed (90 days): 61
- Last signal: `65cb85f` (4 days ago)
- Top modified areas: `CombatManagerMono` (30), `docs` (17), `UpgradeLog3.htm` (1)
- Snapshot date: 2026-05-01

## Current posture
- Stack: .NET
- Docs folder: yes
- Roadmap folder: no
- Features docs: yes
- Tests indexed: no
- Direction:
  - Core combat manager Mono + docs updates are the main churn points
  - There are explicit upgrade notes suggesting ongoing migration or refactor

## Discovery
- [x] Capture and timestamp recent change signal
- [x] Capture top-level area concentration
- [ ] Assign owners for combat engine, API/core service boundary, and migration docs
- [ ] Add explicit release gates for backward compatibility and migration behavior

## V1 (stability)
- [x] Stabilize combat state lifecycle in the active modules
- [x] Add smoke checks for core action flows and session creation
- [ ] Normalize package/config assumptions for local run/build paths
- [ ] Capture upgrade path in a clear checklist from `UpgradeLog3.htm`

### Runtime smoke path
- Start the local CombatManager service.
- Seed a fresh combat session with at least one blank player or monster.
- Run `combat/rollinit`, then `combat/next`, then `combat/prev` against the same session.
- Remove the active combatant and confirm the state rebinds to the next valid combatant or clears the current turn cleanly.

### Runtime breakpoints and recovery
- Empty combat lists are a valid state: `combat/next` and `combat/prev` now no-op safely instead of throwing.
- Removing the current combatant should never leave a stale active reference behind.
- If the session is empty or the turn chain breaks, recover by seeding a new blank combatant and rerunning initiative.

## V2 (confidence)
- [x] Add tests or validation for API/core behavior boundaries
- [ ] Document and exercise sqlite/persistence behavior under common workflows
- [ ] Expand migration/testing guidance for desktop and mobile compatibility
- [ ] Strengthen runbook for known breakpoints in combat state flow
- [ ] Validate CombatStateViewer refresh behavior against live `CurrentPlayerChanged` / `CombatListChanged` / `CharactersChanged` callbacks
- [ ] Decide whether the viewer should surface `GetRound()` or remain an intentionally snapshot-only surface

## Viewer contract notes
- The current viewer contract is callback-driven but not fully live: it refreshes the active character and combat list from cached snapshots rather than binding directly to every property change.
- Character property-only updates and round changes have no dedicated viewer refresh path today, so any UI that depends on them needs an explicit follow-up.
- Ordering between `CharactersChanged` and `CurrentPlayerChanged` matters because the active character border is rebound from the cached character list.

## V10 (scale)
- [ ] Add explicit versioning and deprecation rules for public combat contracts
- [ ] Add observability/telemetry expectations for combat event throughput
- [ ] Define long-range architecture plan for cross-platform maintainability
- [ ] Add governance for upgrade path and dependency management

## Feature-to-roadmap mapping
- [x] API layer: `docs/features/combatmanager-api.md`
- [x] Core engine: `docs/features/combatmanager.md`
- [x] Core capabilities: `docs/features/core-capabilities.md`
- [x] Mono-specific implementation: `docs/features/sub-module-combatmanagermono.md`
- [x] SQLite/data path: `docs/features/sub-module-sqlite.md`
- [x] API core seam: `docs/features/combatmanager-api-core.md`

## Release readiness checklist
- [ ] Engine/session smoke path validated in one reproducible run
- [ ] Upgrade instructions in docs updated alongside each breaking change
- [ ] Migration/compatibility risks logged before merge
- [ ] Recovery path documented for failed migration or corrupted state

## Next move
Use V1 to lock runtime and state reliability, then grow V2 test and migration confidence before V10 governance and contract work.
