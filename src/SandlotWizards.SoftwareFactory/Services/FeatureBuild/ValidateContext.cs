using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Commands;
using SandlotWizards.Core.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private ValidationResult ValidateContext(FeatureBuildCommand command)
    {
        using (ActionLog.Global.BeginStep("Validating CLI input parameters for feature build."))
        {
            if (string.IsNullOrWhiteSpace(command.solution))
            {
                return ValidationResult.Fail("Missing required input: --solution");
            }

            if (string.IsNullOrWhiteSpace(command.feature))
            {
                return ValidationResult.Fail("Missing required input: --feature");
            }


            ActionLog.Global.Info("CLI input parameters validated successfully for feature build.");
        }
        return ValidationResult.Success();
    }
}
