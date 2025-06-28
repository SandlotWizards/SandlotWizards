namespace SandlotWizards.AiPipelines.Contracts;

public class FanOutPlanStep
{
    public required string Type { get; set; } // "prompt" or "command"
    public required string Description { get; set; }
    public string? Command { get; set; }
    public int Order { get; set; }
    public AiCallContractBase? AiInput { get; set; }
}
