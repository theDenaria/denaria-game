using System.Collections.Generic;
using _Project.SceneManagementUtilities.Scripts.Services;
using _Project.SceneManagementUtilities.Utilities;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

namespace _Project.SceneManagementUtilities.Scripts.Commands
{
	public class LoadAdditiveSceneGroupCommand : Command
	{
		[Inject] public SceneGroupType SceneGroupToOpenAdditive { get; set; }

		[Inject] public ISceneChangeService SceneChangeService { get; set; }

		public override void Execute()
		{	
			List<string> scenesToOpenAdditive = new List<string>();

			foreach(SceneObject scene in SceneChangeService.GetScenesByGroup(SceneGroupToOpenAdditive))
			{
				scenesToOpenAdditive.Add(scene.SceneName);
				SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
			}

			SceneChangeService.AdditivelyOpenedSceneGroups.Add(SceneGroupToOpenAdditive);
		}
	}
}