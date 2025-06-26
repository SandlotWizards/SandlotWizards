using System.Diagnostics;
using System.IO;
using SandlotWizards.Core.Interfaces.Windows;

namespace SandlotWizards.Core.Services.Windows
{
    /// <summary>
    /// Default implementation of <see cref="IShellCommandService"/> for executing shell commands.
    /// </summary>
    public class ShellCommandService : IShellCommandService
    {
        public string? StandardOutput { get; private set; }
        public string? StandardError { get; private set; }

        public int ExecuteCommand(string command, string arguments, string? workingDirectory = null, bool captureOutput = false)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = captureOutput,
                RedirectStandardError = captureOutput,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
            };

            using var process = new Process { StartInfo = processStartInfo };
            process.Start();

            if (captureOutput)
            {
                StandardOutput = process.StandardOutput.ReadToEnd();
                StandardError = process.StandardError.ReadToEnd();
            }

            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
