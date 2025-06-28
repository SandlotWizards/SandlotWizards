namespace SandlotWizards.AiPipelines.Contracts;

public class FanoutPlan
{
    public List<FanOutPlanStep> Steps { get; set; } = new();
    public List<WorkingStandard> Standards { get; set; } = new();
    public List<string> SpecPaths { get; set; } = new();
}