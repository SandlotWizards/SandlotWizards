namespace SandlotWizards.AiPipelines.Constants
{
    public static class AiPipelineConstants
    {
        public const string localhost = nameof(localhost);
        public const string GitHubRepositoryUrlFormat = "https://{0}github.com/{1}/{2}.git";
        public const string CanonicalStandardPath = "%USERPROFILE%\\source\\repos\\Sandlot.ArchitectureDocs\\src\\Sandlot.ArchitectureDocs.Content\\20-Standards\\SandlotCopilot.Canonical.Standard.md";
        public const string DefaultPromptRelativePath = "%USERPROFILE%\\source\\repos\\Sandlot.ArchitectureDocs\\src\\Sandlot.ArchitectureDocs.Content\\90-AiPrompts\\SandlotCopilot.Canonical.DefaultPrompt.md";
        public const string StandardSystemPrompt_ExecuteUnifiedResult = @"
            You are generating AI output for a software factory. Return a single JSON object with exactly the following fields:

            - generatedFiles: an array of objects with `path` and `content` (for full source files)
            - shellCommands: an array of objects with `command` and `arguments` (for shell/script tasks)
            - promptCalls: an array of objects each containing:
                - executionContextId: a unique string
                - purpose: a description of the AI task
                - messages: array of { role, content }
                - standards: array of { key, fileName, path, text }
            - executionPlan: an object with:
                - steps: array of execution steps, each with:
                    - type: ""prompt"" or ""command""
                    - description
                    - order
                    - command (if type is command)
                    - aiInput (if type is prompt): same shape as a promptCall
                - standards: array of { key, fileName, path, text }
                - specPaths: array of string paths to relevant spec files

            Rules:
            - Return only the raw JSON object (not markdown)
            - Do not explain or annotate
            - Ensure all property names match exactly
            ";
    }
}