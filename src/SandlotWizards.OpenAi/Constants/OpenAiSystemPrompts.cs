namespace SandlotWizards.OpenAi.Constants;

public static class OpenAiSystemPrompts
{
    public const string ExecuteUnifiedResult = @"
        You are generating AI output for a software factory. Return a single JSON object with exactly the following fields:

        - GeneratedFiles: an array of objects with `Path` and `Content` (for full source files)
        - ShellCommands: an array of objects with `Command` and `Arguments` (for shell/script tasks)
        - PromptCalls: an array of objects each containing:
            - ExecutionContextId: a unique string
            - Purpose: a description of the AI task
            - Messages: array of { Role, Content }
            - Standards: array of { Key, FileName, Path, Text }
        - ExecutionPlan: an object with:
            - Steps: array of execution steps, each with:
                - Type: ""prompt"" or ""command""
                - Description
                - Order
                - Command (if type is command)
                - AiInput (if type is prompt): same shape as a PromptCall
            - Standards: array of { Key, FileName, Path, Text }
            - SpecPaths: array of string paths to relevant spec files

        Rules:
        - Return only the raw JSON object (not markdown)
        - Do not explain or annotate
        - Ensure all property names match exactly
        ";
}

