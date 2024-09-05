using System.Collections.Generic;
using _Project.SceneManagementUtilities.Utilities;

namespace _Project.SceneManagementUtilities.Services
{
    public interface ISceneChangeService
    {
        public List<SceneGroupData> SceneGroupDataList { get; set;  }
        public SceneGroupType CurrentSceneGroupType { get; set; }
        public List<SceneGroupType> AdditivelyOpenedSceneGroups { get; set; }
        public bool IsLoading { get; set; }
         
        public bool SceneIsLoaded(string sceneName);

        public List<SceneObject> GetScenesByGroup(SceneGroupType groupType);
        public SceneObject GetFocusedSceneByGroup(SceneGroupType sceneGroupType);
        public List<SceneObject> GetAdditiveScenes();
        public List<string> GetAdditiveSceneNames();
        public List<string> GetSceneNames(List<SceneObject> sceneList);

        List<SceneObject> GetAllLoadedScenes();

        public List<string> GetSceneNameListBySceneList(List<SceneObject> sceneList);
        public void ClearAdditivelyOpenedSceneGroups();
    }
}