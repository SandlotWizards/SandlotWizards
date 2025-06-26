using SandlotWizards.SoftwareFactory.Commands;

namespace SandlotWizards.SoftwareFactory.Interfaces
{
    public interface IProjectCreateService
    {
        Task ExecuteAsync(ProjectCreateCommand command);
    }
}
