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
    [CustomEditor(typeof(UITextConfig))]
    public class UITextConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UITextConfig textConfig = target as UITextConfig;

            if (GUILayout.Button("Update Text Injectors"))
            {
                textConfig.UpdateTextConfigInjectors();
                EditorUtility.SetDirty(target);
            }
        }
    }
}