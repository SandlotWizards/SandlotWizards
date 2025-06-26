using SandlotWizards.ActionLogger;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private string EstablishWorkingFolder()
    {
        using (ActionLog.Global.BeginStep("Establishing working folder for FeatureBuild."))
        {
            var runnerId = _softwareFactoryFileSystem.CreateSoftwareFactoryWorkingRunner();
            var workingRoot = _softwareFactoryFileSystem.GetSoftwareFactoryRunnerPath(runnerId);

            _softwareFactoryFileSystem.CreateDirectory(workingRoot);

            return workingRoot;
        }
    }
}
