# 🧭 Core.SoftwareDevelopment.Standard.md

## 📌 Purpose

This standard codifies the foundational contracts and development principles of the `Sandlot.Core` library. It defines the baseline software engineering behaviors and interfaces that all Sandlot-related systems must follow. This file shall be included in every build, orchestration, or deployment pipeline to guarantee conformance with the architectural contract layer.

> **Mandate:** All software developed in any automated, AI-assisted, or human-authored pipeline must **use and adhere to the interfaces and implementations defined here** whenever applicable. These abstractions are not optional—they are foundational and enforced by standard.

---

## 🧱 Canonical Interfaces and Implementations

### 🧠 `ICommandHandler<TCommand>` → *Custom Implementations Required*
- Defines the required interface for executing atomic units of work in a command-service pipeline.
- **Rules:**
  - Commands must not contain execution logic.
  - Execution logic belongs in `ICommandHandler<TCommand>.Handle()`.
  - Always supports `CancellationToken`.

---

## 📁 SoftwareFactory File System Layers

All file access shall use the appropriate interface based on the folder of origin. No hardcoded paths, direct `System.IO`, or inlined resolution logic is permitted.

> All file system interfaces inherit from `IFileUtilityService` and are implemented by `FileSystemService`.

### 👨‍💻 `IDeveloperFileSystemService` → `FileSystemService`
- **Purpose**: Resolves developer-side paths for local projects under a standard `source/repos` layout.
- **Methods**: `GetDeveloperRootPath`, `LocateDeveloperSolutionPath`, `LocateDeveloperProjectPath`, etc.
- **Rule**: All dev paths must be routed through this abstraction.

### 🚀 `IDeploymentFileSystemService` → `FileSystemService`
- **Purpose**: Resolves environment-specific solution and project paths under `/Solution_Deployments`.
- **Rule**: Must be used for all environment-aware code.

### 🧱 `ISoftwareFactoryWorkingFileSystemService` → `FileSystemService`
- **Purpose**: Resolves paths under `SoftwareFactory/Working/repos/`.
- **Rule**: Used for cloned working repositories that are being actively built, analyzed, or transformed.

### 📦 `IFileStoreFileSystemService` → `FileSystemService`
- **Purpose**: Resolves persistent files under `SoftwareFactory/FileStore`.
- **Rule**: Used for design specs, prompts, standards, and reference files pulled from GitHub.

---

## 🧪 SQL Access Layer

#### 🔌 `ISqlExecutor` → `SqlExecutor`
#### 🔍 `ISqlConnectionValidator` → `SqlConnectionValidator`
- Enables safe, testable, and contract-based raw SQL execution.
- **Rule**: Never directly use `SqlConnection` in code. Always route SQL logic through these contracts.

---

## 🧭 Configuration Access Layer

#### 🧾 `IEnvironmentOptionsService` → `EnvironmentOptionsService`
- Resolves SQL connection parameters from environment variables or app configuration.
- **Rule**: Never access configuration values manually. All secure configuration must be retrieved from this implementation.

---

## 🖥️ Shell Execution Layer

#### 💻 `IShellCommandService` → `ShellCommandService`
- Executes external shell commands with optional output capture and working directory targeting.
- **Rule**: All shell-level activity must pass through this abstraction.

---

## 🕰️ Time Access Layer

#### ⏱️ `IDateTime` → `DateTimeWrapper`
- Provides `Now`, `UtcNow`, `Today`, and `Null` values.
- **Rule**: Replace all `DateTime.Now` and `DateTime.UtcNow` usage with this abstraction for testability.

---

## 🤖 AI Integration Layer

#### 🧠 `IAiService` → *Custom Implementation Required*
- Accepts structured system and user prompts.
- Returns raw AI output as a string.
- Supports `CancellationToken`.
- **Rule**: All AI calls in the factory or downstream systems must use this pattern.

---

## 🚀 Caching Layer

#### 💾 `IRedisCacheService` → `RedisCacheService`
- Wraps `IDistributedCache` with structured async support, TTL, and serialization.
- **Rule**: Never call `IDistributedCache` directly. All caching must route through this abstraction.

---

## 📁 Constants and Enums

### 📌 `FormMode`
- Enum for form state: `Add`, `Edit`, `Delete`

### 🧾 `EfCoreTargetType`
- Enum for identifying EF Core migration targets: `Application` or `MobileFrame`

### 🧱 `LoggingConstants`
- Defines constants used for structured logging

### 🔤 `RegularExpressions`
- Canonical regex patterns for validation: `camelCase`, `PascalCase`, `Email`, `PhoneNumber`, etc.
- **Rule**: Regex patterns must be reused via constants, not duplicated in logic

---

## 📐 Implementation Principles

- All `Sandlot.Core` interfaces must be:
  - Injectable
  - Mockable
  - Testable in isolation
- **No business logic may depend on concrete implementations of `System.IO`, `SqlConnection`, `DateTime`, etc.**
- All automated pipelines must treat interface implementation as a standard, not an option.

---

## 🧭 Usage in Pipelines

This document is required in all pipelines that:
- Build code
- Generate source files
- Orchestrate deployments
- Perform AI-driven work

Its presence shall be validated by governance tooling as a precondition for production approval.

---

## 🔒 Enforcement

Pipelines and command services must:
- Import this document and validate presence
- Refuse to build if undocumented interfaces or implementations are introduced
- Generate errors or warnings for hard-coded system concerns (e.g. paths, time, shell, SQL)

---

## ✅ Summary

`Core.SoftwareDevelopment.Standard.md` defines the critical **interfaces and their required implementations** for responsible, decoupled, and testable software design within the Sandlot ecosystem. All Sandlot software, whether human- or AI-generated, must conform to the rules declared here. Implementations are not left to chance—they are explicitly defined, governed, and enforced.

