using SandlotWizards.ActionLogger;
using SandlotWizards.CommandLineParser.Core;

namespace SandlotWizards.CommandLineParser.BuiltIn;

public class GreetCommand : ICommand
{
    public Task ExecuteAsync(CommandContext context)
    {
        if (context.Arguments.TryGetValue("name", out var name) && !string.IsNullOrWhiteSpace(name))
        {
            ActionLog.Global.Message($"Hello, {name}!", ConsoleColor.Green);
        }
        else
        {
            ActionLog.Global.Message("Hello there!", ConsoleColor.Green);
        }

        return Task.CompletedTask;
    }
}
