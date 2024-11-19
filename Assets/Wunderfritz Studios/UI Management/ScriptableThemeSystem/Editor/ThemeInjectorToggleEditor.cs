/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_Toggle objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uiToggleNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiToggleNames
 * list converted to an array.
 * 
 * Finally, the selectedToggleConfig property of the ThemeInjectorToggle object is
 * set to the name of the UI_Toggle object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorToggle))]
    public class ThemeInjectorToggleEditor : Editor
    {
        private List<string> uiToggleNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorToggle toggleInjector = (ThemeInjectorToggle)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (toggleInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiToggles from the referenced UITheme object
            uiToggleNames = new List<string>();
            if (toggleInjector.theme.uiToggles.Count > 0)
            {
                foreach (UI_Toggle uiToggle in toggleInjector.theme.uiToggles)
                {
                    uiToggleNames.Add(uiToggle.name);
                }
            }

            if (uiToggleNames.Count > 0)
            {
                if (uiToggleNames.Contains(toggleInjector.selectedToggleConfig))
                {
                    // Show the dropdown list to select a uiToggle
                    int selectedIndex = uiToggleNames.IndexOf(toggleInjector.selectedToggleConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Toggle", selectedIndex, uiToggleNames.ToArray());
                    toggleInjector.selectedToggleConfig = uiToggleNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected UI Toggle", 0, uiToggleNames.ToArray());
                    toggleInjector.selectedToggleConfig = uiToggleNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Toggles found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}
