using Microsoft.Extensions.Options;
using SandlotWizards.AiPipelines.Interfaces;
using SandlotWizards.Core.Configuration;

namespace SandlotWizards.AiPipelines.Working
{
    /// <summary>
    /// Resolves file paths for the AI Software Factory Working directory based on developer configuration.
    /// </summary>
    public class WorkingPathResolver : IWorkingPathResolver
    {
        private readonly string _workingRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingPathResolver"/> class.
        /// </summary>
        /// <param name="config">The configured GitHub repository root path.</param>
        public WorkingPathResolver(IOptions<GitHubConfig> config)
        {
            var reposRoot = config.Value.Repos;

            if (string.IsNullOrWhiteSpace(reposRoot))
            {
                var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                reposRoot = Path.Combine(userProfile, "source", "repos");
            }

            _workingRoot = Path.Combine(reposRoot, "Sandlot.Copilot", "Working");
        }

        public string GetPromptPath() =>
            Path.Combine(_workingRoot, "Prompts", "default.prompt.md");

        public string GetSpecDocPath() =>
            Path.Combine(_workingRoot, "SpecDocs", "canonical-spec.md");

        public string GetDesignDocPath(string serviceName) =>
            Path.Combine(_workingRoot, "Designs", $"{serviceName}.design.md");

        public string GetInjectFilePath(string serviceName) =>
            Path.Combine(_workingRoot, "Outputs", $"{serviceName}.template.cs");

        public string GetContractPath(string serviceName) =>
            Path.Combine(_workingRoot, "Contracts", $"{serviceName}.template.contract.json");
    }
}
