/* 
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UISliderConfig"
 * that holds configuration data for Input Fields. 
 * 
 * The method UpdateSliderConfigInjectors() finds all instances of 
 * "ThemeInjectorSlider" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "SldConfig_", menuName = "Scriptable Theme System/Slider Config", order = 4)]
    public class UISliderConfig : ScriptableObject
    {
        public Sprite spriteHandle;
        public Color normalColor = Color.white;
        public Color highlightedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        public Sprite spriteBackground;
        public Color BackgroundColor = Color.white;

        public Sprite spriteFillRect;
        public Color FillRectColor = Color.white;

        public void UpdateSliderConfigInjectors()
        {
            ThemeInjectorSlider[] themeInjectorSliders = GameObject.FindObjectsOfType<ThemeInjectorSlider>();

            foreach (ThemeInjectorSlider themeInjectorSlider in themeInjectorSliders)
            {
                themeInjectorSlider.UpdateConfig();
            }
        }
    }
}
