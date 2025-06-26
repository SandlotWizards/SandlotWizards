using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private void LogSuccessfulCompletion(Contract contract)
    {
        using (ActionLog.Global.BeginStep($"✅ FeatureBuild completed successfully for: solution = '{contract.solution}', feature = '{contract.feature}'")) { } ;
    }
}
