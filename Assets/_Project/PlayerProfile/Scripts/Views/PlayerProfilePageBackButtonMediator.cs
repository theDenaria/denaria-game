using _Project.PlayerProfile.Scripts.Controllers;
using _Project.ShowLoading.Signals;
using strange.extensions.mediation.impl;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class PlayerProfilePageBackButtonMediator : Mediator
	{
		[Inject] public PlayerProfilePageBackButtonView View { get; set; }
		[Inject] public ClosePlayerProfilePageSignal ClosePlayerProfilePageSignal { get; set; }
		[Inject] public HideLoadingAnimationSignal HideLoadingAnimationSignal { get; set; }
		public override void OnRegister()
		{
			base.OnRegister();

			View.onButtonClick.AddListener(HandleButtonClick);
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onButtonClick.RemoveListener(HandleButtonClick);
		}

		private void HandleButtonClick()
		{
			ClosePlayerProfilePageSignal.Dispatch();
			HideLoadingAnimationSignal.Dispatch();
		}
	}
}