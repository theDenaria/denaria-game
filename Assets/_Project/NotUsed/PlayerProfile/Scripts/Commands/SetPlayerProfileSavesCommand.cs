using _Project.PlayerProfile.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.PlayerProfile.Scripts.Commands
{
	public class SetPlayerProfileSavesCommand : Command
	{
		[Inject] public IPlayerProfileService PlayerProfileService { get; set; }

		public override void Execute()
		{
			PlayerProfileService.SetModelAtStart();
		}
	}
}