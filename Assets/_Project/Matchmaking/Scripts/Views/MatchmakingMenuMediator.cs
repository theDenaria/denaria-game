using _Project.Matchmaking.Scripts.Commands;
using _Project.Matchmaking.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Matchmaking.Scripts.Views
{
    public class MatchmakingMenuMediator : Mediator
    {
        [Inject] public MatchmakingMenuView View { get; set; }
        [Inject] public SearchButtonClickedSignal SearchButtonClickedSignal { get; set; }
        [Inject] public CancelButtonClickedSignal CancelButtonClickedSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onSearchButtonClick.AddListener(HandleOnSearchButtonClick);
            View.onCancelButtonClick.AddListener(HandleOnCancelButtonClick);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onSearchButtonClick.RemoveListener(HandleOnSearchButtonClick);
            View.onCancelButtonClick.RemoveListener(HandleOnCancelButtonClick);
        }

        private void HandleOnSearchButtonClick()
        {
            SearchButtonClickedSignal.Dispatch(View.CurrentPlatform);
        }

        private void HandleOnCancelButtonClick()
        {
            CancelButtonClickedSignal.Dispatch();
            View.CancelMatchmaking();
        }

        [ListensTo(typeof(QueueStartedSignal))]
        public void HandleOnQueueStarted()
        {
            View.StartMatchmaking();
        }

        [ListensTo(typeof(MatchFoundSignal))]
        public void HandleOnMatchFound(MatchFoundCommandData matchFoundCommandData)
        {
            View.CancelMatchmaking();
        }

        [ListensTo(typeof(QueueFinishedSignal))]
        public void HandleOnQueueFinished()
        {
            View.CancelMatchmaking();
        }

        [ListensTo(typeof(PlatformTriggerEnterSignal))]
        public void HandleOnPlatformTriggerEnter(PlatformTriggerEnterSignalData platformTriggerEnterSignalData)
        {
            View.ShowMatchmakingMenu(platformTriggerEnterSignalData.Platform);
        }

        [ListensTo(typeof(PlatformTriggerExitSignal))]
        public void HandleOnPlatformTriggerExit()
        {
            View.HideMatchmakingMenu();
        }

    }
}