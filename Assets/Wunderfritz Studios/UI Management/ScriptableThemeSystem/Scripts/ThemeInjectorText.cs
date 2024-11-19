/*
 * Version: 1.0.1 
 * 
 * Description: 
 * This script injects the theme settings for Text and TMPro UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorText : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UITextConfig textConfig;
        [HideInInspector]
        public string selectedTextConfig;
        Text text;
        TextMeshProUGUI tmpText;

        void Start()
        {
            tmpText = GetComponentInChildren<TextMeshProUGUI>();
            text = GetComponent<Text>();
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

        public UITextConfig CurrentTextConfig(string configName)
        {
            var text = theme.uiTexts.Find(x => x.name == configName);
            if (text != null)
            {
                return text.uiTitleConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentTextConfig(selectedTextConfig) != null)
            {
                textConfig = CurrentTextConfig(selectedTextConfig);

                if (text != null)
                {
                    if (textConfig.font != null)
                        text.font = textConfig.font;
                    text.color = textConfig.textColor;
                }

                if (tmpText != null)
                {
                    if (textConfig.tmpFontMaterial != null)
                        tmpText.fontSharedMaterial = textConfig.tmpFontMaterial;
                    tmpText.color = textConfig.textColor;
                }
            }
        }
    }
}