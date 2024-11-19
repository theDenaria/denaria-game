/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script injects the theme settings for Slider UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEngine.UI;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorSlider : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UISliderConfig sliderConfig;
        [HideInInspector]
        public string selectedSliderConfig;
        Slider slider;
        Image handleImage;
        Image backgroundImage;
        Image fillRectImage;

        void Start()
        {
            slider = GetComponent<Slider>();
            handleImage = slider.handleRect.GetComponent<Image>();
            backgroundImage = slider.transform.Find("Background").GetComponent<Image>();
            fillRectImage = slider.fillRect.GetComponent<Image>();

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

        public UISliderConfig CurrentSliderConfig(string configName)
        {
            var slider = theme.uiSliders.Find(x => x.name == configName);
            if (slider != null)
            {
                return slider.uiSliderConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentSliderConfig(selectedSliderConfig) != null)
            {

                sliderConfig = CurrentSliderConfig(selectedSliderConfig);

                // Set handle sprite
                if (handleImage != null)
                {
                    if (sliderConfig.spriteHandle != null)
                        handleImage.sprite = sliderConfig.spriteHandle;
                    else
                        handleImage.sprite = null;
                }

                // Set colors for handle
                if (handleImage != null)
                {
                    var colorBlock = slider.colors;
                    colorBlock.normalColor = sliderConfig.normalColor;
                    colorBlock.highlightedColor = sliderConfig.highlightedColor;
                    colorBlock.pressedColor = sliderConfig.pressedColor;
                    colorBlock.selectedColor = sliderConfig.selectedColor;
                    colorBlock.disabledColor = sliderConfig.disabledColor;
                    slider.colors = colorBlock;
                }

                // Set background sprite and color
                if (backgroundImage != null)
                {
                    if (sliderConfig.spriteBackground != null)
                        backgroundImage.sprite = sliderConfig.spriteBackground;
                    else
                        backgroundImage.sprite = null;
                    backgroundImage.color = sliderConfig.BackgroundColor;
                }

                // Set fill rect sprite and color
                if (fillRectImage != null)
                {
                    if (sliderConfig.spriteFillRect != null)
                        fillRectImage.sprite = sliderConfig.spriteFillRect;
                    else
                        fillRectImage.sprite = null;

                    fillRectImage.color = sliderConfig.FillRectColor;
                }
            }
        }
    }
}