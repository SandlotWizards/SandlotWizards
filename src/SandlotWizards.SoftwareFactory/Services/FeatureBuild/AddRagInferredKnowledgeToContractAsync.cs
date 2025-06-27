using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task<Contract> AddRagInferredKnowledgeToContractAsync(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Retrieving relevant RAG knowledge to enrich AI contract."))
        {
            var queryText = contract.DesignSpecText + "\n" + contract.ContractText;
            var ragResults = await _ragRetriever.QueryAsync(queryText);
            contract.WorkingContext.RagChunks = ragResults
                .Where(x => !string.IsNullOrWhiteSpace(x.Content))
                .Select(x => new RagChunk
                {
                    Title = $"{x.File ?? "Unknown File"} :: {x.Section ?? "Unnamed Section"}",
                    Content = x.Content!,
                    Source = x.File ?? "unknown"
                })
                .ToList();
        }
        return contract;
    }
}
