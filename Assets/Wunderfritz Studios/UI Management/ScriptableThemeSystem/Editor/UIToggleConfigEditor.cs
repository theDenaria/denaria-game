/*
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Text Injectors" 
 * to manually update the ThemeInjectorText components.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UIToggleConfig))]
    public class UIToggleConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UIToggleConfig toggleConfig = target as UIToggleConfig;

            if (GUILayout.Button("Update Toggle Injectors"))
            {
                toggleConfig.UpdateToggleConfigInjectors();
                EditorUtility.SetDirty(target);
            }
        }
    }
}
