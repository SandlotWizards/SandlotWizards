namespace SandlotWizards.AiPipelines.Contracts
{
    public class RagChunk
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string Source { get; set; } = string.Empty;
    }
}
