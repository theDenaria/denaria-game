/*using _Project.CBSUtility.Models;
using _Project.GameLifecycle.Scripts.Signals;
using _Project.PlayerProfile.Scripts.Services;*///TODO: Uncomment 21 August
using _Project.LoadingScreen.Models;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities;
using _Project.SceneManagementUtilities.Services;
using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using _Project.Utilities;
using Newtonsoft.Json;
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.LoadingScreen.Scripts
{
	public class LoadingBarCompletedCommand : Command
	{
		[Inject] public LoadingBarView LoadingBarView { get; set; }
		[Inject] public ISceneChangeService SceneChangeService { get; set; }
		//[Inject] public IPlayerProfileService PlayerProfileService { get; set; }//TODO: Uncomment 21 August
		//[Inject] public ICustomProfileData CustomProfileData { get; set; }//TODO: Uncomment 21 August
		//[Inject] public StartCheckingNetworkConnectionSignal StartCheckingNetworkConnectionSignal { get; set; }//TODO: Uncomment 21 August

		[Inject] public NotifySceneChangeSignal NotifySceneChangeSignal { get; set; }
		[Inject] public SwitchSceneAccordingToProgressSignal SwitchSceneAccordingToProgressSignal { get; set; }

		public override void Execute()
		{
#if UNITY_EDITOR
			if (PlayerPrefs.GetInt("IsTesting" , 0) == 1)
			{
				string[] scenesToTest = PlayerPrefs.GetString("SceneToTest", string.Empty).Split(",");

				SetDummyData(scenesToTest);
				HandleOpenTestScenes(scenesToTest);

				return;
			}
#endif
			ITermsOfServiceModel termsOfServiceModel = new TermsOfServiceModel();
			string termsOfServiceAcceptedString = "termsOfServiceAcceptedString"; //CustomProfileData.ProfileDataDictionary[Constants.PLAYER_TERMS_OF_SERVICE_ACCEPTED];
                                                                         ////TODO: Uncomment 21 August
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
				NotifySceneChangeCommandData changeCommandData = new NotifySceneChangeCommandData(Constants.AGREEMENT_SCENE, Constants.SCENE_COMPLETED, "success");
				NotifySceneChangeSignal.Dispatch(changeCommandData);
			}
			else
			{
				SwitchSceneAccordingToProgressSignal.Dispatch();
			}

			//StartCheckingNetworkConnectionSignal.Dispatch();
		}

		private void SetDummyData(string[] scenesToTest)
		{
			/*
			if (PlayerProfileService.GetPlayerName() == null)
			{
				PlayerProfileService.SavePlayerNickname("Test");
			}*/
		}

		private void HandleOpenTestScenes(string[] scenesToTest)
		{
			if (PlayerPrefs.GetString("SceneGroupToChange", string.Empty) != string.Empty)
			{
				SceneChangeService.CurrentSceneGroupType = (SceneGroupType)System.Enum.Parse(typeof(SceneGroupType), PlayerPrefs.GetString("SceneGroupToChange"));
			}

			for (int i = 0; i < scenesToTest.Length; i++)
			{
				if (scenesToTest.Equals(string.Empty))
					continue;

				if (i == 0)
				{
					SceneManager.LoadScene(scenesToTest[i], LoadSceneMode.Single);
				}
				else
				{
					SceneManager.LoadScene(scenesToTest[i], LoadSceneMode.Additive);
				}
			}
		}
	}
}