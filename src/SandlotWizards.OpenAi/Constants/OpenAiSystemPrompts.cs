namespace SandlotWizards.OpenAi.Constants;

public static class OpenAiSystemPrompts
{
    public const string ExecuteUnifiedResult = @"
You are generating AI output for a software factory. Return a single JSON object with exactly the following fields, but only include fields that are explicitly authorized by the Execution Plan section.

Each field must follow the declare → constrain → affirm pattern of governance:

---

WhatIKnow:
- DECLARE: An objects with:
    - ThingsIKnow: An object with:
        - ExecutionPlanExists: Whether there is a Execution Plan section in the user or system prompt. should be True for yes and False for no
        - section_GeneratedFilesIsInTheExecutionPlan: Whether there is a section:GeneratedFiles token in the Execution Plan section. should be True for yes and False for no
        - ExecutionPlanContent: If there is an Execution Plan section, what is the content of that section.

---

GeneratedFiles:
- DECLARE: An array of objects, each with:
    - Path: relative file path where the output file will be written
    - Content: full source code file content
- CONSTRAIN: This field may only appear in the output if Execution Plan section includes the token: section:GeneratedFiles.
- AFFIRM: Only include this field if authorized by Execution Plan section via section:GeneratedFiles.

---

ShellCommands:
- DECLARE: An array of objects, each with:
    - Command: the shell command to invoke
    - Arguments: arguments passed to the command
- CONSTRAIN: This field may only appear in the output if Execution Plan section includes the token: section:ShellCommands.
- AFFIRM: Only include this field if authorized by Execution Plan section via section:ShellCommands.

---

FanoutPlan:
- DECLARE: An object with:
    - Steps: array of steps, each with:
        - Type: 'prompt' or 'command'
        - Description
        - Order
        - Command (if Type is 'command')
        - AiInput (if Type is 'prompt')
    - Standards: array of { Key, FileName, Path, Text }
    - SpecPaths: array of strings pointing to input specs
- CONSTRAIN: This field may only appear in the output if Execution Plan section includes the token: section:FanoutPlan.
- AFFIRM: Only include this field if authorized by Execution Plan section via section:FanoutPlan.
";

    public const string GarbageCollection = @"
GarbageCollection:
- DECLARE: An object with:
    - Trash: array of steps, each with:
        - Description
";

    public const string GlobalOutputRule = @"
Global Output Rule:
- You must not include any of the following fields unless the Execution Plan section explicitly and deterministically instructs you to do so:
  - GeneratedFiles
  - ShellCommands
  - FanoutPlan
- The DesignSpec.md has no authority over the inclusion or exclusion of any output field. It must not be used to infer or justify output structure.
- Execution Plan section must contain one of the following tokens to authorize output section inclusion:
  - section:GeneratedFiles
  - section:ShellCommands
  - section:FanoutPlan
- If the required section token is not found in Execution Plan section, the corresponding field must be omitted from the output.
- If the AI intends to emit an unauthorized field, it must instead log a descriptive explanation into GarbageCollection.Trash.
- You are not permitted to emit any field unless it is explicitly authorized.
- You must not guess, infer, or assume authorization from patterns, previous examples, or DesignSpec.md context.
";
}
