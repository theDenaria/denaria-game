using System;
using System.ComponentModel;
using _Project.Utilities;
using StompyRobot.SROptions;
using UnityEngine;

    public partial class SROptions
{
    public static event Action OnCopyAllLogsButtonPressed;
    public static event Action OnCopyAllErrorLogsButtonPressed;
    public static event Action OnCopyAllDistinctLogsButtonPressed;    
    
    public static event Action OnSendAllLogsButtonPressed;
    public static event Action OnSendAllErrorLogsButtonPressed;
    public static event Action OnSendAllDistinctLogsButtonPressed;
    
    public static event Action OnZipAllLogsButtonPressed;

    public static event Action OnSendLogZipButtonPressed;
    
    [Category("Logs")]
    public void CopyAllLogs()
    {
        OnCopyAllLogsButtonPressed();
    }

    [Category("Logs")]
    public void CopyAllErrorLogs()
    {
        OnCopyAllErrorLogsButtonPressed();
    }

    [Category("Logs")]
    public void CopyAllDistinctLogs()
    {
        OnCopyAllDistinctLogsButtonPressed();
    }
    
    [Category("Logs")]
    public void SendAllLogs()
    {
        OnSendAllLogsButtonPressed();
    }
    
    [Category("Logs")]
    public void SendAllDistinctLogs()
    {
        OnSendAllDistinctLogsButtonPressed();
    }
    
    [Category("Logs")]
    public void SendAllErrorLogs()
    {
        OnSendAllErrorLogsButtonPressed();
    }
    
    [Category("Logs")]
    public void ZipAllLogs()
    {
        OnZipAllLogsButtonPressed();
    }   
    
    [Category("Logs")]
    public void SendLogZip()
    {
        OnSendLogZipButtonPressed();
    }

    [Category("Utilities")]
    public void ClearPlayerPrefs()
    {
        Debug.Log("Clearing PlayerPrefs");
        PlayerPrefs.DeleteAll();
    }
    
    [Category("Utilities")]
    public void ClearPlayerPrefsAndQuitGame()
    {
        Debug.Log("Clearing PlayerPrefs and quitting");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

   [Category("Utilities")]
   public void ClearDatabaseAndPlayerPrefsAndQuitGame()
   {
        SRDebuggerBusSystem.CallOnClearDatabaseAndPlayerPrefsAndQuitGame();
   }

    [Category("Utilities")]
    public void AddGemsToAccount()
    {
        SRDebuggerBusSystem.CallOnAddGemsToAccount();
    }

    [Category("Utilities")]
    public void SubstractGemsFromAccount()
    {
        SRDebuggerBusSystem.CallOnSubstractGemsFromAccount();
    }
    
    [Category("Utilities")]
    public void SubstractAllGemsFromAccount()
    {
        SRDebuggerBusSystem.CallOnSubstractAllGemsFromAccount();
    }

   [Category("Utilities")]
    public void CopyDeviceUniqueID()
    {
        GUIUtility.systemCopyBuffer = PlayerPrefs.GetString(Constants.DEVICE_ID_KEY,SystemInfo.deviceUniqueIdentifier);
    }

#if UNITY_EDITOR
    [Category("Live Ops")]
    public void AddTestDayOffsetToCBS()
    {
        SRDebuggerBusSystem.CallAddTestDayOffsetToCBS();
    }
#endif

    [Category("Addressables")]
    public void ClearAssetBundleCache()
    {
        Caching.ClearCache();
    }

}