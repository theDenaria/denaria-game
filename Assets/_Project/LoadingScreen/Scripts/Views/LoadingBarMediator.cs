using _Project.LoadingScreen.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.LoadingScreen.Scripts.Views
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
			View.temporaryLoadingBarCompletedSignal.AddListener(ShowTermsServicePopUp);

			View.init();
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.startLoadingSignal.RemoveListener(StartLoading);
			//View.loadingBarCompletedSignal.RemoveListener(ShowTermsServicePopUp);
			View.temporaryLoadingBarCompletedSignal.RemoveListener(ShowTermsServicePopUp);

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