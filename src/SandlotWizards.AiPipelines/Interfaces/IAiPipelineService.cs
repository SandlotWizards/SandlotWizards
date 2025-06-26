using SandlotWizards.AiPipelines.Models;

namespace SandlotWizards.AiPipelines.Interfaces
{
    public interface IAiPipelineService
    {
        Task<List<GeneratedFile>> ExecuteAsync(IAiContract contract, CancellationToken token = default);
    }
}