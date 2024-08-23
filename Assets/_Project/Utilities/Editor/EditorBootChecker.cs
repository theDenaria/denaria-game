using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class EditorBootChecker
{
	static EditorBootChecker()
	{
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state)
	{
		if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
		{
			string activeScenes = string.Empty;

			for(int i = 0; i < SceneManager.sceneCount; i++)
			{
				if (i == SceneManager.sceneCount - 1)
				{
					activeScenes += SceneManager.GetSceneAt(i).name;
				}
				else
				{
					activeScenes += SceneManager.GetSceneAt(i).name + ",";
				}
			}

			if (activeScenes.Contains("BootScene"))
			{
				PlayerPrefs.SetString("SceneToTest", string.Empty);
				PlayerPrefs.SetString("SceneGroupToChange", string.Empty);
				PlayerPrefs.SetInt("IsTesting", 0);

				EditorApplication.isPlaying = true;
			}
			else
			{
				PlayerPrefs.SetString("SceneToTest", activeScenes);
				PlayerPrefs.SetInt("IsTesting", 1);

				EditorSceneManager.OpenScene("Assets/_Project/StrangeIOCUtility/CrossContext/BootScene.unity");
				EditorApplication.isPlaying = true;
			}
		}
		else if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
		{
			PlayerPrefs.SetString("SceneGroupToChange", string.Empty);

			Debug.Log("About to exit Play mode");
		}
	}
}
