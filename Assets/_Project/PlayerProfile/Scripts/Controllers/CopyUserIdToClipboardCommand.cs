using _Project.ABTesting.Scripts.Models;
using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Controllers
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