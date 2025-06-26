using SandlotWizards.Core.Interfaces.Windows;

namespace SandlotWizards.Core.Services.Windows
{
    /// <summary>
    /// Unified implementation of all core file system services, including developer, deployment, working, and filestore paths.
    /// 
    /// All SoftwareFactory systems, services, and pipelines that reference working copies or FileStore-based resources
    /// shall use these interfaces and this implementation for all file and directory operations. Direct usage of raw
    /// file paths, hardcoded directories, or system-level file access APIs is explicitly prohibited.
    /// 
    /// This service guarantees consistent resolution logic, testability, and future extensibility across all software
    /// built under the SoftwareFactory governance model.
    /// </summary>
    public class FileSystemService :
        IDeveloperFileSystemService,
        IDeploymentFileSystemService,
        ISoftwareFactoryWorkingFileSystemService,
        IFileStoreFileSystemService
    {
        private const string DefaultDeveloperRoot = "source\\repos";

        private static string WorkingRoot =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SoftwareFactory", "Working");

        public string GetSoftwareFactoryWorkingRoot() => WorkingRoot;

        public string GetSoftwareFactoryRunnerPath(string runner) => Path.Combine(WorkingRoot, runner);

        public string CreateSoftwareFactoryWorkingRunner()
        {
            string runner = Guid.NewGuid().ToString();
            Directory.CreateDirectory(Path.Combine(WorkingRoot, runner));
            return runner;
        }

        public string LocateSoftwareFactoryWorkingRepoPath(string runner, string repoName)
        {
            return Path.Combine(WorkingRoot, Validate(runner), "repos", Validate(repoName));
        }

        public string LocateSoftwareFactoryWorkingProjectPath(string runner, string repoName, string project)
        {
            return Path.Combine(LocateSoftwareFactoryWorkingSrcPath(runner, repoName), Validate(project));
        }

        public string LocateSoftwareFactoryWorkingDocsPath(string runner, string repoName)
        {
            return Path.Combine(LocateSoftwareFactoryWorkingRepoPath(runner, repoName), "docs");
        }

        public string LocateSoftwareFactoryWorkingSrcPath(string runner, string repoName)
        {
            return Path.Combine(LocateSoftwareFactoryWorkingRepoPath(runner, repoName), "src");
        }

        public string LocateSoftwareFactoryWorkingTestsPath(string runner, string repoName)
        {
            return Path.Combine(LocateSoftwareFactoryWorkingRepoPath(runner, repoName), "tests");
        }

        private static string FileStoreRoot =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SoftwareFactory", "FileStore", "SandlotWizards.SoftwareFactory.Specs");

        public string GetFileStoreRoot() => FileStoreRoot;

        public string LocateFileStoreFolder(string folderName)
        {
            return Path.Combine(FileStoreRoot, Validate(folderName));
        }

        public string LocateFileStoreFile(string folderName, string fileName)
        {
            return Path.Combine(LocateFileStoreFolder(folderName), Validate(fileName));
        }

        public string GetDeveloperRootPath(string rootOverride = null)
        {
            var userRoot = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(rootOverride ?? Path.Combine(userRoot, DefaultDeveloperRoot));
        }

        public string LocateDeveloperSolutionPath(string solution, string rootOverride = null)
        {
            return Path.Combine(GetDeveloperRootPath(rootOverride), solution);
        }

        public string LocateDeveloperDotNetRoot(string solution, string rootOverride = null)
        {
            return Path.Combine(LocateDeveloperSolutionPath(solution, rootOverride), "src");
        }

        public string LocateDeveloperProjectPath(string solution, string project, string rootOverride = null)
        {
            if (string.IsNullOrWhiteSpace(solution))
                throw new ArgumentException("Solution name must be provided when specifying a project path.");

            return Path.Combine(LocateDeveloperDotNetRoot(solution, rootOverride), project);
        }

        public string GetArchitectureDocsPath(string rootOverride = null)
        {
            return LocateDeveloperProjectPath("Sandlot.ArchitectureDocs", "Sandlot.ArchitectureDocs.Content", rootOverride);
        }

        public string LocateDeploymentRepoPath(string environment, string profile = null)
        {
            ValidateEnvironmentProfileRules(environment, profile);
            return environment.ToLower() == "localhost"
                ? Path.Combine("C:\\Solution_Deployments", "localhost", "LocalProfiles", profile!)
                : Path.Combine("C:\\Solution_Deployments", environment);
        }

        public string LocateDeploymentRepoPath(string environment, string solution, string profile = null)
        {
            return Path.Combine(LocateDeploymentRepoPath(environment, profile), "Solutions", solution);
        }

        public string LocateDeploymentRepoPath(string environment, string solution, string project, string profile = null)
        {
            return Path.Combine(LocateDeploymentRepoPath(environment, solution, profile), "src", project);
        }

        private void ValidateEnvironmentProfileRules(string environment, string profile)
        {
            var isLocal = string.Equals(environment, "localhost", StringComparison.OrdinalIgnoreCase);
            if (isLocal && string.IsNullOrWhiteSpace(profile))
                throw new ArgumentException("Profile must be provided when environment is 'localhost'.");
            if (!isLocal && profile is not null)
                throw new ArgumentException("Profile must be null when environment is not 'localhost'.");
        }

        public bool DirectoryExists(string path) => Directory.Exists(path);
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public void DeleteDirectory(string path, bool recursive = true)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }

        public void EmptyDirectory(string path)
        {
            if (!Directory.Exists(path)) return;
            foreach (var file in Directory.GetFiles(path)) File.Delete(file);
            foreach (var dir in Directory.GetDirectories(path)) Directory.Delete(dir, true);
        }

        public bool FileExists(string path) => File.Exists(path);

        public async Task WriteFileAsync(string path, string content, bool append = false)
        {
            if (append) await File.AppendAllTextAsync(path, content);
            else await File.WriteAllTextAsync(path, content);
        }

        public async Task<string> ReadFileAsync(string path) => await File.ReadAllTextAsync(path);

        public void DeleteFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        public void CopyFile(string source, string destination, bool overwrite = true)
        {
            File.Copy(source, destination, overwrite);
        }

        public void MoveFile(string source, string destination, bool overwrite = true)
        {
            if (overwrite && File.Exists(destination)) File.Delete(destination);
            File.Move(source, destination);
        }

        private static string Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Path segment must be provided and non-empty.");
            return input;
        }

        
    }
}
