# 🤖 Prompt: ProjectCreateCommandService

You are an AI software engineer working inside the Sandlot Software Factory. You have been given a design specification for a new CommandService called `ProjectCreateCommand`.

Use the design spec provided and the CommandService design pattern standard to generate the necessary implementation code.

## Design Overview

The `ProjectCreateCommand` should:
- Accept `--solution` and `--project-type` as inputs
- Use a service orchestrator named `ProjectCreateService`
- Implement SRP methods: `ValidateContextAsync`, `AddDomainProjectAsync`
- Follow ActionLog logging standards
- Output files in the canonical structure

## Design Pattern

Use only the rules and structure defined in `CommandService.DesignPattern.Standard.md` to determine file layout, naming, class structure, orchestrator behavior, and logging requirements.

## Deliverables

Generate:
- `ProjectCreateCommand.cs` in `source_code/Commands/`
- `ProjectCreateService.cs` in `source_code/Services/ProjectCreateService/`
- `ValidateContextAsync.cs`, `AddDomainProjectAsync.cs` (one per file)
- `copilot_instructions/CopilotInstructions.json`

Be deterministic. Do not invent structure or names not found in the spec or pattern.
