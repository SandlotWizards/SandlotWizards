using SandlotWizards.AiPipelines.Contracts;

namespace SandlotWizards.AiPipelines.Interfaces
{
    public interface IAiExecutionService
    {
        Task<AiUnifiedResult> ExecuteAsync(AiCallContractBase contract, CancellationToken token = default);
    }
}
