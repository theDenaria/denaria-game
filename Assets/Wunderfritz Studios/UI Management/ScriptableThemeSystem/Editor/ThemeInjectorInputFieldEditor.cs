/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_InputField objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uiInputFieldNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiInputFieldNames
 * list converted to an array.
 * 
 * Finally, the selectedInputFieldConfig property of the ThemeInjectorInputField object is
 * set to the name of the UI_InputField object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorInputField))]
    public class ThemeInjectorInputFieldEditor : Editor
    {
        private List<string> uiInputFieldNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorInputField inputFieldInjector = (ThemeInjectorInputField)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (inputFieldInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiInputFields from the referenced UITheme object
            uiInputFieldNames = new List<string>();
            if (inputFieldInjector.theme.uiInputFields.Count > 0)
            {
                foreach (UI_InputField uiInputField in inputFieldInjector.theme.uiInputFields)
                {
                    uiInputFieldNames.Add(uiInputField.name);
                }
            }

            if (uiInputFieldNames.Count > 0)
            {
                if (uiInputFieldNames.Contains(inputFieldInjector.selectedInputFieldConfig))
                {
                    // Show the dropdown list to select a uiInputField
                    int selectedIndex = uiInputFieldNames.IndexOf(inputFieldInjector.selectedInputFieldConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Input Field", selectedIndex, uiInputFieldNames.ToArray());
                    inputFieldInjector.selectedInputFieldConfig = uiInputFieldNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected Input Field", 0, uiInputFieldNames.ToArray());
                    inputFieldInjector.selectedInputFieldConfig = uiInputFieldNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Input Fields found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}