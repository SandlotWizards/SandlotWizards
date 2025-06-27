using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.SoftwareFactory.Models;

namespace SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

public class Contract
{
    public required string feature { get; set; }
    public required string solution { get; set; }
    public required string ExecutionContextId { get; set; }

    public string FeatureDirectory { get; set; } = string.Empty;
    public string WorkingDirectory { get; set; } = string.Empty;

    public string DesignSpecPath { get; set; } = string.Empty;
    public string ExecutionPlanPath { get; set; } = string.Empty;
    public string ContractPath { get; set; } = string.Empty;

    public string DesignSpecText { get; set; } = string.Empty;
    public string ExecutionPlanText { get; set; } = string.Empty;
    public string ContractText { get; set; } = string.Empty;

    public WorkingContext WorkingContext { get; set; } = new();
}
