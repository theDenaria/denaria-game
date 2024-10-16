using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

//Executed every time the play button is pressed and checks if the tested scene is in the editor.
//It ensures that the necessary scene group is loaded after BootScene and LoadingScene for testing purposes.
//"SceneGroupData.cs" is executed every time "Add Scenes To Editor" button is pressed in SceneListGroupData scriptable object.
namespace _Project.SceneManagementUtilities.Editor
{
	[InitializeOnLoad]
	public class SceneLoaderForTesting {
		static SceneLoaderForTesting()
		{
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			if (EditorApplication.isPlaying) { return; }
			if (EditorApplication.isPlayingOrWillChangePlaymode)
			{
				Debug.Log("SceneLoaderForTesting is handling which scenes should be opened.");

				string activeSceneNames = GetActiveSceneNames();

				if (activeSceneNames.Contains("BootScene"))
				{
					ConductRegularBoot();
				}
				else
				{
					ConductJumpstartBootTo(activeSceneNames);
				}
			}
			else 
			{
				PlayerPrefs.SetString("SceneGroupToChange", string.Empty);

				Debug.Log("About to exit Play mode");
			}
		}

		private static void ConductRegularBoot()
		{
			PlayerPrefs.SetString("SceneToTest", string.Empty);
			PlayerPrefs.SetString("SceneGroupToChange", string.Empty);//TODO: Use scene group type None?
			PlayerPrefs.SetInt("IsTesting", 0);

			EditorApplication.isPlaying = true;
		}
		
		private static void ConductJumpstartBootTo(string activeSceneNames)
		{
			Debug.Log("ConductJumpstartBootTo method started. Will try to start from a scene other than BootScene.");
			PlayerPrefs.SetString("SceneToTest", activeSceneNames);
			PlayerPrefs.SetInt("IsTesting", 1);

			EditorSceneManager.OpenScene("Assets/_Project/StrangeIOCUtility/Scenes/BootScene.unity");
			EditorApplication.isPlaying = true;
		}

		private static string GetActiveSceneNames()
		{
			string activeSceneNames = string.Empty;

			for(int i = 0; i < SceneManager.sceneCount; i++)
			{
				if (i == SceneManager.sceneCount - 1)
				{
					activeSceneNames += SceneManager.GetSceneAt(i).name;
				}
				else
				{
					activeSceneNames += SceneManager.GetSceneAt(i).name + ",";
				}
			}

			return activeSceneNames;
		}
	}
}