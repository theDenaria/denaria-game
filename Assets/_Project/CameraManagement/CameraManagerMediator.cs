using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.CameraManagement
{
	public class CameraManagerMediator : Mediator
	{
		[Inject] public CameraManagerView View { get; set; }
		[Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }

		public override void OnRegister()
		{
			ChangeSceneGroupSignal.AddListener(HandleSceneGroupChanged);
			base.OnRegister();
		}

		public override void OnRemove()
		{
			ChangeSceneGroupSignal.RemoveListener(HandleSceneGroupChanged);
			base.OnRemove();
		}

		private void HandleSceneGroupChanged(SceneGroupType sceneGroupType, LoadingOptions loadingOptions)
		{
			View.EnableMainCamera();
		}
	}
}