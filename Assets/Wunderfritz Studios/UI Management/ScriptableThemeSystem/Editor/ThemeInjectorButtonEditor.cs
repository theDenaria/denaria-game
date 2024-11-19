/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_Button objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uiButtonNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiButtonNames
 * list converted to an array.
 * 
 * Finally, the selectedButtonConfig property of the ThemeInjectorButton object is
 * set to the name of the UI_Button object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorButton))]
    public class ThemeInjectorButtonEditor : Editor
    {
        private List<string> uiButtonNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorButton buttonInjector = (ThemeInjectorButton)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (buttonInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiButtons from the referenced UITheme object
            uiButtonNames = new List<string>();
            if (buttonInjector.theme.uiButtons.Count > 0)
            {
                foreach (UI_Button uiButton in buttonInjector.theme.uiButtons)
                {
                    uiButtonNames.Add(uiButton.name);
                }
            }

            if (uiButtonNames.Count > 0)
            {
                if (uiButtonNames.Contains(buttonInjector.selectedButtonConfig))
                {
                    // Show the dropdown list to select a uiButton
                    int selectedIndex = uiButtonNames.IndexOf(buttonInjector.selectedButtonConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Button", selectedIndex, uiButtonNames.ToArray());
                    buttonInjector.selectedButtonConfig = uiButtonNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected UI Button", 0, uiButtonNames.ToArray());
                    buttonInjector.selectedButtonConfig = uiButtonNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Buttons found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}