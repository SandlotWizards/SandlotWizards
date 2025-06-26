# ⚙️ Execution Plan: ProjectCreateCommandService

## 🧭 Pattern
DesignPattern: CommandService

## 📘 Context
- Feature: project-create-command
- Service Name: ProjectCreate
- Project: [determined at runtime]
- Solution: [determined at runtime]
- Design Doc Path: ./Features/project-create-command/DesignSpec.md

---

## 🎯 Purpose
Implement the `ProjectCreateCommand` using the CommandService pattern, enabling CLI-based project generation for supported project types. Initial implementation only supports `domain`.

---

## 🧱 Required Source Code Objects

### ✅ 1. Command: `ProjectCreateCommand.cs`

- **Location:** `source_code/Commands/`
- **Implements:** `ICommand`
- **Properties:** `string Solution`, `string ProjectType`
- **Constructor:** Required to assign both properties
- **Behavior:** Used by CLI to dispatch into orchestrator
- **Logging:** None

---

### ✅ 2. Orchestrator: `ProjectCreateService.cs`

- **Location:** `source_code/Services/ProjectCreateService/`
- **Class:** `internal partial class ProjectCreateService`
- **Method:** `internal Task OrchestrateAsync(ProjectCreateCommand command)`
- **Behavior:**
  1. Logs orchestration start
  2. Calls `ValidateContextAsync(...)`
  3. Calls `AddDomainProjectAsync(...)`
  4. Catches errors and logs via `ActionLog.Global.Error(...)`
- **Logging:** `ActionLog.Global.BeginStep(...)`, `.Error(...)`, `.Success()`

---

### ✅ 3. SRP Methods (Each in Own File)

#### a. `ValidateContextAsync.cs`

- **Location:** `source_code/Services/ProjectCreateService/`
- **Method Signature:** `private async Task ValidateContextAsync(ProjectCreateCommand command)`
- **Purpose:** Validate inputs: solution name not empty, project-type == "domain"
- **Logging:** BeginStep + Success/Error

#### b. `AddDomainProjectAsync.cs`

- **Location:** `source_code/Services/ProjectCreateService/`
- **Method Signature:** `private async Task AddDomainProjectAsync(ProjectCreateCommand command)`
- **Purpose:** Add domain project to solution
- **Logging:** BeginStep + Success/Error

---

### 📦 4. Copilot Instructions Manifest

- **Filename:** `copilot_instructions/CopilotInstructions.json`
- **Contents:** Ordered list of createFile actions with path and file content reference

---

## 🔄 AI Output Format

The AI must return the following Markdown-formatted response:

### ## Payload
A base64-encoded ZIP string containing:
- All source code files listed above
- The `copilot_instructions/CopilotInstructions.json` file

### ## Instructions
A human-readable explanation describing:
- What was generated
- How it maps to the DesignSpec and the CommandService pattern

---

## ✅ Logging Requirements

- Each orchestrator or SRP method must begin with `ActionLog.Global.BeginStep(...)`
- Errors must be captured using `ActionLog.Global.Error(...)`
- End-of-success must use `ActionLog.Global.Success()`

---

## 🚫 Constraints

- Do not invent any other SRP methods
- Do not include placeholder or TODO code
- Do not use external dependencies or command-routing outside the defined pattern
- Use exact filenames and directory structure