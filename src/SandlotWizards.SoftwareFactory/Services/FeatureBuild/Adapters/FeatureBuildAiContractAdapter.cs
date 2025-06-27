using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Builder;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;

namespace SandlotWizards.SoftwareFactory.Services.FeatureBuild.Adapters
{
    public class FeatureBuildAiContractAdapter : AiCallContractBase
    {
        private readonly Contract _contract;

        public FeatureBuildAiContractAdapter(Contract contract)
        {
            _contract = contract;
            ExecutionContextId = contract.ExecutionContextId;
            Standards = contract.WorkingContext.Standards;
            Messages = [ new AiMessage { Role="user", Content = PromptBuilder.BuildUserPrompt(
                _contract.feature,
                _contract.DesignSpecText,
                _contract.WorkingContext.RagChunks
            )}];
        }
    }
}
