using SandlotWizards.SoftwareFactory.Commands;

namespace SandlotWizards.SoftwareFactory.Interfaces
{
    public interface IFeatureBuildService
    {
        Task ExecuteAsync(FeatureBuildCommand command);
    }
}