using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;
using System.Text.Json;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task AddNewVersionObjectsToRepoAsync(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Adding new version objects to source repository."))
        {
            var repoRoot = _softwareFactoryFileSystem.LocateSoftwareFactoryWorkingRepoPath(
                contract.ExecutionContextId,
                contract.solution);

            var generatedPaths = new List<string>();

            foreach (var file in contract.WorkingContext.GeneratedFiles)
            {
                file.Path = file.Path.Replace('/', Path.DirectorySeparatorChar);
                file.Path = file.Path.Replace("source_code", Path.Combine("src", "SandlotWizards.SoftwareFactory"));
                var fullPath = Path.Combine(repoRoot, file.Path);
                var directory = Path.GetDirectoryName(fullPath)!;

                _softwareFactoryFileSystem.CreateDirectory(directory);
                await _softwareFactoryFileSystem.WriteFileAsync(fullPath, file.Content);

                generatedPaths.Add(file.Path);
                ActionLog.Global.Info($"Wrote: {file.Path}");
            }

            var manifestPath = Path.Combine(
                repoRoot, "docs", "Features", contract.feature, "GeneratedFiles.json");

            _softwareFactoryFileSystem.CreateDirectory(Path.GetDirectoryName(manifestPath)!);
            var json = JsonSerializer.Serialize(generatedPaths, new JsonSerializerOptions { WriteIndented = true });
            await _softwareFactoryFileSystem.WriteFileAsync(manifestPath, json);
            ActionLog.Global.Success($"Wrote manifest: {contract.feature}.GeneratedFiles.json"); 
        }
    }
}
