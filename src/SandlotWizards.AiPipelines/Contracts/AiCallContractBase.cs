namespace SandlotWizards.AiPipelines.Contracts;

public abstract class AiCallContractBase
{
    public string Purpose { get; set; } = string.Empty;

    public List<AiMessage> Messages { get; set; } = new();

    public List<WorkingStandard> Standards { get; set; } = new();

    public string ExecutionContextId { get; set; } = string.Empty;

    public virtual string PromptText => string.Join("\n", Messages.Select(m => m.Content));
}
