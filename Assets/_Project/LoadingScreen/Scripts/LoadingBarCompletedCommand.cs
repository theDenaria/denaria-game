/*using _Project.CBSUtility.Models;
using _Project.GameLifecycle.Scripts.Signals;
using _Project.GenrePickScreen.Scripts.Models;
using _Project.HostWelcomeScreen.Scripts.model;
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

		[Inject] public SceneChangedSignal SceneChangedSignal { get; set; }
		[Inject] public SwitchSceneAccordingToProgressSignal SwitchSceneAccordingToProgressSignal { get; set; }

		public override void Execute()
		{
			
			if (OpenWorkedOnSceneDuringDevelopment()) return;

			OpenNextScene();
			
			//StartCheckingNetworkConnectionSignal.Dispatch();
		}

		private bool OpenWorkedOnSceneDuringDevelopment()
		{
#if UNITY_EDITOR
			if (ShouldOpenWorkedOnScenes())
			{
				OpenActivelyWorkedOnScenes();

				return true;
			}
#endif
			return false;
		}

		private void OpenNextScene()
		{
			SwitchSceneAccordingToProgressSignal.Dispatch();
		}

		private void OpenTermsOfServicePopup()
		{
			NotifySceneChangeCommandData changeCommandData = new NotifySceneChangeCommandData(Constants.AGREEMENT_SCENE, Constants.SCENE_COMPLETED, "success");
			SceneChangedSignal.Dispatch(changeCommandData);
			LoadingBarView.Instantiate(LoadingBarView.termsOfServicePopUp, LoadingBarView.transform.parent);
		}

		private static bool ShouldOpenWorkedOnScenes()
		{
			return PlayerPrefs.GetInt("IsTesting" , 0) == 1;
		}

		private void OpenActivelyWorkedOnScenes()
		{
			string[] scenesToTest = PlayerPrefs.GetString("SceneToTest", string.Empty).Split(",");

			SetDummyData(scenesToTest);
			HandleOpenTestScenes(scenesToTest);
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