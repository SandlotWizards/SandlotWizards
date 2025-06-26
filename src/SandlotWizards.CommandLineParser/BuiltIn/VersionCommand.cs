using SandlotWizards.ActionLogger;
using SandlotWizards.CommandLineParser.Core;

namespace SandlotWizards.CommandLineParser.BuiltIn;

public class VersionCommand : ICommand
{
    public Task ExecuteAsync(CommandContext context)
    {
        ActionLog.Global.Message("lore CLI version 1.0.0", ConsoleColor.Green);
        return Task.CompletedTask;
    }
}
