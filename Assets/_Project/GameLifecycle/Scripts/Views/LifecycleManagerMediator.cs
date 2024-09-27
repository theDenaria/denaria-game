using _Project.GameLifecycle.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.GameLifecycle.Scripts.Views
{
	public class LifecycleManagerMediator : Mediator
	{
		[Inject] public LifecycleManagerView View { get; set; }

		[Inject] public ApplicationStartSignal ApplicationStartSignal { get; set; }
		[Inject] public ApplicationFocusChangedSignal ApplicationFocusChangedSignal { get; set; }
		[Inject] public ApplicationPauseSignal ApplicationPauseSignal { get; set; }
		[Inject] public ApplicationQuitSignal ApplicationQuitSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.onGameStartedSignal.AddListener(HandleOnGameStarted);
			View.onApplicationFocusChanged.AddListener(HandleOnApplicationFocusChanged);
			View.onApplicationPause.AddListener(HandleOnApplicationPause);
			View.onApplicationQuit.AddListener(HandleOnApplicationQuit);
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onGameStartedSignal.RemoveListener(HandleOnGameStarted);
			View.onApplicationFocusChanged.RemoveListener(HandleOnApplicationFocusChanged);
			View.onApplicationPause.RemoveListener(HandleOnApplicationPause);
			View.onApplicationQuit.RemoveListener(HandleOnApplicationQuit);
		}

		private void HandleOnGameStarted()
		{
			ApplicationStartSignal.Dispatch();
		}

		private void HandleOnApplicationFocusChanged(bool focus)
		{
			ApplicationFocusChangedSignal.Dispatch(focus);
		}

		private void HandleOnApplicationPause(bool pause)
		{
			ApplicationPauseSignal.Dispatch(pause);
		}

		private void HandleOnApplicationQuit()
		{
			ApplicationQuitSignal.Dispatch();
		}
	}
}