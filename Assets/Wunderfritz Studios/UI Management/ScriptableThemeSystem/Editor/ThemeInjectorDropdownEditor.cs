/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_Dropdown objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uiDropdownNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiDropdownNames
 * list converted to an array.
 * 
 * Finally, the selectedDropdownConfig property of the ThemeInjectorDropdown object is
 * set to the name of the UI_Dropdown object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorDropdown))]
    public class ThemeInjectorThemeInjectorDropdownEditor : Editor
    {
        private List<string> uiDropdownNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorDropdown dropdownInjector = (ThemeInjectorDropdown)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (dropdownInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiDropdowns from the referenced UITheme object
            uiDropdownNames = new List<string>();
            if (dropdownInjector.theme.uiDropdowns.Count > 0)
            {
                foreach (UI_Dropdown uiDropdown in dropdownInjector.theme.uiDropdowns)
                {
                    if (!string.IsNullOrEmpty(uiDropdown.name) && uiDropdown.uiDropdownConfig != null)
                        uiDropdownNames.Add(uiDropdown.name);
                }
            }

            if (uiDropdownNames.Count > 0)
            {
                if (uiDropdownNames.Contains(dropdownInjector.selectedDropdownConfig))
                {
                    // Show the dropdown list to select a uiDropdown
                    int selectedIndex = uiDropdownNames.IndexOf(dropdownInjector.selectedDropdownConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Dropdown", selectedIndex, uiDropdownNames.ToArray());
                    dropdownInjector.selectedDropdownConfig = uiDropdownNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected UI Dropdown", 0, uiDropdownNames.ToArray());
                    dropdownInjector.selectedDropdownConfig = uiDropdownNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Dropdowns found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}
