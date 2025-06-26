using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Interfaces;
using SandlotWizards.AiPipelines.Models;
using System.Diagnostics;
using System.Text;

namespace SandlotWizards.AiPipelines.Services
{
    public class RagRetriever : IRagRetriever, IRagEmbedder
    {
        private readonly string _scriptPath;

        public RagRetriever(string scriptPath = @"C:\RagIndex\retriever.py")
        {
            _scriptPath = scriptPath;
        }

        public async Task<List<RagResult>> QueryAsync(string query, int count = 3)
        {
            var results = new List<RagResult>();

            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{_scriptPath} --query \"{query}\" --results {count}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            using var reader = process.StandardOutput;

            string? line;
            RagResult? current = null;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("Result #"))
                {
                    current = new RagResult();
                    results.Add(current);
                }
                else if (line.StartsWith("- File:"))
                {
                    current!.File = line.Replace("- File:", "").Trim();
                }
                else if (line.StartsWith("- Section:"))
                {
                    current!.Section = line.Replace("- Section:", "").Trim();
                }
                else if (line.StartsWith("- Content:"))
                {
                    current!.Content = "";
                }
                else if (line.StartsWith("------"))
                {
                    continue;
                }
                else if (current?.Content != null)
                {
                    current.Content += line + Environment.NewLine;
                }
            }

            await process.WaitForExitAsync();
            return results;
        }

        public async Task EmbedAsync(string content, Dictionary<string, object> metadata)
        {
            if (metadata == null || metadata.Count == 0)
                throw new InvalidOperationException("Cannot embed: metadata is null or empty.");

            var metadataJson = System.Text.Json.JsonSerializer.Serialize(metadata);

            var tempContentFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempContentFile, content);

            var tempMetadataFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempMetadataFile, metadataJson);

            var argsBuilder = new StringBuilder();
            argsBuilder.Append($"{_scriptPath}");
            argsBuilder.Append(" --embed");
            argsBuilder.Append($" --content-file \"{tempContentFile}\"");
            argsBuilder.Append($" --metadata-file \"{tempMetadataFile}\"");

            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = argsBuilder.ToString(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            string stdout = await process.StandardOutput.ReadToEndAsync();
            string stderr = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            File.Delete(tempContentFile);
            File.Delete(tempMetadataFile);

            if (process.ExitCode != 0)
            {
                throw new Exception($"Embedding failed: {stderr}");
            }

            if (!string.IsNullOrEmpty(stdout.Trim())) Console.WriteLine(stdout);
        }
    }
}