using _Project.SceneManagementUtilities;
using _Project.SceneManagementUtilities.Models;
using _Project.SceneManagementUtilities.Signals;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

namespace _Project.StrangeIOCUtility.CrossContext
{
	public class StartGameCommand : Command
	{
		[Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
		[Inject] public NotifySceneChangeSignal NotifySceneChangeSignal { get; set; }
		public override void Execute()
		{
			CurrentSceneModel.PreviousSceneId = Constants.LOGO_SCENE;
			CurrentSceneModel.CurrentSceneId = Constants.LOGO_SCENE;
			CurrentSceneModel.SceneOpenedEpochTime = DateUtility.GetCurrentEpochSeconds();

			NotifySceneChangeCommandData sceneChangeCommandData = new NotifySceneChangeCommandData(Constants.LOADING_SCENE, Constants.SCENE_COMPLETED, "success");
			NotifySceneChangeSignal.Dispatch(sceneChangeCommandData);
			
			SceneManager.LoadScene("LoadingScreen");
		}
	}
}