using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

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
