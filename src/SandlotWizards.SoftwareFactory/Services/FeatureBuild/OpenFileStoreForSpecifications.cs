using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private void OpenFileStoreForSpecifications(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Cloning file store for specifications."))
        {
            var targetPath = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                "SandlotWizards"
            );

            var repoUrl = $"git@github.com:{_gitHubConfig.Organization}/SandlotWizards.git";
            if (!_softwareFactoryFileSystem.DirectoryExists(targetPath))
            {
                var cloneExitCode = _shellCommandService.ExecuteCommand("git", $"clone {repoUrl} \"{targetPath}\"");

                if (cloneExitCode != 0)
                {
                    ActionLog.Global.Error($"Failed to clone repository {repoUrl} to {targetPath}");
                    throw new InvalidOperationException("Git clone failed.");
                }
            }
        }
    }
}
