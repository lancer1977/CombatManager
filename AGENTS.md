# AGENTS.md

## Repository Retired - Archived/Read-Only Policy

This repository is retired and is intended to be archived and read-only.

- Do not start new agent work in this repository.
- Do not create or generate issues, features, stories, roadmaps, backlog items, or follow-up work.
- Do not modify code, dependencies, workflows, or documentation during routine maintenance or portfolio scans.
- Permitted activity is limited to inspection, history/reference work, or an explicitly authorized unarchive/recovery operation.
- An unarchive or recovery operation must be explicitly authorized for this repository and must define its bounded scope before any mutation.
- Residual validation limitation: the complete legacy application cannot be reproduced in the current automation environment because the required .NET Framework 4.7.1 reference assemblies and proprietary/legacy toolchain components are unavailable. Preserve this limitation in any recovery assessment.

## Purpose

This file is the repo-local guide for coding agents working in this repository.
Follow the closest `AGENTS.md` first, then the workspace-root `AGENTS.md` when this repo lives under `~/code`.

## Default Workflow

- Start with understanding first, then planning, then implementation.
- Read the repository `README.md` and any local docs before changing code.
- Check `docs/features/` and `docs/roadmaps/` when they exist.
- Prefer small, traceable changes over broad rewrites.
- Ask a direct question if the work is ambiguous or would require a risky assumption.
- Preserve existing patterns unless there is a strong reason to change them.
- If this repo participates in portfolio monitoring, treat the Dev Forge Work Matrix as the operator-facing control surface; keep repo-local current-work notes short and avoid duplicating portfolio state in multiple places.

## Validation

- Prefer creating or updating unit tests when they are the right validation surface.
- Use repo-native build, test, and lint commands instead of guessing.
- Verify the smallest relevant surface that proves the change.
- If a change affects docs, update the docs and checklists in the same pass.

## Code Standards

- Keep edits focused and reviewable.
- Prefer strongly typed code and explicit boundaries where practical.
- Avoid destructive commands unless explicitly requested.
- Do not revert user changes you did not make.
- Keep secrets and tokens out of source control.

## Docs Standards

- Treat documentation as part of the product.
- Prefer checklist-heavy docs for progress, validation, and follow-up work.
- Record implementation notes when the repo has ongoing feature work.
- When behavior changes, update the relevant feature or roadmap doc before finishing.

## Git Hygiene

- Do not rewrite shared history unless explicitly asked.
- Do not force-push by default.
- Keep the working tree clean enough that future diffs are easy to inspect.

## Repo Notes

- Add repo-specific commands, constraints, and validation steps below this line.

<!-- dev-forge:dotnet-mvvm-testing:start version=1 -->
## .NET MVVM and Testing Standard

- Keep Views (XAML, Blazor components) limited to layout and bindings; no
  business logic in code-behind or `@code` blocks beyond simple UI wiring.
- ViewModels are plain C# or `ReactiveObject`/`ObservableObject`-based,
  constructed via DI, free of direct UI-framework dependencies, and injected
  with services as interfaces so they can be unit-tested without a UI thread.
- Prefer Blazor over introducing a second UI stack for new .NET UI surfaces —
  its component model maps onto the same MVVM split used elsewhere in the
  portfolio. Record the reason in this repo's `AGENTS.md` when a different
  stack is a deliberate exception.
- Unit-test ViewModels, parsers, and business/domain logic; keep those tests
  free of filesystem, network, database, or UI-thread dependencies.
- Add integration tests where a change crosses a boundary a unit test can't
  observe (parser-to-event pipelines, storage round trips, external API
  calls).
- Full standard: `docs/architecture/dotnet-mvvm-and-testing-standard.md` in
  `dev-forge`.
<!-- dev-forge:dotnet-mvvm-testing:end -->
