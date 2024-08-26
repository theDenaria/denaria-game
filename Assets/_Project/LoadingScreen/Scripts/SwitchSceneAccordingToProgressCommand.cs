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
			NotifySceneChangeCommandData sceneChangeCommandData;
			/*switch (GameProgressService.GetGameProgress())//TODO: Uncomment 21 August
			{
				case GameProgress.None:
					sceneChangeCommandData = new NotifySceneChangeCommandData(Constants.USER_NAME_SCENE,
						Constants.CONTINUE_BUTTON, "success");
					SceneChangedSignal.Dispatch(sceneChangeCommandData);

					ChangeSceneGroupSignal.Dispatch(SceneGroupType.MainMenu, new LoadingOptions());
					break;
			}*/
			sceneChangeCommandData = new NotifySceneChangeCommandData(Constants.LOADING_SCENE,
				Constants.CONTINUE_BUTTON, "success");
			SceneChangedSignal.Dispatch(sceneChangeCommandData);

			ChangeSceneGroupSignal.Dispatch(SceneGroupType.MainMenu, new LoadingOptions());
			
		}

		void OpenTermsOfServiceScene()
		{
			/*////TODO: Uncomment 21 August //Originally was in LoadingBarCompletedCommand

			ITermsOfServiceModel termsOfServiceModel = new TermsOfServiceModel();
			string termsOfServiceAcceptedString = "termsOfServiceAcceptedString"; //CustomProfileData.ProfileDataDictionary[Constants.PLAYER_TERMS_OF_SERVICE_ACCEPTED];
			if (termsOfServiceAcceptedString.Equals(Constants.VALUE_IS_NOT_EXISTS))
			{
				termsOfServiceModel.IsAccepted = false;
			}
			else
			{
				termsOfServiceModel =  JSONUtilityZeitnot.TryDeserializeObject<ITermsOfServiceModel>(termsOfServiceAcceptedString);
			}

			DebugLoggerMuteable.Log("termsOfServiceModel is fetched via LoadingBarCompletedCommand, and it was: " + termsOfServiceModel.IsAccepted);

			if (!termsOfServiceModel.IsAccepted)
			{
				OpenTermsOfServicePopup();
			}
			else
			{
				SwitchSceneAccordingToProgressSignal.Dispatch();
			}*/
		}
	}
}