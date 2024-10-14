using _Project.Login.Scripts.Signals;
using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;

namespace _Project.Animation.Scripts.Views
{
    public class GroundColliderMediator : Mediator
    {
        [Inject] public GroundColliderView View { get; set; }
        [Inject] public OnLandColliderEnterSignal OnLandColliderEnterSignal { get; set; }
        [Inject] public OnLandColliderExitSignal OnLandColliderExitSignal { get; set; }

        public override void OnRegister()
        {
            View.onLandColliderEnter.AddListener(OnLandColliderEnter);
            View.onLandColliderExit.AddListener(OnLandColliderExit);
        }
        private void OnDisable()
        {
            View.onLandColliderEnter.RemoveListener(OnLandColliderEnter);
            View.onLandColliderExit.RemoveListener(OnLandColliderExit);
        }

        private void OnLandColliderEnter()
        {
            OnLandColliderEnterSignal.Dispatch();
        }

        private void OnLandColliderExit()
        {
            OnLandColliderExitSignal.Dispatch();
        }

    }
}