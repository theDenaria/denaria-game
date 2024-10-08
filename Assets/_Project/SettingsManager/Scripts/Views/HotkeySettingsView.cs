using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;

namespace _Project.SettingsManager.Scripts.Views
{
    public class HotkeySettingsView : ViewZeitnot
    {
        internal Signal<float> onMoveForwardInputFieldChanged = new Signal<float>();
        internal Signal<float> onMoveBackwardInputFieldChanged = new Signal<float>();
        internal Signal<float> onMoveLeftInputFieldChanged = new Signal<float>();
        internal Signal<float> onMoveRightInputFieldChanged = new Signal<float>();
        internal Signal<float> onJumpInputFieldChanged = new Signal<float>();
        internal Signal<float> onCrouchInputFieldChanged = new Signal<float>();

        public void MoveForwardInputFieldChanged(float value)
        {
            onMoveForwardInputFieldChanged?.Dispatch(value);
        }

        public void MoveBackwardInputFieldChanged(float value)
        {
            onMoveBackwardInputFieldChanged?.Dispatch(value);
        }

        public void MoveLeftInputFieldChanged(float value)
        {
            onMoveLeftInputFieldChanged?.Dispatch(value);
        }

        public void MoveRightInputFieldChanged(float value)
        {
            onMoveRightInputFieldChanged?.Dispatch(value);
        }

        public void JumpInputFieldChanged(float value)
        {
            onJumpInputFieldChanged?.Dispatch(value);
        }

        public void CrouchInputFieldChanged(float value)
        {
            onCrouchInputFieldChanged?.Dispatch(value);
        }
    }
}
