using _Project.RetryConnection.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.RetryConnection.Scripts.Views
{
    public class RetryConnectionButtonMediator : Mediator
    {
        [Inject] public RetryConnectionButtonView View { get; set; }
        [Inject] public RetryConnectionSignal RetryConnectionSignal { get; set; }
        [Inject] public ToggleRetryConnectionCanvasSignal ToggleRetryConnectionCanvasSignal { get; set; }
        public override void OnRegister()
        {
            base.OnRegister();

            View.onRetryButtonClick.AddListener(OnRetryButtonClick);
            
            View.init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            View.onRetryButtonClick.RemoveListener(OnRetryButtonClick);
        }

        private void OnRetryButtonClick()
        {
            RetryConnectionSignal.Dispatch();
        }
        
    }
}