using SandlotWizards.AiPipelines.Contracts;
using System.Text;

namespace SandlotWizards.SoftwareFactory.Services.FeatureBuild.Builder;

internal static class PromptBuilder
{
    public static string BuildUserPrompt(string feature, string designSpecText, List<RagChunk> ragChunks, string executionPlanText)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"You are generating source code for the feature: \"{feature}\".");
        sb.AppendLine("Use the design specification and relevant standards provided below to implement a command-service pipeline according to Sandlot Software Factory architecture principles.");
        sb.AppendLine("\n---\n");

        sb.AppendLine("### 🔷 Design Specification");
        sb.AppendLine(designSpecText.Trim());
        sb.AppendLine("\n---\n");

        if (ragChunks?.Any() == true)
        {
            sb.AppendLine("### 📘 Relevant Standards");

            foreach (var chunk in ragChunks)
            {
                sb.AppendLine($"[{chunk.Title}]");
                sb.AppendLine(chunk.Content.Trim());
                sb.AppendLine();
            }

            sb.AppendLine("---\n");
        }

        //sb.AppendLine("### 📜 Execution Plan");
        sb.AppendLine(executionPlanText.Trim());
        //sb.AppendLine("\n---\n");

        sb.AppendLine("### 🛠️ Instructions");
        sb.AppendLine("- Only use information provided above.");
        sb.AppendLine("- Do not assume functionality not stated.");
        sb.AppendLine("- Return the output as a single JSON object, as described in the system instructions.");

        return sb.ToString();
    }
}
