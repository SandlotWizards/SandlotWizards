using SandlotWizards.ActionLogger;
using SandlotWizards.CommandLineParser.Core;
using SandlotWizards.AiPipelines.Interfaces;

namespace SandlotWizards.AiPipelines.Commands;

public sealed class RagEmbedCommand : ICommand
{
    public required string chunkfile { get; init; }

    public async Task ExecuteAsync(CommandContext context)
    {
        ActionLog.Global.BeginStep($"Embedding RAG chunks from: {chunkfile}");
        var embedService = context.Resolve<IRagEmbedService>();
        await embedService.EmbedFromFileAsync(chunkfile);
    }
}
