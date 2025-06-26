using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipeline.Interfaces;
using SandlotWizards.AiPipeline.Utilities;
using SandlotWizards.AiPipelines.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SandlotWizards.AiPipeline.Services;

public class AiExecutionService : IAiExecutionService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AiExecutionService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<List<GeneratedFile>> ExecuteAsync(IAiContract contract, CancellationToken token = default)
    {
        var systemPrompt = GetSystemPrompt();
        var userPrompt = contract.PromptText;

        var requestBody = new
        {
            model = "gpt-4-0613",
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            },
            temperature = 0.1
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
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
            .GetString() ?? "";

        if (rawContent.StartsWith("Apologies", StringComparison.OrdinalIgnoreCase))
        {
            ActionLog.Global.Error("AI returned fallback message instead of file manifest.");
            return new List<GeneratedFile>();
        }

        var files = FileManifestParser.ExtractGeneratedFilesFromJson(rawContent);
        return files;
    }

    private static string GetSystemPrompt() => @"
        You are generating source code for a command-service feature. Return the output as a JSON array of objects. Each object must include:

        path: the relative path to the file to be created (e.g., source_code/Commands/MyCommand.cs)
        content: the full source code contents of the file, as a string

        Do not explain, introduce, or annotate the output. Do not wrap in markdown. Return only the raw JSON array. The system will parse and write each file using the paths and contents you provide.
        ";
    }
