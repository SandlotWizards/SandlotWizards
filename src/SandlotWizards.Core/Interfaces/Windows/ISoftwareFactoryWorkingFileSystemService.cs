namespace SandlotWizards.Core.Interfaces.Windows
{
    public interface ISoftwareFactoryWorkingFileSystemService : IUtilitityFileSystemService
    {
        string GetSoftwareFactoryWorkingRoot();
        string GetSoftwareFactoryRunnerPath(string runner);
        string CreateSoftwareFactoryWorkingRunner();
        string LocateSoftwareFactoryWorkingRepoPath(string runner, string repoName);
        string LocateSoftwareFactoryWorkingProjectPath(string runner, string repoName, string project);
        string LocateSoftwareFactoryWorkingDocsPath(string runner, string repoName);
        string LocateSoftwareFactoryWorkingSrcPath(string runner, string repoName);
        string LocateSoftwareFactoryWorkingTestsPath(string runner, string repoName);
    }
}
