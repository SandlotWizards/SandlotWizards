using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.SoftwareFactory.Commands;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private Contract InitializeContractThatHoldsAiContext(FeatureBuildCommand command, string workingRoot)
    {
        using (ActionLog.Global.BeginStep("Initializing AI contract with feature context."))
        {
            var executionContextId = Path.GetFileName(workingRoot);

            return new Contract
            {
                solution = command.solution,
                feature = command.feature,
                ExecutionContextId = executionContextId,
                WorkingDirectory = workingRoot,
                WorkingContext = new WorkingContext()
            };
        }
    }
}
