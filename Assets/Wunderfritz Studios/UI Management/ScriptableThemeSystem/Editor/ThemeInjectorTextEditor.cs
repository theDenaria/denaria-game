/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_Text objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uiTextNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiTextNames
 * list converted to an array.
 * 
 * Finally, the selectedTextConfig property of the ThemeInjectorText object is
 * set to the name of the UI_Text object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorText))]
    public class ThemeInjectorTextEditor : Editor
    {
        private List<string> uiTextNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorText textInjector = (ThemeInjectorText)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (textInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiTexts from the referenced UITheme object
            uiTextNames = new List<string>();
            if (textInjector.theme.uiTexts.Count > 0)
            {
                foreach (UI_Text uiText in textInjector.theme.uiTexts)
                {
                    uiTextNames.Add(uiText.name);
                }
            }

            if (uiTextNames.Count > 0)
            {
                if (uiTextNames.Contains(textInjector.selectedTextConfig))
                {
                    // Show the dropdown list to select a uiText
                    int selectedIndex = uiTextNames.IndexOf(textInjector.selectedTextConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Text", selectedIndex, uiTextNames.ToArray());
                    textInjector.selectedTextConfig = uiTextNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected UI Text", 0, uiTextNames.ToArray());
                    textInjector.selectedTextConfig = uiTextNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Texts found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}