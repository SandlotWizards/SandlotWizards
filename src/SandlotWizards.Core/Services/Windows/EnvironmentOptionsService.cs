using Microsoft.Extensions.Configuration;
using SandlotWizards.Core.Interfaces.Windows;
using System;

namespace SandlotWizards.Core.Services.Windows
{
    /// <summary>
    /// Provides environment and SolutionCentral connection settings from configuration or environment variables.
    /// </summary>
    public class EnvironmentOptionsService : IEnvironmentOptionsService
    {
        public string Host { get; }
        public string UserId { get; }
        public string Password { get; }

        public string ScHost { get; }
        public string ScUserId { get; }
        public string ScPassword { get; }

        public EnvironmentOptionsService(IConfiguration configuration)
        {
            Host = configuration["ENV_HOST"] ?? throw new InvalidOperationException("ENV_HOST is not set.");
            UserId = configuration["ENV_USER"] ?? throw new InvalidOperationException("ENV_USER is not set.");
            Password = configuration["ENV_PASSWORD"] ?? throw new InvalidOperationException("ENV_PASSWORD is not set.");

            ScHost = configuration["SC_HOST"] ?? throw new InvalidOperationException("SC_HOST is not set.");
            ScUserId = configuration["SC_USER"] ?? throw new InvalidOperationException("SC_USER is not set.");
            ScPassword = configuration["SC_PASSWORD"] ?? throw new InvalidOperationException("SC_PASSWORD is not set.");
        }
    }
}
