using UnityEngine;

namespace _Project.LoggingAndDebugging
{
    public class LogEntry
    {
        public string LogString { get; }
        public string StackTrace { get; }
        public LogType Type { get; }
 
        public LogEntry(string logString, string stackTrace, LogType type)
        {
            LogString = logString;
            StackTrace = stackTrace;
            Type = type;
        }
    }
}
