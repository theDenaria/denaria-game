using _Project.GameLifecycle.Scripts.Signals;
using _Project.RetryConnection.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.RetryConnection.Scripts.Views
{
    public class RetryConnectionCanvasMediator : Mediator
    {
        [Inject] public RetryConnectionCanvasView View { get; set; }
        public override void OnRegister()
        {
            base.OnRegister();
            View.init();
        }

        [ListensTo(typeof(ToggleRetryConnectionCanvasSignal))]
        private void OnToggleRetryConnectionCanvas(bool toggle)
        {
            View.onToggleCanvas.Dispatch(toggle);
        }

		[ListensTo(typeof(NetworkConnectionLostSignal))]
		private void OnNetworkConnectionLost()
		{
            View.OnNetworkConnectionLost();
		}

		[ListensTo(typeof(NetworkConnectionSuccessSignal))]
		private void OnNetworkConnectionSuccess()
		{
			View.OnNetworkConnectionSuccess();
		}
	}
}