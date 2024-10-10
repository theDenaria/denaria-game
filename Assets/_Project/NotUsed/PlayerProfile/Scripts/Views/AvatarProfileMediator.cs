using _Project.PlayerProfile.Scripts.Commands;
using strange.extensions.mediation.impl;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class AvatarProfileMediator : Mediator
	{
		[Inject] public AvatarProfileView View { get; set; }

		[Inject] public UpdateAvatarViewSignal UpdateAvatarViewSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.onViewEnabled.AddListener(SetAvatar);

			View.Init();
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onViewEnabled.RemoveListener(SetAvatar);
		}
		
		[ListensTo(typeof(PlayerProfileUpdatedSignal))]
		private void SetAvatar()
		{
			UpdateAvatarViewSignal.Dispatch(View);
		}
	}
}