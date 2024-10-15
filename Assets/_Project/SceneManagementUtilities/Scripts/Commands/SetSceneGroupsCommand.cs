using System.Collections.Generic;
using System.Linq;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities.Scripts.Services;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.SceneManagementUtilities.Scripts.Commands
{
    //TODO: Each opened scene should not trigger that. 
    public class SetSceneGroupsCommand : Command
    {
        [Inject] public ISceneChangeService SceneChangeService { get; set; }
        
        public override void Execute()
        {
            SetSceneGroup();
        }
        
        public void SetSceneGroup()
        {
            SceneGroupListData sceneGroupListData = 
                Resources.Load<SceneGroupListData>(nameof(SceneGroupListData));
            
            List<SceneGroupData> sceneGroupDatas = sceneGroupListData.sceneGroupList.Cast<SceneGroupData>().ToList();

            if (SceneChangeService.SceneGroupDataList.Count != 0) return; //To Disable re-adding to the list.
            
            foreach (var sceneGroupData in sceneGroupDatas)
            {
                ValidateSceneGroupData(sceneGroupData);
                SceneChangeService.SceneGroupDataList.Add(sceneGroupData);
            }

        }

        private static void ValidateSceneGroupData(SceneGroupData sceneGroupData)
        {
            if (sceneGroupData.Scenes.Count < 1)
            {
                DebugLoggerMuteable.LogError("At least one scene should be in the list" +
                                             "'sceneGroupData.Scenes' of the group: "
                                             + sceneGroupData.name);
            }

            if (!sceneGroupData.Scenes.Contains(sceneGroupData.FocusedScene))
            {
                DebugLoggerMuteable.LogWarning("Will return first scene as default FocusedScene because " +
                                             "FocusedScene should be in the 'sceneGroupData.Scenes' of the group: "
                                             + sceneGroupData.name);
            }
        }
    }
}