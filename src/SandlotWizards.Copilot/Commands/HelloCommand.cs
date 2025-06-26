using SandlotWizards.CommandLineParser.Core;

namespace SandlotWizards.Copilot.Commands;

public class HelloCommand : ICommand
{
    public async Task ExecuteAsync(CommandContext context)
    {
        var name = context.Arguments.TryGetValue("name", out var val) ? val : "world";
        Console.WriteLine($"Hello, {name}!");
        await Task.CompletedTask;
    }
}
