using _Project.PlayerProfile.Scripts.Services;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Controllers
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