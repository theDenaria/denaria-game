/* 
 * Version: 1.0.1
 * 
 * Description: 
 * This script injects the theme settings for Toggle UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorToggle : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UIToggleConfig toggleConfig;
        [HideInInspector]
        public string selectedToggleConfig;
        Toggle toggle;
        Image backgroundImage;
        Image checkmarkImage;
        Text text;
        TextMeshProUGUI tmpText;

        void Start()
        {
            toggle = GetComponent<Toggle>();
            backgroundImage = toggle.targetGraphic.GetComponent<Image>();
            checkmarkImage = toggle.graphic.GetComponent<Image>();
            text = toggle.transform.Find("Label").GetComponent<Text>();
            tmpText = toggle.transform.Find("Label").GetComponent<TextMeshProUGUI>();

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

        public UIToggleConfig CurrentToggleConfig(string configName)
        {
            var toggle = theme.uiToggles.Find(x => x.name == configName);
            if (toggle != null)
            {
                return toggle.uiToggleConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentToggleConfig(selectedToggleConfig) != null)
            {

                toggleConfig = CurrentToggleConfig(selectedToggleConfig);

                // Set background sprite
                if (backgroundImage != null)
                {
                    if (toggleConfig.spriteBackground != null)
                        backgroundImage.sprite = toggleConfig.spriteBackground;
                    else
                        backgroundImage.sprite = null;
                }

                // Set colors for background
                if (backgroundImage != null)
                {
                    var colorBlock = toggle.colors;
                    colorBlock.normalColor = toggleConfig.normalColor;
                    colorBlock.highlightedColor = toggleConfig.highlightedColor;
                    colorBlock.pressedColor = toggleConfig.pressedColor;
                    colorBlock.selectedColor = toggleConfig.selectedColor;
                    colorBlock.disabledColor = toggleConfig.disabledColor;
                    toggle.colors = colorBlock;
                }

                // Set checkmark sprite and color
                if (checkmarkImage != null)
                {
                    if (toggleConfig.spriteCheckmark != null)
                        checkmarkImage.sprite = toggleConfig.spriteCheckmark;
                    else
                        checkmarkImage.sprite = null;
                    checkmarkImage.color = toggleConfig.checkmarkColor;
                }

                // Set label font material and color
                if (text != null)
                {
                    if (toggleConfig.font != null)
                        text.font = toggleConfig.font;
                    text.color = toggleConfig.textColor;
                }

                if (tmpText != null)
                {
                    if (toggleConfig.tmpFontMaterial != null)
                        tmpText.fontSharedMaterial = toggleConfig.tmpFontMaterial;
                    tmpText.color = toggleConfig.textColor;
                }
            }
        }
    }
}
