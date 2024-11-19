/*
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Dropdown Injectors" 
 * to manually update the ThemeInjectorDropdown components.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEditor;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UIDropdownConfig))]
    public class UIDropdownConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            UIDropdownConfig dropdownConfig = target as UIDropdownConfig;

            if (GUILayout.Button("Update Dropdown Injectors"))
            {
                dropdownConfig.UpdateDropdownConfigInjectors();
                EditorUtility.SetDirty(target);
            }
        }
    }
}
