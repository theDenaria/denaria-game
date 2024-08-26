using _Project.SceneManagementUtilities;
using _Project.SceneManagementUtilities.Models;
using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

namespace _Project.StrangeIOCUtility.CrossContext
{
	public class StartGameCommand : Command
	{
		[Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
		[Inject] public SceneChangedSignal SceneChangedSignal { get; set; }
		
		[Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }

		public override void Execute()
		{
			CurrentSceneModel.PreviousSceneId = Constants.LOGO_SCENE;
			CurrentSceneModel.CurrentSceneId = Constants.LOGO_SCENE;
			CurrentSceneModel.SceneOpenedEpochTime = DateUtility.GetCurrentEpochSeconds();

			NotifySceneChangeCommandData sceneChangeCommandData = new NotifySceneChangeCommandData(Constants.LOADING_SCENE, Constants.SCENE_COMPLETED, "success");
			SceneChangedSignal.Dispatch(sceneChangeCommandData);
			
			//SceneManager.LoadScene("LoadingScreen");
			
			ChangeSceneGroupSignal.Dispatch(SceneGroupType.Loading, new LoadingOptions());
		}
	}
}