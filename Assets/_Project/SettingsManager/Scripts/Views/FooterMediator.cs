using _Project.SettingsManager.Scripts.Signals;
using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class FooterMediator : Mediator
{
    [Inject] public FooterView View { get; set; }
    [Inject] public ApplySettingsSignal ApplySettingsSignal { get; set; }
    [Inject] public RestoreDefaultSettingsSignal RestoreDefaultSettingsSignal { get; set; }

    public override void OnRegister()
    {
        View.onApplyButtonClicked.AddListener(ApplySettings);
        View.onRestoreButtonClicked.AddListener(RestoreDefaultSettings);
    }
    public override void OnRemove()
    {
        View.onApplyButtonClicked.RemoveListener(ApplySettings);
        View.onRestoreButtonClicked.RemoveListener(RestoreDefaultSettings);
    }

    private void ApplySettings()
    {
        ApplySettingsSignal.Dispatch();
    }

    private void RestoreDefaultSettings()
    {
        RestoreDefaultSettingsSignal.Dispatch();
    }

}