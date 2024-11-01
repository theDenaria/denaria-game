using _Project.Shooting.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Shooting.Scripts.Views
{
    public class PlayerActionMediator : Mediator
    {
        [Inject] public ShootSignal ShootSignal { get; set; }
        [Inject] public StopShootingSignal StopShootingSignal { get; set; }
        
        [Inject] public PlayerAction View { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onFireButtonIsPressedSignal.AddListener(HandleOnFireButtonIsPressed);
            View.onFireButtonReleasedSignal.AddListener(HandleOnFireButtonReleased);
            View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.onFireButtonIsPressedSignal.RemoveListener(HandleOnFireButtonIsPressed);
            View.onFireButtonReleasedSignal.RemoveListener(HandleOnFireButtonReleased);
        }

        private void HandleOnFireButtonIsPressed()
        {
            ShootSignal.Dispatch();
        }

        public void HandleOnFireButtonReleased()
        {
            StopShootingSignal.Dispatch();
        }

    }
}