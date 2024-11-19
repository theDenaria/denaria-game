/* 
 * Version: 1.0.1
 * 
 * Description: 
 * This script injects the theme settings for Image UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEngine;
using UnityEngine.UI;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorImage : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UIImageConfig imageConfig;
        [HideInInspector]
        public string selectedImageConfig;
        Image image;

        void Start()
        {
            image = GetComponent<Image>();
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

        public UIImageConfig CurrentImageConfig(string configName)
        {
            var image = theme.uiImages.Find(x => x.name == configName);
            if (image != null)
            {
                return image.uiImageConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentImageConfig(selectedImageConfig) != null)
            {
                imageConfig = CurrentImageConfig(selectedImageConfig);

                if (image != null)
                {
                    if (imageConfig.spriteImage != null)
                        image.sprite = imageConfig.spriteImage;
                    else
                        image.sprite = null;

                    image.color = imageConfig.color;
                }
            }
        }
    }
}