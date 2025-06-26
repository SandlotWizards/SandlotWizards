using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task<Contract> AddFeatureDesignSpecificationsToContractAsync(Contract contract, string workingRoot)
    {
        using (ActionLog.Global.BeginStep("Adding design spec and related documents to AI contract."))
        {
            var designDocsPath = Path.Combine(workingRoot, "repos", contract.solution, "docs", "Features", contract.feature);
            var specPath = Path.Combine(designDocsPath, "DesignSpec.md");
            var planPath = Path.Combine(designDocsPath, "ExecutionPlan.md");
            var contractPath = Path.Combine(designDocsPath, "Contract.md");

            contract.DesignSpecPath = specPath;
            contract.ExecutionPlanPath = planPath;
            contract.ContractPath = contractPath;

            contract.DesignSpecText = await _fileStoreFileSystem.ReadFileAsync(specPath);
            contract.ExecutionPlanText = await _fileStoreFileSystem.ReadFileAsync(planPath);
            contract.ContractText = await _fileStoreFileSystem.ReadFileAsync(contractPath);
        }
        return contract;
    }
}
