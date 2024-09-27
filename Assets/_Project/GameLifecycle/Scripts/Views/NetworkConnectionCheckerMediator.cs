using _Project.GameLifecycle.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.GameLifecycle.Scripts.Views
{
	public class NetworkConnectionCheckerMediator : Mediator
	{
		[Inject] public NetworkConnectionCheckerView View { get; set; }
		[Inject] public NetworkConnectionLostSignal NetworkConnectionLostSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.onNetworkConnectionLost.AddListener(HandleNetworkConnectionLost);
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onNetworkConnectionLost.RemoveListener(HandleNetworkConnectionLost);
		}

		private void HandleNetworkConnectionLost()
		{
			NetworkConnectionLostSignal.Dispatch();
		}

		[ListensTo(typeof(StartCheckingNetworkConnectionSignal))]
		private void StartCheckingNetworkConnection()
		{
			View.StartCheckNetworkConnection();
		}
	}
}