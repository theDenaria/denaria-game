/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UIInputFieldConfig"
 * that holds configuration data for Input Fields. 
 * 
 * The method UpdateInputFieldConfigInjectors() finds all instances of 
 * "ThemeInjectorInputField" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "IfConfig_", menuName = "Scriptable Theme System/Input Field Config", order = 7)]
    public class UIInputFieldConfig : ScriptableObject
    {
        public Sprite backgroundSprite;
        //public Color backgroundColor = Color.white;
        public Color normalColor = Color.white;
        public Color highlightedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color pressedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1f);
        public Color selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        public Color disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        public Material inputFontMaterial;
        public Color inputTextColor = Color.black;

        public Material placeholderFontMaterial;
        public Color placeholderColor = new Color(0.5843138f, 0.5843138f, 0.5843138f, 0.5f);

        public bool customCaretColor = false;
        public Color caretColor = Color.black;
        public Color selectionColor = new Color(0.6588235f, 0.8078432f, 1f, 0.7529412f);

        public void UpdateInputFieldConfigInjectors()
        {
            ThemeInjectorInputField[] themeInjectorInputFields = GameObject.FindObjectsOfType<ThemeInjectorInputField>();

            foreach (ThemeInjectorInputField themeInjectorInputfield in themeInjectorInputFields)
            {
                themeInjectorInputfield.UpdateConfig();
            }
        }
    }
}