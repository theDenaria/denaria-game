using _Project.ABTesting.Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Commands
{
	public class CopyUserIdToClipboardCommand : Command
	{
		[Inject] public IPlayfabIdModel PlayfabIdModel { get; set; }

		public override void Execute()
		{
			GUIUtility.systemCopyBuffer = PlayfabIdModel.PlayfabId;
		}
	}
}