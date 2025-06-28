using Microsoft.Extensions.Configuration;
using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.AiPipelines.Interfaces;
using SandlotWizards.OpenAi.Constants;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SandlotWizards.OpenAi
{
    public class OpenAiExecutionService : IAiExecutionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public OpenAiExecutionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("Missing OpenAI API key");
            _model = configuration["OpenAI:Model"] ?? "gpt-4";
        }

        public async Task<AiUnifiedResult> ExecuteAsync(AiCallContractBase contract, CancellationToken token = default)
        {
            var systemPrompt = OpenAiSystemPrompts.GlobalOutputRule + "\n" +
                               OpenAiSystemPrompts.ExecuteUnifiedResult + "\n" +
                               OpenAiSystemPrompts.GarbageCollection;

            var userPrompt = contract.PromptText;
            //var userPrompt = "Execute now";

            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                temperature = 0.1
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var request = new HttpRequestMessage(HttpMethod.Post, OpenAiConstants.ChatCompletionsHttpsUrl)
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, token);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(token);
            using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: token);

            var rawContent = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;

            
            if (rawContent.StartsWith("Apologies", StringComparison.OrdinalIgnoreCase))
            {
                ActionLog.Global.Error("AI returned fallback message instead of result.");
                throw new InvalidOperationException("AI returned an apology or fallback message. Execution aborted.");
            }

            try
            {
                return JsonSerializer.Deserialize<AiUnifiedResult>(rawContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new InvalidOperationException("AI returned null or invalid structured result.");
            }
            catch (JsonException ex)
            {
                ActionLog.Global.Error("Failed to deserialize full AiUnifiedResult. Aborting execution.");
                throw new InvalidOperationException("AI output did not match expected format.", ex);
            }
        }
    }
}
