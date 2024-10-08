using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace _Project.SettingsManager.Scripts.Views
{
    public class AudioSettingsView : ViewZeitnot
    {
        internal Signal<float> onMasterVolumeSliderChanged = new Signal<float>();
        internal Signal<float> onGameSoundSliderChanged = new Signal<float>();
        internal Signal<float> onMenuMusicSliderChanged = new Signal<float>();
        internal Signal<string> onSoundDeviceDropdownChanged = new Signal<string>();

        public void MasterVolumeSliderChanged(float value)
        {
            onMasterVolumeSliderChanged.Dispatch(value);
        }

        public void GameSoundSliderChanged(float value)
        {
            onGameSoundSliderChanged.Dispatch(value);
        }

        public void MenuMusicSliderChanged(float value)
        {
            onMenuMusicSliderChanged.Dispatch(value);
        }

        public void SoundDeviceDropdownChanged(string value)
        {
            onSoundDeviceDropdownChanged.Dispatch(value);
        }
    }
}

