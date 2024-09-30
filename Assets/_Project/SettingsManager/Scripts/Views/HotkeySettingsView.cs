using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

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
