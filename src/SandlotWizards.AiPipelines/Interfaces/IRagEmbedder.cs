namespace SandlotWizards.AiPipelines.Interfaces
{
    public interface IRagEmbedder
    {
        Task EmbedAsync(string content, Dictionary<string, object> metadata);
    }
}
