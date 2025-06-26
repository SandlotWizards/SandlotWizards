namespace SandlotWizards.AiPipelines.Models;

public class AiResult
{
    public List<GeneratedFile> GeneratedFiles { get; set; } = new();
    public List<string> Instructions { get; set; } = new();
    public string Log { get; set; } = string.Empty;
    public bool Success { get; set; }
}
