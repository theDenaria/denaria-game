/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script injects the theme settings for Dropdown UI elements.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [ExecuteInEditMode]
    public class ThemeInjectorDropdown : MonoBehaviour, IThemeInjector
    {
        public UITheme theme;
        UIDropdownConfig dropdownConfig;
        [HideInInspector]
        public string selectedDropdownConfig;

        public TMP_Dropdown dropdown;
        public Image backgroundImage;
        public Image arrowImage;
        public TextMeshProUGUI tmpLabelText;
        public Image viewportImage;
        public Toggle item;
        public Image itemBackgroundImage;
        public Image itemCheckmarkImage;
        public TextMeshProUGUI tmpItemLabelText;
        public Scrollbar scrollbar;
        public Image scrollbarBackgroundImage;
        public Image scrollbarHandleImage;

        void Start()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            backgroundImage = dropdown.targetGraphic.GetComponent<Image>();
            arrowImage = dropdown.transform.Find("Arrow").GetComponent<Image>();
            tmpLabelText = dropdown.captionText.GetComponent<TextMeshProUGUI>();
            viewportImage = dropdown.template.transform.GetComponent<Image>();

            item = dropdown.template.transform.Find("Viewport/Content/Item").GetComponent<Toggle>();
            itemBackgroundImage = item.targetGraphic.GetComponent<Image>();
            itemCheckmarkImage = item.graphic.GetComponent<Image>();
            tmpItemLabelText = dropdown.template.transform.Find("Viewport/Content/Item/Item Label").GetComponent<TextMeshProUGUI>();

            scrollbar = dropdown.template.transform.Find("Scrollbar").GetComponent<Scrollbar>();
            scrollbarBackgroundImage = scrollbar.transform.GetComponent<Image>();
            scrollbarHandleImage = scrollbar.targetGraphic.transform.GetComponent<Image>();

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

        public UIDropdownConfig CurrentDropdownConfig(string configName)
        {
            var dropdown = theme.uiDropdowns.Find(x => x.name == configName);
            if (dropdown != null)
            {
                return dropdown.uiDropdownConfig;
            }
            else
            {
                return null;
            }
        }

        public void UpdateConfig()
        {
            if (CurrentDropdownConfig(selectedDropdownConfig) != null)
            {
                dropdownConfig = CurrentDropdownConfig(selectedDropdownConfig);

                // Set background sprite
                if (backgroundImage != null)
                {
                    if (dropdownConfig.spriteBackground != null)
                        backgroundImage.sprite = dropdownConfig.spriteBackground;
                    else
                        backgroundImage.sprite = null;
                }

                // Set colors for background
                if (backgroundImage != null)
                {
                    var colorBlock = dropdown.colors;
                    colorBlock.normalColor = dropdownConfig.normalColor;
                    colorBlock.highlightedColor = dropdownConfig.highlightedColor;
                    colorBlock.pressedColor = dropdownConfig.pressedColor;
                    colorBlock.selectedColor = dropdownConfig.selectedColor;
                    colorBlock.disabledColor = dropdownConfig.disabledColor;
                    dropdown.colors = colorBlock;
                }

                // Set arrow sprite and color
                if (arrowImage != null)
                {
                    if (dropdownConfig.spriteArrow != null)
                        arrowImage.sprite = dropdownConfig.spriteArrow;
                    else
                        arrowImage.sprite = null;
                    arrowImage.color = dropdownConfig.arrowColor;
                }

                // Set label font material and color
                if (tmpLabelText != null)
                {
                    if (dropdownConfig.tmpLabelFontMaterial != null)
                        tmpLabelText.fontSharedMaterial = dropdownConfig.tmpLabelFontMaterial;
                    tmpLabelText.color = dropdownConfig.textLabelColor;
                }

                // Set viewport background sprite and color
                if (viewportImage != null)
                {
                    if (dropdownConfig.spriteViewportBackground != null)
                        viewportImage.sprite = dropdownConfig.spriteViewportBackground;
                    else
                        viewportImage.sprite = null;
                    viewportImage.color = dropdownConfig.viewportBackgroundColor;
                }

                if (item != null)
                {
                    // Set item background sprite
                    if (itemBackgroundImage != null)
                    {
                        if (dropdownConfig.spriteItemBackground != null)
                            itemBackgroundImage.sprite = dropdownConfig.spriteItemBackground;
                        else
                            itemBackgroundImage.sprite = null;
                    }

                    // Set colors for background
                    var colorBlock = item.colors;
                    colorBlock.normalColor = dropdownConfig.normalColorItem;
                    colorBlock.highlightedColor = dropdownConfig.highlightedColorItem;
                    colorBlock.pressedColor = dropdownConfig.pressedColorItem;
                    colorBlock.selectedColor = dropdownConfig.selectedColorItem;
                    colorBlock.disabledColor = dropdownConfig.disabledColorItem;
                    item.colors = colorBlock;

                    // Set viewport checkmark sprite and color
                    if (itemCheckmarkImage != null)
                    {
                        if (dropdownConfig.spriteItemCheckmark != null)
                            itemCheckmarkImage.sprite = dropdownConfig.spriteItemCheckmark;
                        else
                            itemCheckmarkImage.sprite = null;
                        itemCheckmarkImage.color = dropdownConfig.itemCheckmarkColor;
                    }

                    // Set label font material and color
                    if (tmpItemLabelText != null)
                    {
                        if (dropdownConfig.tmpItemLabelFontMaterial != null)
                            tmpItemLabelText.fontSharedMaterial = dropdownConfig.tmpItemLabelFontMaterial;
                        tmpItemLabelText.color = dropdownConfig.textItemLabelColor;
                    }
                }

                if (scrollbar != null)
                {
                    // Set scrollbar background sprite and color
                    if (scrollbarBackgroundImage != null)
                    {
                        if (dropdownConfig.spriteScrollbarBackround != null)
                            scrollbarBackgroundImage.sprite = dropdownConfig.spriteScrollbarBackround;
                        else
                            scrollbarBackgroundImage.sprite = null;
                        scrollbarBackgroundImage.color = dropdownConfig.scrollBackgroundColor;
                    }

                    // Set scrollbar handle sprite
                    if (scrollbarHandleImage != null)
                    {
                        if (dropdownConfig.spriteScrollbarHandle != null)
                            scrollbarHandleImage.sprite = dropdownConfig.spriteScrollbarHandle;
                        else
                            scrollbarHandleImage.sprite = null;
                    }

                    // Set colors for scrollbar handle
                    var colorBlock = scrollbar.colors;
                    colorBlock.normalColor = dropdownConfig.normalColorHandle;
                    colorBlock.highlightedColor = dropdownConfig.highlightedColorHandle;
                    colorBlock.pressedColor = dropdownConfig.pressedColorHandle;
                    colorBlock.selectedColor = dropdownConfig.selectedColorHandle;
                    colorBlock.disabledColor = dropdownConfig.disabledColorHandle;
                    scrollbar.colors = colorBlock;
                }
            }
        }
    }
}
