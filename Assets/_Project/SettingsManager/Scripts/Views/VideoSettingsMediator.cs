using _Project.SettingsManager.Scripts.Controllers;
using strange.extensions.mediation.impl;
using _Project.SettingsManager.Scripts.Signals;
using _Project.SettingsManager.Scripts.Enums;
using _Project.SettingsManager.Scripts.Controllers;

namespace _Project.SettingsManager.Scripts.Views
{
    public class VideoSettingsMediator : Mediator
    {
        [Inject] public VideoSettingsView View { get; set; }
        [Inject] public ChangeSettingsSignal ChangeSettingsSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onResolutionChanged.AddListener(HandleOnResolutionChanged);
            View.onFullscreenToggled.AddListener(HandleOnFullscreenToggled);
            View.onQualitySliderChanged.AddListener(HandleOnQualitySliderChanged);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onResolutionChanged.RemoveListener(HandleOnResolutionChanged);
            View.onFullscreenToggled.RemoveListener(HandleOnFullscreenToggled);
            View.onQualitySliderChanged.RemoveListener(HandleOnQualitySliderChanged);
        }

        private void HandleOnResolutionChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.Resolution, value.ToString()));
        }

        private void HandleOnFullscreenToggled(bool isFullscreen)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.Fullscreen, isFullscreen.ToString()));
        }

        private void HandleOnQualitySliderChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.Quality, value.ToString()));
        }
    }
}

