using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Interfaces.Ai
{
    public interface IAiService
    {
        /// <summary>
        /// Sends a request to the AI provider using the given system and user prompts.
        /// </summary>
        /// <param name="systemPrompt">Context-setting instructions for the assistant.</param>
        /// <param name="userPrompt">The specific task or request from the developer.</param>
        /// <param name="ct">Cancellation token for graceful shutdown.</param>
        /// <returns>Raw AI-generated response as a string.</returns>
        Task<string> GenerateAsync(string systemPrompt, string userPrompt, CancellationToken ct = default);
    }
}
