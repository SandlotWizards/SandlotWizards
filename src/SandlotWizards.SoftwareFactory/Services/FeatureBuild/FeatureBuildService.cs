using Microsoft.Extensions.Options;
using SandlotWizards.ActionLogger;
using SandlotWizards.AiPipelines.Interfaces;
using SandlotWizards.Core.Configuration;
using SandlotWizards.Core.Interfaces.Windows;
using SandlotWizards.SoftwareFactory.Commands;
using SandlotWizards.SoftwareFactory.Interfaces;

namespace SandlotWizards.SoftwareFactory.Services;

internal partial class FeatureBuildService : IFeatureBuildService
{
    private readonly ISoftwareFactoryWorkingFileSystemService _softwareFactoryFileSystem;
    private readonly IFileStoreFileSystemService _fileStoreFileSystem;
    private readonly IShellCommandService _shellCommandService;
    private readonly GitHubConfig _gitHubConfig;
    private readonly IRagRetriever _ragRetriever;
    private readonly IAiExecutionService _aiExecutionService;

    public FeatureBuildService(
        ISoftwareFactoryWorkingFileSystemService softwareFactoryFileSystem,
        IFileStoreFileSystemService fileStoreFileSystem,
        IShellCommandService shellCommandService,
        IOptions<GitHubConfig> gitHubConfig,
        IRagRetriever ragRetriever,
        IAiExecutionService aiExecutionService)
    {
        _softwareFactoryFileSystem = softwareFactoryFileSystem;
        _fileStoreFileSystem = fileStoreFileSystem;
        _shellCommandService = shellCommandService;
        _gitHubConfig = gitHubConfig.Value;
        _ragRetriever = ragRetriever;
        _aiExecutionService = aiExecutionService;
    }

    public async Task ExecuteAsync(FeatureBuildCommand command)
    {
        using (ActionLog.Global.BeginStep("Starting feature build orchestrator."))
        {
            var validationResult = ValidateContext(command);
            if (!validationResult.IsValid)
            {
                ActionLog.Global.Error($"Feature build aborted: {validationResult.ErrorMessage}");
                return;
            }

            try
            {
                var workingRoot = EstablishWorkingFolder();
                var contract = InitializeContractThatHoldsAiContext(command, workingRoot);

                OpenSoftwareRepositoryForUpdates(contract);
                contract = await AddFeatureDesignSpecificationsToContractAsync(contract, workingRoot);
                OpenFileStoreForSpecifications(contract);
                contract = await AddRelevantStandardsToContractAsync(contract);
                contract = await AddRagInferredKnowledgeToContractAsync(contract);
                contract = await EngageAiWorkerAsync(contract);

                await RemoveOldVersionObjectsFromRepoAsync(contract);
                await AddNewVersionObjectsToRepoAsync(contract);
                SaveChangesToSoftwareRepository(contract);
                RemoveWorkingFolder(contract);
            }
            catch (Exception ex)
            {
                ActionLog.Global.Error($"Pipeline failed: {ex.Message}");
            }
        }
    }
}
