using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using _Project.Utilities;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class ChangeNicknameButtonMediator : Mediator
	{
		[Inject] public ChangeNicknameButtonView View { get; set; }
		[Inject] public LoadAdditiveSceneGroupSignal LoadAdditiveSceneSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();

			View.onButtonClickSignal.AddListener(OpenNicknameSceneAdditive);
		}

		public override void OnRemove()
		{
			base.OnRemove();

			View.onButtonClickSignal.RemoveListener(OpenNicknameSceneAdditive);
		}

		private void OpenNicknameSceneAdditive()
		{
			//TODO: WHEN SCENE MANAGEMENT COMPLETED WE WILL DISPACTH OUR SCENE MANAGEMENT'S SIGNAL HERE
			LoadAdditiveSceneSignal.Dispatch(SceneGroupType.PlayerProfileEnterName);
		}
	}
}