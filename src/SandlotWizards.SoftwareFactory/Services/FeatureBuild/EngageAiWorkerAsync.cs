using SandlotWizards.ActionLogger;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Adapters;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Models;
using System.Xml.Serialization;
using SandlotWizards.SoftwareFactory.Services.FeatureBuild.Builder;
using SandlotWizards.AiPipelines.Contracts;
using SandlotWizards.AiPipelines.Helpers;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService
{
    private async Task<Contract> EngageAiWorkerAsync(Contract contract)
    {
        using (ActionLog.Global.BeginStep("Engaging AI pipeline to generate feature artifacts."))
        {
            var adapter = new FeatureBuildAiContractAdapter(contract);

            var result = await _aiExecutionService.ExecuteAsync(adapter);

            contract.WorkingContext.GeneratedFiles = result.GeneratedFiles;

            if (result.FanoutPlan.Steps.Any())
                await SaveExecutionPlanAsync(result.FanoutPlan, contract);

            if (result.ShellCommands.Any())
            {
                foreach (var cmd in result.ShellCommands)
                    _shellCommandService.ExecuteCommand(cmd.Command, cmd.Arguments);
            }

            return contract;
        }
    }

    private async Task SaveExecutionPlanAsync(FanoutPlan plan, Contract contract)
    {
        using (ActionLog.Global.BeginStep("Saving ExecutionPlan.yaml to working directory."))
        {
            var executionPlanPath = Path.Combine(contract.WorkingDirectory, "ExecutionPlan.yaml");
            var yaml = YamlSerializer.Serialize(plan);
            await File.WriteAllTextAsync(executionPlanPath, yaml);
            contract.ExecutionPlanPath = executionPlanPath;
            contract.ExecutionPlanText = yaml;
        }
    }

    private async Task SavePromptCallsAsync(List<AiCallContractBase> calls, Contract contract)
    {
        using (ActionLog.Global.BeginStep("Saving Prompt.md and Contract.md to working directory."))
        {
            var promptPath = Path.Combine(contract.WorkingDirectory, "Prompt.md");
            var contractPath = Path.Combine(contract.WorkingDirectory, "Contract.md");

            var promptText = string.Join("\n\n", calls.SelectMany(c => c.Messages)
                .Where(m => m.Role == "user")
                .Select(m => m.Content));

            var contractText = string.Join("\n\n", calls.SelectMany(c => c.Messages)
                .Where(m => m.Role == "system")
                .Select(m => m.Content));

            await File.WriteAllTextAsync(promptPath, promptText);
            await File.WriteAllTextAsync(contractPath, contractText);

            contract.ContractText = contractText;
            contract.ContractPath = contractPath;
        }
    }
}
