using SandlotWizards.AiPipelines.Models;

namespace SandlotWizards.AiPipeline.Interfaces
{
    public interface IAiExecutionService
    {
        Task<List<GeneratedFile>> ExecuteAsync(IAiContract contract, CancellationToken token = default);
    }
}
