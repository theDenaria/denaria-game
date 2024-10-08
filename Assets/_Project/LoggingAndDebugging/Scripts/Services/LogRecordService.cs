using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace _Project.LoggingAndDebugging
{
    //NonLazy
    public class LogRecordService : ILogRecordService//WARNING: Do not Debug.Log() inside this class, it might create endless loop.
    {
        [Inject]
        public INativeShareService NativeShareServiceInstance {get; set;}
        
        const int INITIAL_CHARACTER_CAPACITY = 15000;
        const int CHARACTER_CAPACITY_THRESHOLD = 120000;
        const int MAXIMUM_NUMBER_OF_RECORDED_LOGS = 700;
        const int MAXIMUM_NUMBER_OF_RECORDED_ERROR_LOGS = 150;
        const int MAXIMUM_NUMBER_OF_RECORDED_DISTINCT_LOGS = 300;
        
        private List<LogEntry> _logEntries;
        private List<LogEntry> _errorLogEntries;
        private List<LogEntry> _logEntriesWithoutDuplicates;
        
        private LogEntryComparer _logEntryComparer;

        [Construct]
        public LogRecordService()//WARNING: Some logs thrown at the beginning may not be recorded
            //because the execution order of nonLazy Services are currently random.
        {
            _logEntries = new List<LogEntry>();
            _errorLogEntries = new List<LogEntry>();
            _logEntriesWithoutDuplicates = new List<LogEntry>();

            _logEntryComparer = new LogEntryComparer();
        }
        
        public void SubscribeToLogs()
        {
            Application.logMessageReceivedThreaded += HandleLogMessageReceived;
            
            SROptions.OnCopyAllLogsButtonPressed += HandleCopyAllLogsButtonPressed;
            SROptions.OnCopyAllErrorLogsButtonPressed += HandleCopyAllErrorLogsButtonPressed;
            SROptions.OnCopyAllDistinctLogsButtonPressed += HandleCopyAllDistinctLogsButtonPressed;
            
            SROptions.OnSendAllLogsButtonPressed += HandleSendAllLogsButtonPressed;
            SROptions.OnSendAllErrorLogsButtonPressed += HandleSendAllErrorLogsButtonPressed;
            SROptions.OnSendAllDistinctLogsButtonPressed += HandleSendAllDistinctLogsButtonPressed;
        }

        public void HandleLogMessageReceived(string logString, string stackTrace, LogType type)
        {
            //#if !UNITY_EDITOR //You can make the functions only work outside UNITY_EDITOR
            //to get a small performance boost while working on other stuff.
            
            #if (MULTI_BRANCH_BUILD || DEVELOPMENT_BUILD || UNITY_EDITOR) //For better performance in release builds
            LogEntry newLogEntry = new LogEntry(logString, stackTrace, type);
            
            AddLogToLogEntries(newLogEntry);
            AddLogToDistinctLogEntries(newLogEntry);
            AddLogToErrorLogEntries(newLogEntry);
            #endif
            
            //#endif
        }

        private void AddLogToLogEntries(LogEntry logEntry)
        {
            _logEntries.Add(logEntry);

            if (_logEntries.Count > MAXIMUM_NUMBER_OF_RECORDED_LOGS)
            {
                _logEntries.RemoveAt(0);
            }
        }
        
        private void AddLogToErrorLogEntries(LogEntry logEntry)
        {
            if(logEntry.Type != LogType.Error) { return; }
            
            _errorLogEntries.Add(logEntry);
            
            if (_errorLogEntries.Count > MAXIMUM_NUMBER_OF_RECORDED_ERROR_LOGS)
            {
                _errorLogEntries.RemoveAt(0);
            }
        }

        private void AddLogToDistinctLogEntries(LogEntry logEntry)
        {
            _logEntriesWithoutDuplicates.Add(logEntry);
            _logEntriesWithoutDuplicates = _logEntriesWithoutDuplicates.ToArray().Distinct(_logEntryComparer).ToList();
            //ToArray() is used to solve "Collection was modified; enumeration operation may not execute" error.
            
            if (_logEntriesWithoutDuplicates.Count > MAXIMUM_NUMBER_OF_RECORDED_DISTINCT_LOGS)
            {
                _logEntriesWithoutDuplicates.RemoveAt(0);
            }
        }

        private void HandleCopyAllLogsButtonPressed()
        {
            CopyLogEntryListToClipboard(_logEntries, false);
        }
        
        private void HandleCopyAllErrorLogsButtonPressed()
        {
            CopyLogEntryListToClipboard(_errorLogEntries);
        }
        
        private void HandleCopyAllDistinctLogsButtonPressed()
        {
            CopyLogEntryListToClipboard(_logEntriesWithoutDuplicates);
        }        
        
        private void HandleSendAllLogsButtonPressed()
        {
            string logListString = CreateStringFromLogEntries(_logEntries, false);
            NativeShareServiceInstance.ShareText("AllLogs", logListString);
        }
        
        private void HandleSendAllErrorLogsButtonPressed()
        {
            string logListString = CreateStringFromLogEntries(_errorLogEntries, true);
            NativeShareServiceInstance.ShareText("AllErrorLogs", logListString);
        }
        
        private void HandleSendAllDistinctLogsButtonPressed()
        {
            string logListString = CreateStringFromLogEntries(_logEntriesWithoutDuplicates, true);
            NativeShareServiceInstance.ShareText("AllDistinctLogs", logListString);
        }
        
        private void CopyLogEntryListToClipboard(List<LogEntry> logEntries, bool willIncludeStackTrace = true)
        {
            string logListString = CreateStringFromLogEntries(logEntries, willIncludeStackTrace);

            CopyStringToClipboard(logListString);
        }

        private string CreateStringFromLogEntries(List<LogEntry> logEntries, bool willIncludeStackTrace)
        {
            StringBuilder stringBuilder = new StringBuilder(INITIAL_CHARACTER_CAPACITY);
            
            foreach (LogEntry logEntry in logEntries)
            {
                if (stringBuilder.Length > CHARACTER_CAPACITY_THRESHOLD) { break; }
                
                stringBuilder.Append("\n" + logEntry.LogString + "\n");
                
                if(!willIncludeStackTrace) { continue; }
                stringBuilder.Append(logEntry.StackTrace);
            }

            return stringBuilder.ToString();
        }

        private void CopyStringToClipboard(string textToCopy)
        {
            if (textToCopy.Length == 0)
            {
                GUIUtility.systemCopyBuffer = "";
                return;
            }

            TextEditor textEditor = new TextEditor {text = textToCopy};
            textEditor.SelectAll();
            textEditor.Copy();
            
            //GUIUtility.systemCopyBuffer = _stringBuilder.ToString(); //Sometimes does not work in mobiles because of authentication.
        }
        
    }
}