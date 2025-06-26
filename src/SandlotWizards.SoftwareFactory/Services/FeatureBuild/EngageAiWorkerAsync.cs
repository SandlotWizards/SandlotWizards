using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Adapters;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task<Contract> EngageAiWorkerAsync(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Engaging AI pipeline to generate feature artifacts."))
        {
            var adapter = new FeatureBuildAiContractAdapter(contract);
            var generatedFiles = await _aiPipelineService.ExecuteAsync(adapter);

            contract.WorkingContext.GeneratedFiles = generatedFiles;
            return contract;
        }
    }
}
