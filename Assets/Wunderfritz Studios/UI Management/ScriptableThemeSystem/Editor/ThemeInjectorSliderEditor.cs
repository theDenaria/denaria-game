/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script displays a dropdown list using the EditorGUILayout.Popup method 
 * to allow the user to select one of the UI_Slider objects. 
 * 
 * The index of the currently selected item in the dropdown list is determined 
 * using the IndexOf method of the uisliderNames list. 
 * 
 * The selected index is then passed to the Popup method along with the uiSliderNames
 * list converted to an array.
 * 
 * Finally, the selectedSliderConfig property of the ThemeInjectorSlider object is
 * set to the name of the UI_Slider object corresponding to the selected index in 
 * the dropdown list.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(ThemeInjectorSlider))]
    public class ThemeInjectorSliderEditor : Editor
    {
        private List<string> uiSliderNames;

        public override void OnInspectorGUI()
        {
            ThemeInjectorSlider sliderInjector = (ThemeInjectorSlider)target;

            // Show the default inspector GUI
            DrawDefaultInspector();

            // If the UITheme reference is not set, show a message and return
            if (sliderInjector.theme == null)
            {
                EditorGUILayout.HelpBox("Please assign a UITheme object.", MessageType.Info);
                return;
            }

            // Get the names of uiSliders from the referenced UITheme object
            uiSliderNames = new List<string>();
            if (sliderInjector.theme.uiSliders.Count > 0)
            {
                foreach (UI_Slider uiSlider in sliderInjector.theme.uiSliders)
                {
                    uiSliderNames.Add(uiSlider.name);
                }
            }

            if (uiSliderNames.Count > 0)
            {
                if (uiSliderNames.Contains(sliderInjector.selectedSliderConfig))
                {
                    // Show the dropdown list to select a uiSlider
                    int selectedIndex = uiSliderNames.IndexOf(sliderInjector.selectedSliderConfig);
                    selectedIndex = EditorGUILayout.Popup("Selected UI Slider", selectedIndex, uiSliderNames.ToArray());
                    sliderInjector.selectedSliderConfig = uiSliderNames[selectedIndex];
                }
                else
                {
                    EditorGUILayout.Popup("Selected UI Slider", 0, uiSliderNames.ToArray());
                    sliderInjector.selectedSliderConfig = uiSliderNames[0];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No UI Sliders found in the assigned UITheme object.", MessageType.Warning);
            }
        }
    }
}
