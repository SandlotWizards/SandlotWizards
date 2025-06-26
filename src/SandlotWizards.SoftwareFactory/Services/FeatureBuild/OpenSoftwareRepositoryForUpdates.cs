using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private void OpenSoftwareRepositoryForUpdates(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Cloning and preparing solution repository for feature updates."))
        {
            var targetPath = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                contract.solution
            );

            var repoUrl = $"git@github.com:{_gitHubConfig.Organization}/{contract.solution}.git";
            var cloneExitCode = _shellCommandService.ExecuteCommand("git", $"clone {repoUrl} \"{targetPath}\"");

            if (cloneExitCode != 0)
            {
                ActionLog.Global.Error($"Failed to clone repository {repoUrl} to {targetPath}");
                throw new InvalidOperationException("Git clone failed.");
            }

            var checkoutExitCode = _shellCommandService.ExecuteCommand(
                "git", $"checkout -b feature/{contract.feature}", workingDirectory: targetPath);

            if (checkoutExitCode != 0)
            {
                ActionLog.Global.Error($"Failed to checkout feature branch 'feature/{contract.feature}' in {targetPath}");
                throw new InvalidOperationException("Git checkout failed.");
            }
        }
    }
}
