using _Project.SceneManagementUtilities.Scripts.Commands;
using _Project.SceneManagementUtilities.Scripts.Models;
using _Project.SceneManagementUtilities.Scripts.Signals;
using _Project.SceneManagementUtilities.Utilities;
using _Project.Utilities;
using strange.extensions.command.impl;

namespace _Project.StrangeIOCUtility.Scripts.Commands
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

			SceneChangedCommandData sceneChangedCommandData = new SceneChangedCommandData(Constants.LOADING_SCENE, Constants.SCENE_COMPLETED, "success");
			SceneChangedSignal.Dispatch(sceneChangedCommandData);
			
			ChangeSceneGroupSignal.Dispatch(SceneGroupType.Loading, new LoadingOptions());
		}
	}
}