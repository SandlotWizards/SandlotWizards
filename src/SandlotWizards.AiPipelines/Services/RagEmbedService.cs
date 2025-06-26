using System.Text.Json;
using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Interfaces;

namespace SandlotWizards.AiPipelines.Services;

public class RagEmbedService : IRagEmbedService
{
    private readonly IRagEmbedder _embedder;

    public RagEmbedService(IRagEmbedder embedder)
    {
        _embedder = embedder;
    }

    public async Task EmbedFromFileAsync(string chunkFilePath)
    {
        ActionLog.Global.BeginStep($"Embedding RAG chunks from: {chunkFilePath}");

        if (!File.Exists(chunkFilePath))
        {
            ActionLog.Global.Error($"File not found: {chunkFilePath}");
            return;
        }

        var json = await File.ReadAllTextAsync(chunkFilePath);
        var chunks = JsonSerializer.Deserialize<List<RagChunk>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (chunks is null || chunks.Count == 0)
        {
            ActionLog.Global.Error("No chunks found or failed to parse chunk file.");
            return;
        }

        foreach (var chunk in chunks)
        {
            await _embedder.EmbedAsync(chunk.Content, chunk.Metadata);
            ActionLog.Global.Info($"Embedded chunk: {chunk.Title}"); 
        }

        ActionLog.Global.Success("RAG embedding complete.");
    }

    private class RagChunk
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
