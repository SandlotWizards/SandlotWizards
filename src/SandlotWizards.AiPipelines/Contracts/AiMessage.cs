namespace SandlotWizards.AiPipelines.Contracts;

public class AiMessage
{
    public string Role { get; set; } = "user"; // or "system"
    public string Content { get; set; } = string.Empty;
}