using SandlotWizards.AiPipelines.Interfaces;
using SandlotWizards.SoftwareFactory.Commands;
using SandlotWizards.SoftwareFactory.Interfaces;

namespace SandlotWizards.SoftwareFactory.Services.FeaturePlan;

public class FeaturePlanService : IFeaturePlanService
{
    private readonly IRagRetriever _rag;

    public FeaturePlanService(IRagRetriever rag) => _rag = rag;

    public async Task ExecuteAsync(FeaturePlanCommand command)
    {
        var results = await _rag.QueryAsync("What does a command-service orchestrator look like?");
        foreach (var result in results)
        {
            Console.WriteLine($"[{result.Section}] from {result.File}:\n{result.Content}");
        }
    }
}
