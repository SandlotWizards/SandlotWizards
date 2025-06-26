namespace SandlotWizards.Core.Configuration
{
    /// <summary>
    /// Represents GitHub configuration settings for accessing repositories.
    /// </summary>
    public class GitHubConfig
    {
        /// <summary>
        /// The name of the GitHub organization.
        /// </summary>
        public string Organization { get; set; } = string.Empty;

        /// <summary>
        /// The personal access token or API token used for GitHub authentication.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The path or identifier for the local repositories directory.
        /// </summary>
        public string Repos { get; set; } = string.Empty;
    }
}
