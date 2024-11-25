using System;
using UnityEditor;
using UnityEngine;
using WunderfritzTools.UiManagement.ScriptableThemeSystem;

namespace _Project.Theme.Scripts.Utility
{
    public class ThemeChangeTester : MonoBehaviour
    {
        [Header("Theme Selector")]
        [Tooltip("Enter the name of the theme to select.")]
        public string themeName;

        public ScriptableThemesManager ScriptableThemesManager { get; set; }
        private void Start()
        { 
            ScriptableThemesManager = GetComponent<ScriptableThemesManager>();
        }

        /// <summary>
        /// Selects a theme based on the provided name.
        /// </summary>
        /// <param name="themeName">The name of the theme to select.</param>
        public void SelectTheme(string themeName)
        {
            Debug.Log($"Theme '{themeName}' selected!");
            ScriptableThemesManager.SelectTheme(this.themeName);
        }

        /// <summary>
        /// Selects the theme based on the inspector's input.
        /// </summary>
        public void SelectTheme()
        {
            SelectTheme(themeName);
        }
    }
}