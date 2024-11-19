/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_Image objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uiImageNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiImageNames
 * list converted to an array.
 * 
 * Finally, the selectedImageConfig property of the ThemeInjectorImage object is
 * set to the name of the UI_Image object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorImage))]
    public class ThemeInjectorImageEditor : Editor
    {
        private List<string> uiImageNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorImage imageInjector = (ThemeInjectorImage)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (imageInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiImages from the referenced UITheme object
            uiImageNames = new List<string>();
            if (imageInjector.theme.uiImages.Count > 0)
            {
                foreach (UI_Image uiImage in imageInjector.theme.uiImages)
                {
                    if (!string.IsNullOrEmpty(uiImage.name) && uiImage.uiImageConfig != null)
                        uiImageNames.Add(uiImage.name);
                }
            }

            if (uiImageNames.Count > 0)
            {
                if (uiImageNames.Contains(imageInjector.selectedImageConfig))
                {
                    // Show the dropdown list to select a uiImage
                    int selectedIndex = uiImageNames.IndexOf(imageInjector.selectedImageConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Image", selectedIndex, uiImageNames.ToArray());
                    imageInjector.selectedImageConfig = uiImageNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected UI Image", 0, uiImageNames.ToArray());
                    imageInjector.selectedImageConfig = uiImageNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Images found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}