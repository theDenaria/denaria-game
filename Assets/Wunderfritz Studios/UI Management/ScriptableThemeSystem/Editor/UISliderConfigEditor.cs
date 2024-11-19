/* 
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Slider Injectors" 
 * to manually update the ThemeSliderField components.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UISliderConfig))]
    public class UISliderConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UISliderConfig sliderConfig = target as UISliderConfig;

            if (GUILayout.Button("Update Slider Injectors"))
            {
                sliderConfig.UpdateSliderConfigInjectors();
                EditorUtility.SetDirty(target);
            }
        }
    }
}
