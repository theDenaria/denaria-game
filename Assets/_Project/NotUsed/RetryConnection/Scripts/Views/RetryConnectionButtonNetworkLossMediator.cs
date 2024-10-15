using _Project.GameLifecycle.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.RetryConnection.Scripts.Views
{
	public class RetryConnectionButtonNetworkLossMediator : Mediator
	{
		[Inject] public RetryConnectionButtonNetworkLossView View { get; set; }
		[Inject] public RetryNetworkConnectionSignal RetryNetworkConnectionSignal { get; set; }

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
			RetryNetworkConnectionSignal.Dispatch();
		}
	}
}