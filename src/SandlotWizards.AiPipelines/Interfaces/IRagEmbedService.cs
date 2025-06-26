namespace SandlotWizards.AiPipelines.Interfaces
{
    public interface IRagEmbedService
    {
        Task EmbedFromFileAsync(string chunkFilePath);
    }
}