/*
 * Version: 1.0.1
 * 
 * Description: 
 * This script defines a ScriptableObject class called "UIImageConfig"
 * that holds configuration data for Images. 
 * 
 * The method UpdateImageConfigInjectors() finds all instances of 
 * "ThemeInjectorImage" components in the scene and updates their 
 * configuration during runtime.
 * 
 * (c) Copyright by Wunderfritz Studios.
 */

using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CreateAssetMenu(fileName = "ImgConfig_", menuName = "Scriptable Theme System/Image Config", order = 1)]
    public class UIImageConfig : ScriptableObject
    {
        public Sprite spriteImage;
        public Color color = Color.white;

        public void UpdateImageConfigInjectors()
        {
            ThemeInjectorImage[] themeInjectorIimages = GameObject.FindObjectsOfType<ThemeInjectorImage>();
            
            foreach (ThemeInjectorImage themeInjectorImage in themeInjectorIimages)
            {
                themeInjectorImage.UpdateConfig();
            }
        }
    }
}
