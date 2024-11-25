/*
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Theme" to the inspector that calls the 
 * UpdateTheme() method on the UITheme to manually update the theme.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UITheme))]
    public class UIThemeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UITheme iuTheme = target as UITheme;
            
            if (GUILayout.Button("Initialize Theme From Template"))
            {
                iuTheme.InitializeThemeFromTemplate();
                EditorUtility.SetDirty(target);
            }

            GUILayout.Space(10);
            
            if (GUILayout.Button("Update Theme From Template"))
            {
                iuTheme.UpdateThemeFromTemplate();
                EditorUtility.SetDirty(target);
            }
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Update Theme In Scenes"))
            {
                iuTheme.UpdateTheme();
                EditorUtility.SetDirty(target);
            }
        }
    }
}