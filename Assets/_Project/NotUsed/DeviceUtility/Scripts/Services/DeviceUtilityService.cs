using UnityEngine;

namespace _Project.DeviceUtility.Scripts.Services
{
    public class DeviceUtilityService : IDeviceUtilityService
    {
        public void QuitApplication()
        {
#if UNITY_IOS
            return;
#elif !UNITY_EDITOR
            QuitRuntimeApplication();
#else
            QuitEditorApplication();
#endif
        }


        /// <summary>
        ///     That will be called on all player builds, except iOS builds.iOS has no quit functionality, calling quit may cause crash reports.
        /// </summary>
        private void QuitRuntimeApplication()
        {
            Application.Quit();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     If we are in Unity editor, that will stop the editor playing.
        /// </summary>
        private void QuitEditorApplication()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif

    }
}