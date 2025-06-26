using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;
using System.Text.Json;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task RemoveOldVersionObjectsFromRepoAsync(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Removing old version objects from source repository."))
        {
            var repoRoot = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                contract.solution);

            var manifestPath = Path.Combine(
                repoRoot, "docs", "Features", contract.feature, "GeneratedFiles.json");

            if (!_softwareFactoryFileSystem.FileExists(manifestPath))
            {
                ActionLog.Global.Info("No previous manifest found. Nothing to delete.");
                return;
            }

            var json = await _softwareFactoryFileSystem.ReadFileAsync(manifestPath);
            var paths = JsonSerializer.Deserialize<List<string>>(json) ?? new();

            foreach (var relPath in paths)
            {
                var fullPath = Path.Combine(repoRoot, relPath);
                if (_softwareFactoryFileSystem.FileExists(fullPath))
                {
                    _softwareFactoryFileSystem.DeleteFile(fullPath);
                    ActionLog.Global.Info($"Deleted: {relPath}");
                }
            }
        }
    }
}