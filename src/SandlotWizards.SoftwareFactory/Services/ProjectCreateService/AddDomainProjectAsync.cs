using System;
using System.Threading.Tasks;
using Commands;

namespace Services.ProjectCreateService
{
    internal partial class ProjectCreateService
    {
        private async Task AddDomainProjectAsync(ProjectCreateCommand command)
        {
            ActionLog.Global.BeginStep("Adding domain project");

            // TODO: Implement domain project creation logic here

            ActionLog.Global.EndStep("Adding domain project");
        }
    }
}