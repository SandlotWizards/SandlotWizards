using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private void RemoveWorkingFolder(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Cleaning up working folder."))
        {
            var repoRoot = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                contract.solution);

            var workingFolder = contract.WorkingDirectory;

            if (!string.IsNullOrWhiteSpace(workingFolder))
            {
                foreach (var subDir in Directory.GetDirectories(workingFolder, "*", SearchOption.AllDirectories))
                {
                    foreach (var file in Directory.GetFiles(subDir))
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                    }
                }
                _softwareFactoryFileSystem.DeleteDirectory(workingFolder);
                ActionLog.Global.Success($"Deleted: {contract.WorkingDirectory}");
            }
        }
    }
}