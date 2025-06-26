namespace SandlotWizards.Core.Interfaces.Windows
{
    public interface IDeploymentFileSystemService : IUtilitityFileSystemService
    {
        string LocateDeploymentRepoPath(string environment, string profile = null);
        string LocateDeploymentRepoPath(string environment, string solution, string profile = null);
        string LocateDeploymentRepoPath(string environment, string solution, string project, string profile = null);
    }
}
