using SandlotWizards.AiPipelines.Models;

namespace SandlotWizards.AiPipelines.Interfaces
{
    public interface IRagRetriever
    {
        Task<List<RagResult>> QueryAsync(string query, int count = 3);
    }
}

