using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using _Project.Utilities;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class ChangeAvatarButtonMediator : Mediator
	{
		[Inject] public ChangeAvatarButtonView View { get; set; }
		[Inject] public LoadAdditiveSceneGroupSignal LoadAdditiveSceneGroupSignal { get; set; }	

		public override void OnRegister()
		{
			base.OnRegister();

			View.onButtonClickSignal.AddListener(OpenAvatarSelectionSceneAdditive);
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onButtonClickSignal.RemoveListener(OpenAvatarSelectionSceneAdditive);
		}

		private void OpenAvatarSelectionSceneAdditive()
		{
			LoadAdditiveSceneGroupSignal.Dispatch(SceneGroupType.AvatarSelection);
		}
	}
}