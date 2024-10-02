using _Project.ABTesting.Scripts.Signals;
using _Project.PlayerProfile.Scripts.Controllers;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class UserIdCopyToClipboardButtonMediator : Mediator
	{
		[Inject] public UserIdCopyToClipboardButtonView View { get; set; }
		[Inject] public CopyUserIdToClipboardSignal CopyUserIdToClipboardSignal { get; set; }
		[Inject] public SetPlayfabIdTextSignal SetPlayfabIdTextSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.onButtonClick.AddListener(CopyUserIdToClipboard);
			View.onViewRegistered.AddListener(GetPlayfabIdText);

			View.Init();
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onButtonClick.RemoveListener(CopyUserIdToClipboard);
			View.onViewRegistered.RemoveListener(GetPlayfabIdText);
		}

		private void CopyUserIdToClipboard()
		{
			CopyUserIdToClipboardSignal.Dispatch();
		}

		[ListensTo(typeof(PlayfabIdReceivedSignal))]
		private void SetPlayfabIdText(string playfabId)
		{
			View.SetPlayfabId(playfabId);
		}

		private void GetPlayfabIdText()
		{
			SetPlayfabIdTextSignal.Dispatch(View);
		}
	}
}