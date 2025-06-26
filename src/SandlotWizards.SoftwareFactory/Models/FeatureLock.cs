namespace SandlotWizards.SoftwareFactory.Models
{
    public class FeatureLock
    {
        public string feature { get; init; }
        public string solution { get; init; }

        public FeatureLock(string feature, string solution)
        {
            this.feature = feature;
            this.solution = solution;
        }
    }
}
