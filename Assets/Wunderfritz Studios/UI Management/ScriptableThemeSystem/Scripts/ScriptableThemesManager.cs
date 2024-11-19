/* 
 * Version: 1.0.1
 * 
 * Description: 
 * This class is responsible for managing scriptable themes and injecting them
 * into UI elements. It contains a public list of ScriptableTheme objects.
 * 
 * The SetTheme method is used to set the theme for all the UI elements in the scene.
 * 
 * The SelectTheme method is used to select a theme from the list of available 
 * scriptable themes based on the name of the theme and then set the selected theme 
 * for all the UI elements
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    public class ScriptableThemesManager : MonoBehaviour
    {
        public List<ScriptableTheme> scriptableThemes;

        public void SelectTheme(string scriptableThemeName)
        {
            ScriptableTheme selectedTheme = scriptableThemes.Find(theme => theme.name == scriptableThemeName);

            if (selectedTheme != null)
            {
                SetTheme(selectedTheme.uITheme);
            }
            else
            {
                Debug.LogError("No scriptable theme found with the name: " + scriptableThemeName);
            }
        }

        public void SetTheme(UITheme uITheme)
        {
            ThemeInjectorText[] themeInjectorTexts = GameObject.FindObjectsOfType<ThemeInjectorText>();
            ThemeInjectorImage[] themeInjectorIimages = GameObject.FindObjectsOfType<ThemeInjectorImage>();
            ThemeInjectorButton[] themeInjectorButtons = GameObject.FindObjectsOfType<ThemeInjectorButton>();
            ThemeInjectorInputField[] themeInjectorInputFields = GameObject.FindObjectsOfType<ThemeInjectorInputField>();
            ThemeInjectorSlider[] themeInjectorSliders = GameObject.FindObjectsOfType<ThemeInjectorSlider>();
            ThemeInjectorToggle[] themeInjectorToggles = GameObject.FindObjectsOfType<ThemeInjectorToggle>();
            ThemeInjectorDropdown[] themeInjectorDropdowns = GameObject.FindObjectsOfType<ThemeInjectorDropdown>();

            foreach (ThemeInjectorText themeInjectorText in themeInjectorTexts)
            {
                themeInjectorText.SetTheme(uITheme);
            }

            foreach (ThemeInjectorImage themeInjectorImage in themeInjectorIimages)
            {
                themeInjectorImage.SetTheme(uITheme);
            }

            foreach (ThemeInjectorButton themeInjectorButton in themeInjectorButtons)
            {
                themeInjectorButton.SetTheme(uITheme);
            }

            foreach (ThemeInjectorInputField themeInjectorInputField in themeInjectorInputFields)
            {
                themeInjectorInputField.SetTheme(uITheme);
            }

            foreach (ThemeInjectorSlider themeInjectorSlider in themeInjectorSliders)
            {
                themeInjectorSlider.SetTheme(uITheme);
            }

            foreach (ThemeInjectorToggle themeInjectorToggle in themeInjectorToggles)
            {
                themeInjectorToggle.SetTheme(uITheme);
            }

            foreach (ThemeInjectorDropdown themeInjectorDropdown in themeInjectorDropdowns)
            {
                themeInjectorDropdown.SetTheme(uITheme);
            }
        }
    }

    [System.Serializable]
    public class ScriptableTheme
    {
        public string name;
        public UITheme uITheme;
    }
}