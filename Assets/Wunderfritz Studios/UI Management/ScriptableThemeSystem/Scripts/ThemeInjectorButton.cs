/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script injects the theme settings for Button UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorButton : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UIButtonConfig buttonConfig;
        [HideInInspector]
        public string selectedButtonConfig;
        Button button;
        Image image;
        Text text;
        TextMeshProUGUI tmpText;

        void Start()
        {
            button = GetComponent<Button>();
            text = GetComponentInChildren<Text>();
            tmpText = GetComponentInChildren<TextMeshProUGUI>();
            image = GetComponentInChildren<Image>();
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

        public UIButtonConfig CurrentButtonConfig(string configName)
        {
            var button = theme.uiButtons.Find(x => x.name == configName);
            if (button != null)
            {
                return button.uiButtonConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentButtonConfig(selectedButtonConfig) != null)
            {
                if (button != null)
                {
                    buttonConfig = CurrentButtonConfig(selectedButtonConfig);
                    var colorBlock = button.colors;

                    colorBlock.normalColor = buttonConfig.normalColor;
                    colorBlock.highlightedColor = buttonConfig.highlightedColor;
                    colorBlock.pressedColor = buttonConfig.pressedColor;
                    colorBlock.selectedColor = buttonConfig.selectedColor;
                    colorBlock.disabledColor = buttonConfig.disabledColor;
                    button.colors = colorBlock;
                }

                if (image != null)
                {
                    if (buttonConfig.spriteImage != null)
                        image.sprite = buttonConfig.spriteImage;
                    else
                        image.sprite = null;
                }

                if (text != null)
                {
                    if (buttonConfig.font != null)
                        text.font = buttonConfig.font;
                    text.color = buttonConfig.textColor;
                }

                if (tmpText != null)
                {
                    if (buttonConfig.tmpFontMaterial != null)
                        tmpText.fontSharedMaterial = buttonConfig.tmpFontMaterial;
                    tmpText.color = buttonConfig.textColor;
                }
            }
        }
    }
}
