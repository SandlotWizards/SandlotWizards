namespace SandlotWizards.Core.Interfaces.Windows
{
    public interface IUtilitityFileSystemService
    {
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        void DeleteDirectory(string path, bool recursive = true);
        void EmptyDirectory(string path);
        bool FileExists(string path);
        Task WriteFileAsync(string path, string content, bool append = false);
        Task<string> ReadFileAsync(string path);
        void DeleteFile(string path);
        void CopyFile(string source, string destination, bool overwrite = true);
        void MoveFile(string source, string destination, bool overwrite = true);
    }
}
