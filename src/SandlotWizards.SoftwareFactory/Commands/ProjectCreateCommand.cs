using System;
using System.Threading.Tasks;

namespace Commands
{
    public class ProjectCreateCommand : ICommand
    {
        public string Solution { get; set; }
        public string ProjectType { get; set; }

        public ProjectCreateCommand(string solution, string projectType)
        {
            Solution = solution;
            ProjectType = projectType;
        }

        public async Task ExecuteAsync()
        {
            var service = new Services.ProjectCreateService.ProjectCreateService();
            await service.OrchestrateAsync(this);
        }
    }
}