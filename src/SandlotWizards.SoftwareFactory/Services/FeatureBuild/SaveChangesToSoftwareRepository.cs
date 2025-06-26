using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private void SaveChangesToSoftwareRepository(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Saving changes to software repository."))
        {
            var repoRoot = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                contract.solution);

            _shellCommandService.ExecuteCommand("git", "add .", workingDirectory: repoRoot, captureOutput: true);
            Console.WriteLine(_shellCommandService.StandardOutput);
            Console.WriteLine(_shellCommandService.StandardError);
            _shellCommandService.ExecuteCommand("git", $"commit -m \"feat: update {contract.feature} via FeatureBuild pipeline\"", workingDirectory: repoRoot, captureOutput: true);
            Console.WriteLine(_shellCommandService.StandardOutput);
            Console.WriteLine(_shellCommandService.StandardError);
            _shellCommandService.ExecuteCommand("git", "push", workingDirectory: repoRoot, captureOutput: true);
            Console.WriteLine(_shellCommandService.StandardOutput);
            Console.WriteLine(_shellCommandService.StandardError);

            ActionLog.Global.Success("Git commit and push completed.");
        }
    }
}