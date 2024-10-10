using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;

namespace _Project.GameLifecycle.Scripts.Views
{
	public class LifecycleManagerView : ViewZeitnot
	{
		internal Signal onGameStartedSignal = new Signal();
		internal Signal<bool> onApplicationFocusChanged = new Signal<bool>();
		internal Signal<bool> onApplicationPause = new Signal<bool>();
		internal Signal onApplicationQuit = new Signal();

		protected override void Awake()
		{
			base.Awake();
			onGameStartedSignal.Dispatch();
		}

		private void OnApplicationFocus(bool focus)
		{
			onApplicationFocusChanged.Dispatch(focus);
		}

		private void OnApplicationPause(bool pause)
		{
			onApplicationPause.Dispatch(pause);
		}

		private void OnApplicationQuit()
		{
			onApplicationQuit.Dispatch();
		}
	}
}