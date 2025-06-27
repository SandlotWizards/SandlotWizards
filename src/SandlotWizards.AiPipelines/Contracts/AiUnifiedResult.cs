namespace SandlotWizards.AiPipelines.Contracts;

public class AiUnifiedResult
{
    public List<GeneratedFile> GeneratedFiles { get; set; } = new();
    public List<ShellCommand> ShellCommands { get; set; } = new();
    public List<AiCallContractBase> PromptCalls { get; set; } = new();
    public ExecutionPlan ExecutionPlan { get; set; } = new();
}