---
title: Sub-module: CombatStateViewer
status: done
owner: @DreadBreadcrumb
priority: high
complexity: 1
created: 2026-03-22
updated: 2026-05-27
tags: [feature, rpg-combat-manager, existing]
---

# Sub-module: CombatStateViewer

Existing implementation of Sub-module: CombatStateViewer within the rpg-combat-manager codebase.

## Contract notes
- The viewer is wired as a WCF duplex callback client and currently reacts to only three service callbacks: `CurrentPlayerChanged`, `CombatListChanged`, and `CharactersChanged`.
- Initial connect pulls a snapshot via `GetCharacters()`, `GetCombatList()`, and `GetCurrentCharacterID()`, then binds the active character border and combat list from cached in-memory collections.
- The UI surface is intentionally narrow: it renders the active character name and the combat list. There is no round display even though the service contract exposes `GetRound()`.

## Live-update gaps to follow up
- Character property-only changes are not pushed through the service callback path, so the viewer can lag behind live edits that do not add/remove characters or alter combat ordering.
- The service queues `CurrentPlayerChanged` separately from `CharactersChanged`, but the viewer only rebinds the active character when the callback arrives. If character data refreshes after the current-player callback, the active border can stay on a stale snapshot until the next current-player event.
- The combat list rebuild also depends on the cached character list, so ordering changes that arrive before the character snapshot refresh can momentarily resolve to stale/null entries.
- The round value is part of the service contract but has no viewer binding or callback refresh path, so it is effectively invisible in this sub-module.

## Follow-up direction
Treat this viewer as a snapshot consumer, not a fully live state projection. Any future hardening should either add a round/status binding and a refresh pass after `CharactersChanged`, or explicitly document that the viewer only guarantees eventual consistency for the three callback types above.
