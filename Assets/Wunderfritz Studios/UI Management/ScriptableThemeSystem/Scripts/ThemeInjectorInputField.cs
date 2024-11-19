/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script injects the theme settings for Input Field UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorInputField : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UIInputFieldConfig inputFieldConfig;
        [HideInInspector]
        public string selectedInputFieldConfig;
        TMP_InputField inputField;
        Image backgroundImage;
        TMP_Text placeholderText;
        TMP_Text inputText;

        void Start()
        {
            inputField = GetComponent<TMP_InputField>();
            backgroundImage = GetComponent<Image>();
            placeholderText = inputField.placeholder.GetComponent<TMP_Text>();
            inputText = inputField.textComponent;
            UpdateConfig();
        }

        private void OnEnable()
        {
            UpdateConfig();
        }

        public void SetTheme(UITheme newTheme)
        {
            theme = newTheme;
            UpdateConfig();
        }

        public UIInputFieldConfig CurrentInputFieldConfig(string configName)
        {
            var inputField = theme.uiInputFields.Find(x => x.name == configName);
            if (inputField != null)
            {
                return inputField.uiInputFieldConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentInputFieldConfig(selectedInputFieldConfig) != null)
            {
                inputFieldConfig = CurrentInputFieldConfig(selectedInputFieldConfig);

                if (backgroundImage != null)
                {
                    if (inputFieldConfig.backgroundSprite != null)
                        backgroundImage.sprite = inputFieldConfig.backgroundSprite;
                    else
                        backgroundImage.sprite = null;
                }

                if (inputField != null)
                {
                    var colorBlock = inputField.colors;
                    colorBlock.normalColor = inputFieldConfig.normalColor;
                    colorBlock.highlightedColor = inputFieldConfig.highlightedColor;
                    colorBlock.pressedColor = inputFieldConfig.pressedColor;
                    colorBlock.selectedColor = inputFieldConfig.selectedColor;
                    colorBlock.disabledColor = inputFieldConfig.disabledColor;
                    inputField.colors = colorBlock;

                    inputField.customCaretColor = inputFieldConfig.customCaretColor;
                    inputField.caretColor = inputFieldConfig.caretColor;
                    inputField.selectionColor = inputFieldConfig.selectionColor;
                }

                if (placeholderText != null)
                {
                    if (inputFieldConfig.placeholderFontMaterial != null)
                        placeholderText.fontSharedMaterial = inputFieldConfig.placeholderFontMaterial;

                    placeholderText.color = inputFieldConfig.placeholderColor;
                }

                if (inputText != null)
                {
                    if (inputFieldConfig.inputFontMaterial != null)
                        inputText.fontSharedMaterial = inputFieldConfig.inputFontMaterial;

                    inputText.color = inputFieldConfig.inputTextColor;
                }
            }
        }
    }
}
