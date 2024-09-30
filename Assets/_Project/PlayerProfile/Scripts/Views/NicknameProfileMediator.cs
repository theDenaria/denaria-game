using _Project.PlayerProfile.Scripts.Controllers;
using strange.extensions.mediation.impl;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class NicknameProfileMediator : Mediator
	{
		[Inject] public NicknameProfileView View { get; set; }

		[Inject] public UpdateNicknameViewSignal UpdateNicknameViewSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.onViewEnabled.AddListener(SetNickname);

			View.Init();
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onViewEnabled.RemoveListener(SetNickname);
		}
		
		[ListensTo(typeof(PlayerProfileUpdatedSignal))]
		private void SetNickname()
		{
			UpdateNicknameViewSignal.Dispatch(View);
		}
	}
}