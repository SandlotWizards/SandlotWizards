using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;
using SandlotWizards.AiPipelines.Models;

namespace SandlotWizards.SoftwareFactory.Services.FeatureBuild.Adapters
{
    public class FeatureBuildAiContractAdapter : IAiContract
    {
        private readonly Contract _source;

        public FeatureBuildAiContractAdapter(Contract source)
        {
            _source = source;
        }

        public string PromptText =>
            _source.DesignSpecText + "\n" + _source.ContractText;

        public Dictionary<string, string> ContextFiles =>
            new()
            {
                { "DesignSpec.md", _source.DesignSpecText },
                { "ExecutionPlan.md", _source.ExecutionPlanText },
                { "Contract.md", _source.ContractText }
            };

        public string ExecutionContextId => _source.ExecutionContextId;
    }
}
