namespace SandlotWizards.Core.Interfaces.Windows
{
    public interface IDeveloperFileSystemService : IUtilitityFileSystemService
    {
        string GetDeveloperRootPath(string rootOverride = null);
        string LocateDeveloperSolutionPath(string solution, string rootOverride = null);
        string LocateDeveloperDotNetRoot(string solution, string rootOverride = null);
        string LocateDeveloperProjectPath(string solution, string project, string rootOverride = null);
        string GetArchitectureDocsPath(string rootOverride = null);
    }
}
