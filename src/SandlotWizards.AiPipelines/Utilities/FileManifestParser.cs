using SandlotWizards.AiPipelines.Models;
using System.Text.Json;

namespace SandlotWizards.AiPipeline.Utilities
{
   public static class FileManifestParser
    {
        public static List<GeneratedFile> ExtractGeneratedFilesFromJson(string jsonOutput)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var files = JsonSerializer.Deserialize<List<GeneratedFile>>(jsonOutput, options);
                return files ?? new List<GeneratedFile>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine("❌ Failed to parse JSON file manifest: " + ex.Message);
                return new List<GeneratedFile>();
            }
        }

        public static void WriteFilesToDisk(IEnumerable<GeneratedFile> files, string rootDirectory)
        {
            foreach (var file in files)
            {
                var fullPath = Path.Combine(rootDirectory, file.path.Replace('/', Path.DirectorySeparatorChar));
                var directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                File.WriteAllText(fullPath, file.content);
            }
        }
    }
}
