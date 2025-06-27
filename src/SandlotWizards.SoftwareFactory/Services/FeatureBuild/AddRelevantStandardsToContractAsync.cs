using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task<Contract> AddRelevantStandardsToContractAsync(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Adding relevant standards documents to AI contract."))
        {
            var targetPath = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                "SandlotWizards"
            );

            var commandServiceStandardPath = Path.Combine(targetPath, "docs", "Standards", "CommandService.DesignPattern.Standard.md");
            var softwareDevStandardPath = Path.Combine(targetPath, "docs", "Standards", "Core.SoftwareDevelopment.Standard.md");

            var commandServiceText = await _fileStoreFileSystem.ReadFileAsync(commandServiceStandardPath);
            var softwareDevText = await _fileStoreFileSystem.ReadFileAsync(softwareDevStandardPath);

            contract.WorkingContext.Standards.Add(new WorkingStandard
            {
                Key = "CommandService",
                FileName = "CommandService.DesignPattern.Standard.md",
                Path = commandServiceStandardPath,
                Text = commandServiceText
            });

            contract.WorkingContext.Standards.Add(new WorkingStandard
            {
                Key = "CoreSoftwareDevelopment",
                FileName = "Core.SoftwareDevelopment.Standard.md",
                Path = softwareDevStandardPath,
                Text = softwareDevText
            });
        }
        return contract;
    }
}
