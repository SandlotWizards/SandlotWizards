namespace SandlotWizards.AiPipelines.Contracts;

public class AiUnifiedResult
{
    public List<GeneratedFile> GeneratedFiles { get; set; } = new();
    public List<ShellCommand> ShellCommands { get; set; } = new();
    public FanoutPlan FanoutPlan { get; set; } = new();
}