/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UITextConfig"
 * that holds configuration data for Input Fields. 
 * 
 * The method UpdateTextConfigInjectors() finds all instances of 
 * "ThemeInjectorText" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "TxtConfig_", menuName = "Scriptable Theme System/Text Config", order = 2)]
    public class UITextConfig : ScriptableObject
    {
        public Font font;
        public Material tmpFontMaterial;
        public Color textColor = Color.black;

        public void UpdateTextConfigInjectors()
        {
            ThemeInjectorText[] themeInjectorTexts = GameObject.FindObjectsOfType<ThemeInjectorText>();

            foreach (ThemeInjectorText themeInjectorText in themeInjectorTexts)
            {
                themeInjectorText.UpdateConfig();
            }
        }
    }
}
