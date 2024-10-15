using _Project.GameLifecycle.Scripts.Signals;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameLifecycle.Scripts.Commands
{
	public class RetryNetworkConnectionCommand : Command
	{
		[Inject] public NetworkConnectionSuccessSignal NetworkConnectionSuccessSignal { get; set; }

		public override void Execute()
		{
			if (!CheckNetworkConnection()) return;
			NetworkConnectionSuccessSignal.Dispatch();
		}

		private bool CheckNetworkConnection()
		{
			return Application.internetReachability != NetworkReachability.NotReachable;
		}
	}
}