using SandlotWizards.SoftwareFactory.Commands;

namespace SandlotWizards.SoftwareFactory.Interfaces
{
    public interface IFeaturePlanService
    {
        Task ExecuteAsync(FeaturePlanCommand command);
    }
}