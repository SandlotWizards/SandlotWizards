# 🧱 DesignSpec: ProjectCreateCommandService

## 🧭 Pattern

DesignPattern: CommandService

---

## 📘 Purpose

Create a command that allows users to generate different types of projects (e.g., domain, web, worker) into a solution using a standard CLI command.

---

## 🧾 Command Inputs

| Name         | Type   | Required | Description                                |
| ------------ | ------ | -------- | ------------------------------------------ |
| solution     | string | ✅       | Name of the target solution                |
| project-type | string | ✅       | The type of project to create (e.g., domain, web, infra, worker) |

---

## 🎯 Desired Behavior

When a user runs:

```bash
lore project create --solution MyApp --project-type domain
```

It should:
- Log the start of the operation
- Resolve the correct project-type handler
- Call the method to create that project in the solution
- Log success or meaningful error

---

## 🧪 Validation Requirements

- Project type must be supported (`domain` is implemented now; others rejected with a clear error)
- Solution name must not be empty

---

## 📂 Detailed Design Outputs

This CommandService must generate:

### ✅ Command

- File: `ProjectCreateCommand.cs`
- Location: `source_code/Commands/`
- Implements `ICommand`
- Uses properties: `string Solution`, `string ProjectType`
- Calls into `ProjectCreateService.OrchestrateAsync()`

### ✅ Orchestrator

- File: `ProjectCreateService.cs`
- Location: `source_code/Services/ProjectCreateService/`
- Declared as: `internal partial class ProjectCreateService`
- Method: `internal Task OrchestrateAsync(ProjectCreateCommand command)`

### ✅ SRP Methods

Each must be in its own file:
- `ValidateContextAsync`
- `AddDomainProjectAsync`

### ✅ Logging

Use `ActionLog.Global.BeginStep(...)` in all methods. Errors must use `ActionLog.Global.Error(...)`.

---

## 🤖 Prompt Message

Design and implement the `ProjectCreateCommand` command-service using the CommandService design pattern defined in `CommandService.DesignPattern.Standard.md`.

Context:
- solution: {solution}
- project: {project}
- service-name: ProjectCreate
- design-doc-path: ./Features/project-create-command/DesignSpec.md
- change-type: add

Do not add placeholder methods beyond what is listed in the "Detailed Design Outputs". Follow all structural and behavioral rules in the standard.
