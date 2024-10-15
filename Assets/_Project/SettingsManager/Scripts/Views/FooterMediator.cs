using _Project.SettingsManager.Scripts.Signals;
using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace _Project.SettingsManager.Scripts.Views
{
    public class FooterMediator : Mediator
    {
        [Inject] public FooterView View { get; set; }
        [Inject] public ApplySettingsSignal ApplySettingsSignal { get; set; }
        [Inject] public RestoreDefaultSettingsSignal RestoreDefaultSettingsSignal { get; set; }
        //[Inject] public SettingsMenuClosedSignal SettingsMenuClosedSignal { get; set; }

        public override void OnRegister()
        {
            View.onApplyButtonClicked.AddListener(HandleApplySettings);
            View.onRestoreButtonClicked.AddListener(HandleRestoreDefaultSettings);
            Debug.Log("UUU onRegister Before onBackButton");
            View.onBackButtonClicked.AddListener(HandleBackButtonClicked);
            Debug.Log("UUU onRegister After onBackButton");
        }
        public override void OnRemove()
        {
            View.onApplyButtonClicked.RemoveListener(HandleApplySettings);
            View.onRestoreButtonClicked.RemoveListener(HandleRestoreDefaultSettings);
            View.onBackButtonClicked.RemoveListener(HandleBackButtonClicked);
        }

        private void HandleApplySettings()
        {
            ApplySettingsSignal.Dispatch();
        }

        private void HandleRestoreDefaultSettings()
        {
            RestoreDefaultSettingsSignal.Dispatch();
        }

        private void HandleBackButtonClicked()
        {
            Debug.Log("HandleBackButtonClicked MEDIATOR");
            View.SettingsPanel.SetActive(false);
            View.MainMenuPanel.SetActive(true);
            Debug.Log("SettingsMenuClosedSignal dispatch");
        }

    }
}
