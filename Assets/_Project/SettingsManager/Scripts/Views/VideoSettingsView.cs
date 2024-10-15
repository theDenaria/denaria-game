using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;

namespace _Project.SettingsManager.Scripts.Views
{
    public class VideoSettingsView : ViewZeitnot
    {
        internal Signal<float> onResolutionChanged = new Signal<float>();
        internal Signal<bool> onFullscreenToggled = new Signal<bool>();
        internal Signal<float> onQualitySliderChanged = new Signal<float>();

        public void ResolutionDropdownChanged(float value)
        {
            onResolutionChanged.Dispatch(value);
        }

        public void FullscreenToggled(bool isFullscreen)
        {
            onFullscreenToggled.Dispatch(isFullscreen);
        }

        public void QualitySliderChanged(float value)
        {
            onQualitySliderChanged.Dispatch(value);
        }
    }
}
