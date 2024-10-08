using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.GameLifecycle.Scripts.Views
{
	public class NetworkConnectionCheckerView : ViewZeitnot
	{
		internal Signal onNetworkConnectionLost = new Signal();

		public void StartCheckNetworkConnection()
		{
			InvokeRepeating(nameof(CheckNetwork), 0, 1f);
		}

		public void CheckNetwork()
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				onNetworkConnectionLost.Dispatch();
			}
		}
	}
}