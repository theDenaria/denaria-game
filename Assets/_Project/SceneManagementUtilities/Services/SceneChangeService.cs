using System;
using System.Collections.Generic;
using System.Linq;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.SceneManagementUtilities.Services
{
//NOTE : This works with SceneGroupListData.asset file.
// READ ME :
/*
 You call ChangeScenes method;
 if any of the scene you passed is already loaded and LoadingOptions.WillReloadExistingScenes is false then LoadingManager will not unload or reload this scene.
 order of operations is:
 1-) Show LoadingScene if LoadingOptions.WillUseLoadingScreen
 2-) Load the new scenes needed in new scene group.
 3-) Once the first scene is loaded, focus it.
 4-) Unload the scenes to be removed in previous scene group. (except LoadingScene)
 5-) When both loading and unloading are done hide to loading screen.
*/
    public class SceneChangeService : ISceneChangeService
    {
        public static event Action OnScenesLoaded;
        public static event Action OnLoadingFinished;
        
        //This initialization below looks redundant but needed to prevent null reference exception in SetSceneGroupCommand
        public List<SceneGroupData> SceneGroupDataList { get; set; } = new List<SceneGroupData>();
        public SceneGroupType CurrentSceneGroupType { get; set; } = SceneGroupType.Boot; //Assuming game starts with BootScene
        public List<SceneGroupType> AdditivelyOpenedSceneGroups { get; set; } = new List<SceneGroupType>();
		public bool IsLoading { get; set; } = false;

        public List<SceneObject> GetScenesByGroup(SceneGroupType sceneGroupType)
        {
            SceneGroupData sceneGroupData = GetSceneGroupDataByGroup(sceneGroupType);
            return sceneGroupData.Scenes;
        }
        
        public List<string> GetAdditiveSceneNames()
        {
            List<SceneObject> additiveScenes = GetAdditiveScenes();
            
            List<string> sceneNames = GetSceneNames(additiveScenes);
            
            return sceneNames;
        }

        public SceneObject GetFocusedSceneByGroup(SceneGroupType sceneGroupType)
        {
            SceneGroupData sceneGroupData = GetSceneGroupDataByGroup(sceneGroupType);

            if (sceneGroupData.Scenes.Count < 1)
            {
                DebugLoggerMuteable.LogError("At least one scene should be in the list" +
                                             "'sceneGroupData.Scenes' of the group: " 
                                             + sceneGroupData.name);
            }

            if (sceneGroupData.FocusedScene != null)
            {
                return sceneGroupData.Scenes[0];
            }

            if (!sceneGroupData.Scenes.Contains(sceneGroupData.FocusedScene))
            {
                DebugLoggerMuteable.LogWarning("Returning first scene as default FocusedScene because " +
                                             "FocusedScene should be in the 'sceneGroupData.Scenes' of the group: " 
                                             + sceneGroupData.name);
            }
            return sceneGroupData.FocusedScene;

        }
        
        public List<SceneObject> GetAdditiveScenes()
        {
            List<SceneObject> additiveScenes = new List<SceneObject>();
            foreach (SceneGroupType additivelyOpenedSceneGroup in AdditivelyOpenedSceneGroups)
            {
                List<SceneObject> additivelyOpenedScenesInThisGroup = GetScenesByGroup(additivelyOpenedSceneGroup);
                foreach (SceneObject additivelyOpenedSceneInThisGroup in additivelyOpenedScenesInThisGroup)
                {
                    if (!additiveScenes.Contains(additivelyOpenedSceneInThisGroup))
                    {
                        additiveScenes.Add(additivelyOpenedSceneInThisGroup);
                    }
                }
            }

            return additiveScenes;
        }

        private SceneGroupData GetSceneGroupDataByGroup(SceneGroupType sceneGroupType)
        {
            if (sceneGroupType == SceneGroupType.None)
            {
                var dummySceneGroupData = ScriptableObject.CreateInstance<SceneGroupData>();
                dummySceneGroupData.Scenes = new List<SceneObject>();
                dummySceneGroupData.SceneGroupType = SceneGroupType.None;
                return dummySceneGroupData;
            }
            return SceneGroupDataList.Find(x => x.SceneGroupType == sceneGroupType);
        }
        
        public List<string> GetSceneNames(List<SceneObject> sceneList)
        {
            List<string> sceneNames = new List<string>();

            foreach (SceneObject sceneObject in sceneList)
            {
                if (!sceneNames.Contains(sceneObject.SceneName))
                {
                    sceneNames.Add(sceneObject.SceneName);
                }
            }
            
            return sceneNames;
        }
        
        public List<string> GetSceneNameListBySceneList(List<SceneObject> sceneList)
        {
            List<string> sceneNameList = new List<string>();

            if (sceneList != null)
            {
                foreach (SceneObject sceneObject in sceneList)
                {
                    sceneNameList.Add(sceneObject.SceneName);
                }
            }

            return sceneNameList;
        }

        public List<SceneObject> GetAllLoadedScenes()
        {
            List<SceneObject> existingScenes = new List<SceneObject>();
            
            List<SceneObject> scenesInCurrentGroup = GetScenesByGroup(CurrentSceneGroupType);
            List<SceneObject> additiveScenes = GetAdditiveScenes();

            existingScenes = scenesInCurrentGroup.Union(additiveScenes).ToList();
            return existingScenes;
        }

        public bool SceneIsLoaded(string sceneName)
        {
            return SceneManager.GetSceneByName(sceneName).isLoaded;
        }

        public bool SceneIsLoaded(SceneObject sceneObject)
        {
            return SceneManager.GetSceneByName(sceneObject.SceneName).isLoaded;
        }

        public void ClearAdditivelyOpenedSceneGroups()
        {
            List<SceneObject> existingScenes = GetAllLoadedScenes();

            foreach (SceneGroupType additivelyOpenedSceneGroup in AdditivelyOpenedSceneGroups)
            {
                List<SceneObject> neededAdditivelyOpenedScenesInGroup = GetScenesByGroup(additivelyOpenedSceneGroup);

                foreach (SceneObject neededAdditivelyOpenedScene in neededAdditivelyOpenedScenesInGroup)
                {
                    if (!existingScenes.Contains(neededAdditivelyOpenedScene))
                    {
                        AdditivelyOpenedSceneGroups.Remove(additivelyOpenedSceneGroup);
                    }
                }
            }
        }

        //----------------

        private Dictionary<string,SceneGroupType> GetScenesWithReflectionByName()
        {
            var definedIn = typeof(SceneChangeService).Assembly.GetName().Name;
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies.Where(a => !a.GlobalAssemblyCache && a.GetName().Name == definedIn))
            {
                try
                {
                    var baseType = typeof(SceneZeitnot);
                    foreach (var type in assembly.GetTypes().Where(t => t != baseType && baseType.IsAssignableFrom(t)))
                    {
                        var scene = (SceneZeitnot)Activator.CreateInstance(type);
                        if (SceneTypes.ContainsKey(scene.Path)) continue;

                        SceneTypes[scene.Path] = scene.SceneGroupType;
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogError($"PseudoSceneManager collect scenes error!\n{exception}");
                }
            }

            // make sure our loading screen is present.
            ///if (!SceneManager.GetSceneByName(ScenesHelper.kSceneLoader).isLoaded)
            ///{
            ///    SceneManager.LoadScene(ScenesHelper.kSceneLoader, LoadSceneMode.Additive);
            ///}

            return SceneTypes;
        }
        
        private Dictionary<string, SceneGroupType> SceneTypes { get; set; }

		public void ActivatePseudoSceneObjects(string sceneName)
        {
            // change active scene for lighting settings.
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }
}