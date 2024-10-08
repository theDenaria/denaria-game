namespace _Project.LoggingAndDebugging
{
    public interface IFileOperationService
    {
        void CreateDirectory(string path);
        void DeleteDirectory(string path);
        void DeleteFile(string path);
        void DeleteFiles(string path, int count);
        void CompressDirectory(string path, string zipPath);
        void SaveValue(string path, string value);
        string GetValue(string path);
    }
}
