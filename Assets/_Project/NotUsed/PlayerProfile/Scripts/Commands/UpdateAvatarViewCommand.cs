using _Project.PlayerProfile.Scripts.Services;
using _Project.PlayerProfile.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.PlayerProfile.Scripts.Commands
{
	public class UpdateAvatarViewCommand : Command
	{
		[Inject] public AvatarProfileView View { get; set; }

		[Inject] public IPlayerProfileService PlayerProfileService { get; set; }
		
		public override void Execute()
		{
			//View.SetAvatar(InterestDatabaseModel.InterestList[PlayerProfileService.GetInterestIndex()].defaultIcon);
		}
	}
}