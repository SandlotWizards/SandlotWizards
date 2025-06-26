using SandlotWizards.ActionLogger;
using SandlotWizards.CommandLineParser.Core;
using SandlotWizards.Core.Helpers;
using SandlotWizards.SoftwareFactory.Interfaces;

namespace SandlotWizards.SoftwareFactory.Commands;

public sealed class ProjectCreateCommand : ICommand
{
    public string? Solution { get; set; } = string.Empty;
    public string? ProjectType { get; set; } = string.Empty;

    public async Task ExecuteAsync(CommandContext context)
    {
        ActionLog.Global.BeginStep($"Executing feature plan command {PropertyFormatter.Format(this)}.");
        var service = context.Resolve<IProjectCreateService>();
        await service.ExecuteAsync(this);
    }
}
