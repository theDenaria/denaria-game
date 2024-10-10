using _Project.PlayerProfile.Scripts.Services;
using _Project.PlayerProfile.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.PlayerProfile.Scripts.Commands
{
	public class UpdateNicknameViewCommand : Command
	{
		[Inject] public NicknameProfileView View { get; set; }

		[Inject] public IPlayerProfileService PlayerProfileService { get; set; }

		public override void Execute()
		{
			View.SetNickname(PlayerProfileService.GetPlayerName());
		}
	}
}