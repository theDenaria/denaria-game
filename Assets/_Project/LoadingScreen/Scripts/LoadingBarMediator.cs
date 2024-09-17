using strange.extensions.mediation.impl;

namespace _Project.LoadingScreen.Scripts
{
	public class LoadingBarMediator : Mediator
	{
		[Inject] public StartLoadingSignal StartLoadingSignal { get; set; }
		[Inject] public LoadingBarCompletedSignal LoadingBarCompletedSignal { get; set; }
		[Inject] public LoadingBarView View { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.startLoadingSignal.AddListener(StartLoading);
			//View.loadingBarCompletedSignal.AddListener(ShowTermsServicePopUp);
			View.init();
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.startLoadingSignal.RemoveListener(StartLoading);
			//View.loadingBarCompletedSignal.RemoveListener(ShowTermsServicePopUp);
		}

		private void StartLoading()
		{
			StartLoadingSignal.Dispatch(View);
		}

		[ListensTo(typeof(CompleteLoadingSignal))]
		private void ShowTermsServicePopUp()
		{
			LoadingBarCompletedSignal.Dispatch(View);
		}
	}
}