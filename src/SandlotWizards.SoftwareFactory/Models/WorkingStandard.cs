namespace SandlotWizards.SoftwareFactory.Models;

public class WorkingStandard
{
    public required string Key { get; set; }                 // e.g., "CommandService"
    public required string FileName { get; set; }            // e.g., "CommandService.DesignPattern.Standard.md"
    public required string Path { get; set; }                // Full path in FileStore
    public required string Text { get; set; }                // File contents
}
