namespace SandlotWizards.AiPipelines.Models
{
    public interface IAiContract
    {
        string PromptText { get; }
        Dictionary<string, string> ContextFiles { get; }
        string ExecutionContextId { get; }
    }
}
