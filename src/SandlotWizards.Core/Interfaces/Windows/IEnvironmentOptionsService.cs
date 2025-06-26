namespace SandlotWizards.Core.Interfaces.Windows
{
    /// <summary>
    /// Provides access to connection options for both the environment and SolutionCentral.
    /// </summary>
    public interface IEnvironmentOptionsService
    {
        // Environment database connection

        /// <summary>
        /// Gets the environment SQL Server hostname.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets the environment SQL Server login username.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets the environment SQL Server login password.
        /// </summary>
        string Password { get; }

        // SolutionCentral database connection

        /// <summary>
        /// Gets the SolutionCentral SQL Server hostname.
        /// </summary>
        string ScHost { get; }

        /// <summary>
        /// Gets the SolutionCentral SQL Server login username.
        /// </summary>
        string ScUserId { get; }

        /// <summary>
        /// Gets the SolutionCentral SQL Server login password.
        /// </summary>
        string ScPassword { get; }
    }
}
