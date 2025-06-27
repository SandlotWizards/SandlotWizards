namespace SandlotWizards.AiPipelines.Contracts;

public class ExecutionPlan
{
    public List<ExecutionPlanStep> Steps { get; set; } = new();
    public List<WorkingStandard> Standards { get; set; } = new();
    public List<string> SpecPaths { get; set; } = new();
}