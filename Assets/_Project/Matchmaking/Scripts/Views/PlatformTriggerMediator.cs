
using _Project.Matchmaking.Scripts.Enums;
using _Project.Matchmaking.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Matchmaking.Scripts.Views
{
    public class PlatformTriggerMediator : Mediator
    {
        [Inject] public PlatformTriggerView View { get; set; }
        [Inject] public PlatformTriggerEnterSignal PlatformTriggerEnterSignal { get; set; }
        [Inject] public PlatformTriggerExitSignal PlatformTriggerExitSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onPlatformTriggerEnter.AddListener(HandleOnPlatformTriggerEnter);
            View.onPlatformTriggerExit.AddListener(HandleOnPlatformTriggerExit);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onPlatformTriggerEnter.RemoveListener(HandleOnPlatformTriggerEnter);
            View.onPlatformTriggerExit.RemoveListener(HandleOnPlatformTriggerExit);
        }

        private void HandleOnPlatformTriggerEnter(MatchmakingPlatformEnum platform)
        {
            PlatformTriggerEnterSignal.Dispatch(new PlatformTriggerEnterSignalData(platform));
        }

        private void HandleOnPlatformTriggerExit()
        {
            PlatformTriggerExitSignal.Dispatch();
        }
    }
}