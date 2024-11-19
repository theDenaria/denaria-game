/*
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Image Injectors" 
 * to manually update the ThemeInjectorImage components.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UIImageConfig))]
    public class UIImageConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UIImageConfig imageConfig = target as UIImageConfig;

            if (GUILayout.Button("Update Image Injectors"))
            {
                imageConfig.UpdateImageConfigInjectors();
                EditorUtility.SetDirty(target);
            }
        }
    }
}