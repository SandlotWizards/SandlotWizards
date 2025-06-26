using System;
using System.Threading.Tasks;
using Commands;

namespace Services.ProjectCreateService
{
    internal partial class ProjectCreateService
    {
        internal async Task OrchestrateAsync(ProjectCreateCommand command)
        {
            ActionLog.Global.BeginStep("Orchestrating Project Create Command");

            try
            {
                await ValidateContextAsync(command);
                await AddDomainProjectAsync(command);

                ActionLog.Global.EndStep("Orchestrating Project Create Command");
            }
            catch (Exception ex)
            {
                ActionLog.Global.Error("Failed to orchestrate Project Create Command", ex);
                throw;
            }
        }
    }
}