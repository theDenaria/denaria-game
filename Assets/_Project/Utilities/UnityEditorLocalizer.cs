#if UNITY_EDITOR

using System.Globalization;
using System.Threading;
using UnityEditor;

namespace _Project.Utilities
{
    public class UnityEditorLocalizer
    {
        [InitializeOnLoadMethod]
        private static void InitializeLocalization()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            UnityEngine.Debug.LogWarningFormat("Current culture info: {0}", Thread.CurrentThread.CurrentCulture);
        }
    }
}

#endif