/*
 * Version: 1.0.1 
 * 
 * Description: 
 * This script manages user interface (UI) elements such as buttons, 
 * text, input fields, sliders, and images. 
 * 
 * The script defines several classes that hold configuration data for specific UI elements. 
 * These classes include UIButtonConfig, UITextConfig, UIImageConfig, UIInputFieldConfig, 
 * UIToggleConfig, UIDropdownConfig, and UISliderConfig.
 * 
 * Each class has properties for various aspects of the element's appearance and behavior,
 * such as its font, text color, sprite image, etc.
 * 
 * This ScriptableObject class holds a list of instances of the UI classes mentioned earlier..
 * 
 * InitializeThemeFromTemplate() initializes the UI theme lists using a ThemeSharedTemplate 
 * as the source list. 
 * 
 * The script also includes a method called UpdateTheme() that updates theme injectors in 
 * the scene by finding them.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "Theme_", menuName = "Scriptable Theme System/Theme", order = 0)]
    public class UITheme : ScriptableObject
    {
        public ThemeSharedTemplate themeSharedTemplate;
        public List<UI_Image> uiImages;
        public List<UI_Text> uiTexts;
        public List<UI_Button> uiButtons;
        public List<UI_InputField> uiInputFields;
        public List<UI_Slider> uiSliders;
        public List<UI_Toggle> uiToggles;
        public List<UI_Dropdown> uiDropdowns;

        public void InitializeList<T>(List<T> destinationList, List<UIElementData> sourceList) where T : UIElementBase, new()
        {
            destinationList.Clear();

            for (int i = 0; i < sourceList.Count; i++)
            {
                T element = new T();
                element.name = sourceList[i].name;
                destinationList.Add(element);
            }
        }

        public void InitializeThemeFromTemplate()
        {
            if (themeSharedTemplate == null)
            {
                Debug.LogWarning("No ThemeSharedTemplate assigned in the editor. Please assign one.");
                return;
            }

            InitializeList(uiImages, themeSharedTemplate.uiImages);
            InitializeList(uiTexts, themeSharedTemplate.uiTexts);
            InitializeList(uiButtons, themeSharedTemplate.uiButtons);
            InitializeList(uiInputFields, themeSharedTemplate.uiInputFields);
            InitializeList(uiSliders, themeSharedTemplate.uiSliders);
            InitializeList(uiToggles, themeSharedTemplate.uiToggles);
            InitializeList(uiDropdowns, themeSharedTemplate.uiDropdowns);
        }

        private void UpdateThemeInjectors<T>(T[] themeInjectors) where T : IThemeInjector
        {
            foreach (T themeInjector in themeInjectors)
            {
                themeInjector.UpdateConfig();
            }
        }

        public void UpdateTheme()
        {
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorText>());
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorImage>());
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorButton>());
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorInputField>());
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorSlider>());
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorToggle>());
            UpdateThemeInjectors(GameObject.FindObjectsOfType<ThemeInjectorDropdown>());
        }
    }

    [System.Serializable]
    public class UIElementBase
    {
        [SerializeField]
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    [System.Serializable]
    public class UI_Button : UIElementBase
    {
        public UIButtonConfig uiButtonConfig;
    }

    [System.Serializable]
    public class UI_Text : UIElementBase
    {
        public UITextConfig uiTitleConfig;
    }

    [System.Serializable]
    public class UI_Image : UIElementBase
    {
        public UIImageConfig uiImageConfig;
    }

    [System.Serializable]
    public class UI_InputField : UIElementBase
    {
        public UIInputFieldConfig uiInputFieldConfig;
    }

    [System.Serializable]
    public class UI_Slider : UIElementBase
    {
        public UISliderConfig uiSliderConfig;
    }

    [System.Serializable]
    public class UI_Toggle : UIElementBase
    {
        public UIToggleConfig uiToggleConfig;
    }

    [System.Serializable]
    public class UI_Dropdown : UIElementBase
    {
        public UIDropdownConfig uiDropdownConfig;
    }

    public interface IThemeInjector
    {
        void UpdateConfig();
    }
}
