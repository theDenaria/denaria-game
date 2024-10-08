using _Project.SettingsManager.Scripts.Controllers;
using _Project.SettingsManager.Scripts.Enums;
using _Project.SettingsManager.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.SettingsManager.Scripts.Views
{
    public class AudioSettingsMediator : Mediator
    {
        [Inject] public AudioSettingsView View { get; set; }
        [Inject] public ChangeSettingsSignal ChangeSettingsSignal { get; set; }
        public override void OnRegister()
        {
            View.onMasterVolumeSliderChanged.AddListener(HandleMasterVolumeChange);
            View.onGameSoundSliderChanged.AddListener(HandleGameSoundChange);
            View.onMenuMusicSliderChanged.AddListener(HandleMenuMusicChange);
            View.onSoundDeviceDropdownChanged.AddListener(HandleSoundDeviceChange);
        }
        public override void OnRemove()
        {
            View.onMasterVolumeSliderChanged.RemoveListener(HandleMasterVolumeChange);
            View.onGameSoundSliderChanged.RemoveListener(HandleGameSoundChange);
            View.onMenuMusicSliderChanged.RemoveListener(HandleMenuMusicChange);
            View.onSoundDeviceDropdownChanged.RemoveListener(HandleSoundDeviceChange);
        }

        private void HandleMasterVolumeChange(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.MasterVolume, value.ToString()));
        }

        private void HandleGameSoundChange(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.GameSoundVolume, value.ToString()));
        }

        private void HandleMenuMusicChange(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.MenuMusicVolume, value.ToString()));
        }

        private void HandleSoundDeviceChange(string value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.SoundDevice, value));
        }
    }
}

