using SandlotWizards.ActionLogger;
using SandlotWizards.CommandLineParser.Core;
using SandlotWizards.Core.Helpers;
using SandlotWizards.SoftwareFactory.Interfaces;

namespace SandlotWizards.SoftwareFactory.Commands;

public sealed class FeatureBuildCommand : ICommand
{
    public required string solution { get; init; }
    public required string feature { get; init; }

    public async Task ExecuteAsync(CommandContext context)
    {
        ActionLog.Global.Message($"Executing feature build command {PropertyFormatter.Format(this)}.");
        var service = context.Resolve<IFeatureBuildService>();
        await service.ExecuteAsync(this);
        ActionLog.Global.Message($"Completed feature build command {PropertyFormatter.Format(this)}.");

    }
}
