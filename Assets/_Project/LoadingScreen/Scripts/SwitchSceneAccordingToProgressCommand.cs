//using _Project.GameProgression.Scripts.Models;//TODO: Uncomment 21 August
//using _Project.GameProgression.Scripts.Services;//TODO: Uncomment 21 August
using _Project.SceneManagementUtilities;
using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using _Project.Utilities;
using strange.extensions.command.impl;

namespace _Project.LoadingScreen.Scripts
{
	public class SwitchSceneAccordingToProgressCommand : Command
	{
		[Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }
		//[Inject] public IGameProgressService GameProgressService { get; set; }//TODO: Uncomment 21 August
		[Inject] public SceneChangedSignal SceneChangedSignal { get; set; }

		public override void Execute()
		{
			ChangeScene();
		}

		private void ChangeScene()
		{
			SceneChangedCommandData sceneChangedCommandData;
			/*switch (GameProgressService.GetGameProgress())//TODO: Uncomment 21 August
			{
				case GameProgress.None:
					sceneChangeCommandData = new NotifySceneChangeCommandData(Constants.USER_NAME_SCENE,
						Constants.CONTINUE_BUTTON, "success");
					NotifySceneChangeSignal.Dispatch(sceneChangeCommandData);

					ChangeSceneGroupSignal.Dispatch(SceneGroupType.MainMenu, new LoadingOptions());
					break;
			}*/
			sceneChangedCommandData = new SceneChangedCommandData(Constants.LOADING_SCENE,
				Constants.CONTINUE_BUTTON, "success");
			SceneChangedSignal.Dispatch(sceneChangedCommandData);

			ChangeSceneGroupSignal.Dispatch(SceneGroupType.MainMenu, new LoadingOptions());
			
		}
	}
}