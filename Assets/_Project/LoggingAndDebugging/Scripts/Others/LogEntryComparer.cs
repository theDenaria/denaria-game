using System.Collections.Generic;

namespace _Project.LoggingAndDebugging
{
    public class LogEntryComparer : IEqualityComparer<LogEntry>
    {
        public int GetHashCode(LogEntry logEntry)
        {
            return logEntry.GetHashCode();
        }
         
        public bool Equals(LogEntry x, LogEntry y)
        {
            return x != null 
                   && y != null                    
                   && x.LogString.Equals(y.LogString)
                   && x.StackTrace.Equals(y.StackTrace)
                   && x.Type.ToString().Equals(y.Type.ToString());
        }
    }
}