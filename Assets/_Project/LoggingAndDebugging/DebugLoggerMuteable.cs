using System.Diagnostics;
using _Project.Utilities;
using Object = UnityEngine.Object;

namespace _Project.LoggingAndDebugging
{
    //Use this class instead of Debug class of Unity, so that we can simply ignore Log methods below, in release builds. 
    public class DebugLoggerMuteable
    {
        [Conditional(Constants.TEST_BRANCH_BUILD), Conditional(Constants.UNITY_EDITOR)]
        public static void Log(string logMessage, Object context = null)
        {
            if (context == null)
            {
                UnityEngine.Debug.Log(logMessage);
                return;
            }
    
            UnityEngine.Debug.Log(logMessage, context);
        }
    
        [Conditional(Constants.TEST_BRANCH_BUILD), Conditional(Constants.UNITY_EDITOR)]
        public static void LogWarning(string logMessage, Object context = null)
        {
            if (context == null)
            {
                UnityEngine.Debug.LogWarning(logMessage);
                return;
            }
    
            UnityEngine.Debug.LogWarning(logMessage, context);
        }
    
        [Conditional(Constants.TEST_BRANCH_BUILD), Conditional(Constants.UNITY_EDITOR)]
        public static void LogError(string logMessage, Object context = null)
        {
            if (context == null)
            {
                UnityEngine.Debug.LogError(logMessage);
                return;
            }
    
            UnityEngine.Debug.LogError(logMessage, context);
        }
    }
}