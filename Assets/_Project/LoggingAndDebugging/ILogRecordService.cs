using UnityEngine;

namespace _Project.LoggingAndDebugging
{
    public interface ILogRecordService
    {
        void HandleLogMessageReceived(string condition, string stacktrace, LogType type);
        void SubscribeToLogs();
    }
}