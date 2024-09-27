using _Project.ABTesting.Scripts.Models;
using _Project.PlayerProfile.Scripts.Views;
using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Controllers
{
	public class SetPlayfabIdTextCommand : Command
	{
		[Inject] public UserIdCopyToClipboardButtonView UserIdCopyToClipboardButtonView { get; set; }
		[Inject] public IPlayfabIdModel PlayfabIdModel { get; set; }

		public override void Execute()
		{
			string playfabId = PlayfabIdModel.PlayfabId;

			if(playfabId != string.Empty)
				UserIdCopyToClipboardButtonView.SetPlayfabId(playfabId);
		}
	}
}