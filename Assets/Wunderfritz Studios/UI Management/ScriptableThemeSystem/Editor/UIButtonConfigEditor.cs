/*
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Button Injectors" 
 * to manually update the ThemeInjectorButton components.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UIButtonConfig))]
    public class UIButtonConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UIButtonConfig buttonConfig = target as UIButtonConfig;

            if (GUILayout.Button("Update Button Injectors"))
            {
                buttonConfig.UpdateButtonConfigInjectors();
                EditorUtility.SetDirty(target);
            }
        }
    }
}