using SandlotWizards.AiPipelines.Models;

namespace SandlotWizards.SoftwareFactory.Models;

public class WorkingContext
{
    public List<WorkingStandard> Standards { get; set; } = new();
    public List<RagChunk> RagChunks { get; set; } = new(); 
    
    public List<GeneratedFile> GeneratedFiles { get; set; } = new();
    public List<string> Instructions { get; set; } = new();
}
