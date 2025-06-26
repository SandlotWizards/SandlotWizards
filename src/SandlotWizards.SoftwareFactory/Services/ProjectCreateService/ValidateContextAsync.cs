using System;
using System.Threading.Tasks;
using Commands;

namespace Services.ProjectCreateService
{
    internal partial class ProjectCreateService
    {
        private async Task ValidateContextAsync(ProjectCreateCommand command)
        {
            ActionLog.Global.BeginStep("Validating context");

            if (string.IsNullOrEmpty(command.Solution))
            {
                ActionLog.Global.Error("Solution name must not be empty");
                throw new ArgumentException("Solution name must not be empty", nameof(command.Solution));
            }

            if (command.ProjectType != "domain")
            {
                ActionLog.Global.Error("Unsupported project type");
                throw new ArgumentException("Unsupported project type", nameof(command.ProjectType));
            }

            ActionLog.Global.EndStep("Validating context");
        }
    }
}