/*
 * Version: 1.0.1 
 * 
 * Description: 
 * The purpose of ThemeSharedTemplate is to define a set of UI elements 
 * that can be shared across multiple themes. 
 * 
 * You can inject this ScriptableObject, into your scriptable themes as 
 * a source for initializing the lists of UI elements. This allows you 
 * to define UI elements once in the shared template and reuse them in 
 * different themes without having to set them manually each time.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using System.Collections.Generic;
using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "ThemeSharedTemplate", menuName = "Scriptable Theme System/Theme Shared Template", order = 0)]
    public class ThemeSharedTemplate : ScriptableObject
    {
        public List<UIElementData> uiImages;
        public List<UIElementData> uiTexts;
        public List<UIElementData> uiButtons;
        public List<UIElementData> uiInputFields;
        public List<UIElementData> uiSliders;
        public List<UIElementData> uiToggles;
        public List<UIElementData> uiDropdowns;
    }

    [System.Serializable]
    public class UIElementData
    {
        public string name;
    }
}