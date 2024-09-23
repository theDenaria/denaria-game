namespace _Project.LoggingAndDebugging
{
    public interface INativeShareService
    {
        void ShareFile(string path, OnFinishSharedDelegate onFinishShared = null);
        void ShareText(string subject, string text, OnFinishSharedDelegate onFinishShared = null);
    }
}