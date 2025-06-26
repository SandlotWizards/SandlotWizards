namespace SandlotWizards.Core.Interfaces.Windows
{
    /// <summary>
    /// Provides functionality to execute shell commands and capture results.
    /// </summary>
    public interface IShellCommandService
    {
        /// <summary>
        /// Executes a shell command and returns the process exit code.
        /// </summary>
        int ExecuteCommand(string command, string arguments, string? workingDirectory = null, bool captureOutput = false);

        /// <summary>
        /// Gets the standard output from the last executed command (if captured).
        /// </summary>
        string? StandardOutput { get; }

        /// <summary>
        /// Gets the standard error from the last executed command (if captured).
        /// </summary>
        string? StandardError { get; }
    }
}
