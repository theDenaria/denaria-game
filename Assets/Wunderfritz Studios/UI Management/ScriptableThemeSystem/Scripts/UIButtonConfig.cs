/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UIButtonConfig"
 * that holds configuration data for buttons. 
 * 
 * The method UpdateButtonConfigInjectors() finds all instances of 
 * "ThemeInjectorButton" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "BtnConfig_", menuName = "Scriptable Theme System/Button Config", order = 5)]
    public class UIButtonConfig : ScriptableObject
    {
        public Sprite spriteImage;
        public Color normalColor = Color.white;
        public Color highlightedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        public Font font;
        public Material tmpFontMaterial;
        public Color textColor = Color.black;

        public void UpdateButtonConfigInjectors()
        {
            ThemeInjectorButton[] themeInjectorButtons = GameObject.FindObjectsOfType<ThemeInjectorButton>();
            
            foreach (ThemeInjectorButton themeInjectorButton in themeInjectorButtons)
            {
                themeInjectorButton.UpdateConfig();
            }
        }
    }
}