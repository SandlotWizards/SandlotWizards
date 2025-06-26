using SandlotWizards.SoftwareFactory.Commands;

namespace SandlotWizards.SoftwareFactory.Interfaces
{
    public interface IFeatureDesignService
    {
        Task ExecuteAsync(FeatureDesignCommand command);
    }
}