using _Project.SettingsManager.Scripts.Controllers;
using _Project.SettingsManager.Scripts.Enums;
using _Project.SettingsManager.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.SettingsManager.Scripts.Views
{
    public class HotkeySettingsMediator : Mediator
    {
        [Inject] public HotkeySettingsView View { get; set; }
        [Inject] public ChangeSettingsSignal ChangeSettingsSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onMoveForwardInputFieldChanged.AddListener(HandleMoveForwardInputFieldChanged);
            View.onMoveBackwardInputFieldChanged.AddListener(HandleMoveBackwardInputFieldChanged);
            View.onMoveLeftInputFieldChanged.AddListener(HandleMoveLeftInputFieldChanged);
            View.onMoveRightInputFieldChanged.AddListener(HandleMoveRightInputFieldChanged);
            View.onJumpInputFieldChanged.AddListener(HandleJumpInputFieldChanged);
            View.onCrouchInputFieldChanged.AddListener(HandleCrouchInputFieldChanged);
        }
        public override void OnRemove()
        {
            base.OnRemove();
            View.onMoveForwardInputFieldChanged.RemoveListener(HandleMoveForwardInputFieldChanged);
            View.onMoveBackwardInputFieldChanged.RemoveListener(HandleMoveBackwardInputFieldChanged);
            View.onMoveLeftInputFieldChanged.RemoveListener(HandleMoveLeftInputFieldChanged);
            View.onMoveRightInputFieldChanged.RemoveListener(HandleMoveRightInputFieldChanged);
            View.onJumpInputFieldChanged.RemoveListener(HandleJumpInputFieldChanged);
            View.onCrouchInputFieldChanged.RemoveListener(HandleCrouchInputFieldChanged);
        }
        public void HandleMoveForwardInputFieldChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.MoveForwardKey, value.ToString()));
        }

        public void HandleMoveBackwardInputFieldChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.MoveBackwardKey, value.ToString()));
        }

        public void HandleMoveLeftInputFieldChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.MoveLeftKey, value.ToString()));
        }

        public void HandleMoveRightInputFieldChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.MoveRightKey, value.ToString()));
        }

        public void HandleJumpInputFieldChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.JumpKey, value.ToString()));
        }

        public void HandleCrouchInputFieldChanged(float value)
        {
            ChangeSettingsSignal.Dispatch(new ChangeSettingsCommandData(SettingsEnum.CrouchKey, value.ToString()));
        }

    }
}



