using _Project.SettingsManager.Scripts.Signals;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
namespace _Project.SettingsManager.Scripts.Views
{
    public class SettingsViewMediator : Mediator
    {
        [Inject]
        public SettingsView view { get; set; }

        [Inject]
        public ApplySettingsSignal applySettingsSignal { get; set; }

        [Inject]
        public RestoreDefaultSettingsSignal restoreDefaultSettingsSignal { get; set; }

        public override void OnRegister()
        {
            // Link UI buttons to signals
            view.applyButton.onClick.AddListener(() => applySettingsSignal.Dispatch());
            view.restoreDefaultsButton.onClick.AddListener(() => restoreDefaultSettingsSignal.Dispatch());
        }

        public override void OnRemove()
        {
            // Clean up listeners
            view.applyButton.onClick.RemoveListener(() => applySettingsSignal.Dispatch());
            view.restoreDefaultsButton.onClick.RemoveListener(() => restoreDefaultSettingsSignal.Dispatch());
        }
    }
}
