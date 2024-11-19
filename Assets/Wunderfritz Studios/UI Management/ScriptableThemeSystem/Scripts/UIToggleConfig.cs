/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UIToggleConfig"
 * that holds configuration data for Toggles. 
 * 
 * The method UpdateToggleConfigInjectors() finds all instances of 
 * "ThemeInjectorToggle" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "TglConfig_", menuName = "Scriptable Theme System/Toggle Config", order = 3)]
    public class UIToggleConfig : ScriptableObject
    {
        public Sprite spriteBackground;
        public Color normalColor = Color.white;
        public Color highlightedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        public Sprite spriteCheckmark;
        public Color checkmarkColor = Color.white;

        public Font font;
        public Material tmpFontMaterial;
        public Color textColor = Color.black;


        public void UpdateToggleConfigInjectors()
        {
            ThemeInjectorToggle[] themeInjectorToggles = GameObject.FindObjectsOfType<ThemeInjectorToggle>();

            foreach (ThemeInjectorToggle themeInjectorToggle in themeInjectorToggles)
            {
                themeInjectorToggle.UpdateConfig();
            }
        }
    }
}
