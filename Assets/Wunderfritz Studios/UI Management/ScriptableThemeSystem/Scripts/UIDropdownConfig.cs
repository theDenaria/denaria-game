/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UIDropdownConfig"
 * that holds configuration data for Dropdowns. 
 * 
 * The method UpdateDropdownConfigInjectors() finds all instances of 
 * "ThemeInjectorDropdown" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "DdConfig_", menuName = "Scriptable Theme System/Dropdown Config", order = 6)]
    public class UIDropdownConfig : ScriptableObject
    {
        [Header("Dropdown Config")]
        public Sprite spriteBackground;
        public Color normalColor = Color.white;
        public Color highlightedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        public Sprite spriteArrow;
        public Color arrowColor = Color.white;

        public Material tmpLabelFontMaterial;
        public Color textLabelColor = Color.black;

        [Header("Options Template Config")]
        [Header("Viewport")]
        public Sprite spriteViewportBackground;
        public Color viewportBackgroundColor = Color.white;

        [Header("Scrollbar")]
        public Sprite spriteScrollbarBackround;
        public Color scrollBackgroundColor = Color.white;
        public Sprite spriteScrollbarHandle;
        public Color normalColorHandle = Color.white;
        public Color highlightedColorHandle = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColorHandle = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColorHandle = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColorHandle = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        [Header("Item")]
        public Sprite spriteItemBackground;
        public Color normalColorItem = Color.white;
        public Color highlightedColorItem = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColorItem = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColorItem = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColorItem = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        public Sprite spriteItemCheckmark;
        public Color itemCheckmarkColor = Color.white;

        public Material tmpItemLabelFontMaterial;
        public Color textItemLabelColor = Color.black;

        public void UpdateDropdownConfigInjectors()
        {
            ThemeInjectorDropdown[] themeInjectorDropdowns = GameObject.FindObjectsOfType<ThemeInjectorDropdown>();

            foreach (ThemeInjectorDropdown themeInjectorDropdown in themeInjectorDropdowns)
            {
                themeInjectorDropdown.UpdateConfig();
            }
        }
    }
}
