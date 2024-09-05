using System.Collections.Generic;
using System.Linq;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities.Services;
using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Command = strange.extensions.command.impl.Command;

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

namespace _Project.SceneManagementUtilities.Controllers
{
    public class ChangeSceneGroupCommand : Command
    {
        [Inject] public ISceneChangeService SceneChangeService { get; set; }
        [Inject] public SceneGroupType SceneGroupType { get; set; }
        [Inject] public LoadingOptions LoadingOptions { get; set; }
        //public LoadingOptions LoadingOptions { get; set; } = new(false,true);
        public bool HasUnloadStarted { get; set; }
        public bool FirstNewSceneLoaded { get; set; }

        private int _loadOperationCount;
        public int LoadOperationCount
        {
            get { return _loadOperationCount; }
            set
            {
                if (_loadOperationCount > value && value == 0)
                {
                    _loadOperationCount = value;
                    DebugLoggerMuteable.Log("ooo _loadOperationCount hit 0");
                    CheckIfSceneLoadAndUnloadAreCompleted();
                }
                else
                {
                    _loadOperationCount = value;
                }
            }
        }

        private int _unloadOperationCount;
        public int UnloadOperationCount
        {
            get { return _unloadOperationCount; }
            set
            {
                if (_unloadOperationCount > value && value == 0)
                {
                    _unloadOperationCount = value;
                    DebugLoggerMuteable.Log("ooo _unloadOperationCount hit 0");
                    CheckIfSceneLoadAndUnloadAreCompleted();
                }
                else
                {
                    _unloadOperationCount = value;
                }
            }
        }

        public override void Execute()
        {
            ChangeIntoNewSceneGroup(SceneGroupType);
        }
        
        private void ChangeIntoNewSceneGroup(SceneGroupType newSceneGroupType)
        {
            //if requested scene group is already loaded just return.
            if (SceneChangeService.CurrentSceneGroupType == newSceneGroupType && !LoadingOptions.WillReloadExistingScenes) return;

            SceneChangeService.IsLoading = true;
            SceneGroupType oldSceneGroupType = SceneChangeService.CurrentSceneGroupType;
            
            ChangeScenes(oldSceneGroupType, newSceneGroupType, LoadingOptions);
            
            SceneChangeService.CurrentSceneGroupType = newSceneGroupType;
        }
        
        private void ChangeScenes(SceneGroupType oldSceneGroupType, SceneGroupType newSceneGroupType, LoadingOptions loadingOptions)
        {
            LoadLoadingScene(loadingOptions.WillUseLoadingScreen);
            
            List<SceneObject> scenesToUnload = GetScenesToUnload(newSceneGroupType, LoadingOptions.WillReloadExistingScenes);
            List<SceneObject> scenesToLoad = GetScenesToLoad(oldSceneGroupType, newSceneGroupType, LoadingOptions.WillReloadExistingScenes);

            List<string> sceneNamesToUnload = SceneChangeService.GetSceneNameListBySceneList(scenesToUnload);
            List<string> sceneNamesToLoad = SceneChangeService.GetSceneNameListBySceneList(scenesToLoad);

            foreach (string sceneName in sceneNamesToLoad)
            { 
                LoadSceneAsynchronously(newSceneGroupType, oldSceneGroupType, sceneName, sceneNamesToLoad, sceneNamesToUnload);
            }
        }
        
        private void LoadLoadingScene(bool willUseLoadingScreen)
        {
            if (willUseLoadingScreen)
            {
                SceneManager.LoadSceneAsync("DefaultLoadingScene", LoadSceneMode.Additive);
            }
        }
        
        private List<SceneObject> GetScenesToUnload(SceneGroupType newSceneGroupType, bool loadingChoicesWillReloadExistingScenes)
        {
            List<SceneObject> scenesToUnload = new List<SceneObject>();
            List<SceneObject> existingScenes = SceneChangeService.GetAllLoadedScenes();
            
            if (loadingChoicesWillReloadExistingScenes)
            {
                return existingScenes;
            }
            
            List<SceneObject> scenesInNewSceneGroup = SceneChangeService.GetScenesByGroup(newSceneGroupType);
            scenesToUnload = existingScenes.Except(scenesInNewSceneGroup).ToList();
            
            return scenesToUnload;
        }

        private List<SceneObject> GetScenesToLoad(SceneGroupType oldSceneGroupType, 
            SceneGroupType newSceneGroupType, 
            bool loadingChoicesWillReloadExistingScenes)
        {
            List<SceneObject> scenesToLoad = new List<SceneObject>();
            List<SceneObject> existingScenes = SceneChangeService.GetAllLoadedScenes();

            if (loadingChoicesWillReloadExistingScenes)
            {
                return SceneChangeService.GetScenesByGroup(newSceneGroupType);
            }
            
            List<SceneObject> scenesInNewSceneGroup = SceneChangeService.GetScenesByGroup(newSceneGroupType);
            scenesToLoad = scenesInNewSceneGroup.Except(existingScenes).ToList();
            
            return scenesToLoad;
        }

        private int LoadSceneAsynchronously(SceneGroupType newSceneGroupType, SceneGroupType oldSceneGroupType,  string sceneName,
            List<string> sceneNamesToLoad, List<string> sceneNamesToUnload)
        {
            LoadOperationCount = sceneNamesToLoad.Count;

            if (SceneChangeService.SceneIsLoaded(sceneName)) return LoadOperationCount;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncOperation.completed += ao =>
            {
                LoadOperationCount = HandleAdditiveSceneLoaded(newSceneGroupType, oldSceneGroupType, sceneNamesToLoad,
                    sceneNamesToUnload);
            };

            return LoadOperationCount;
        }
        
        private int HandleAdditiveSceneLoaded(SceneGroupType newSceneGroupType, SceneGroupType oldSceneGroupType, List<string> sceneNamesToLoad,
            List<string> sceneNamesToUnload)
        {
            LoadOperationCount--;

            FocusOnANewScene(newSceneGroupType);

            UnloadSceneAsynchronously(sceneNamesToUnload);

            return LoadOperationCount;
        }

        private void FocusOnANewScene(SceneGroupType newSceneGroupType)
        {
            FocusFirstNewScene(newSceneGroupType);
            FocusRealFocusedSceneIfLoaded(newSceneGroupType);
        }
        
        private int UnloadSceneAsynchronously(List<string> sceneNamesToUnload)
        {
            if (HasUnloadStarted) { return 0; }
            
            HasUnloadStarted = true;

            UnloadOperationCount = sceneNamesToUnload.Count;

            foreach (string sceneNameToUnload in sceneNamesToUnload)
            {
                if (SceneManager.GetSceneByName(sceneNameToUnload).isLoaded == false)
                {
                    DecreaseUnloadOperationCount();
                    continue;
                }

                AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneNameToUnload);
                asyncOperation.completed += ao =>
                {
                    DecreaseUnloadOperationCount();
                };
            }

            SceneChangeService.ClearAdditivelyOpenedSceneGroups();

            return UnloadOperationCount;
        }

        private void FocusFirstNewScene(SceneGroupType newSceneGroupType)
        {
            if (!FirstNewSceneLoaded)
            {
                FirstNewSceneLoaded = true;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    if (SceneChangeService.GetScenesByGroup(newSceneGroupType).Contains(SceneManager.GetSceneAt(i).name))
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneAt(i));
                    }
                }
            }
        }
        
        private void FocusRealFocusedSceneIfLoaded(SceneGroupType newSceneGroupType)
        {
            if (SceneChangeService.SceneIsLoaded(SceneChangeService.GetFocusedSceneByGroup(newSceneGroupType)))
            {
                Scene focusedScene =
                    SceneManager.GetSceneByName(SceneChangeService.GetFocusedSceneByGroup(newSceneGroupType).SceneName);
                SceneManager.SetActiveScene(focusedScene);
            }
        }
        
        private int DecreaseUnloadOperationCount()
        {
            UnloadOperationCount--;

            return UnloadOperationCount;        
        }
        
        private void CheckIfSceneLoadAndUnloadAreCompleted()
        {
            if (UnloadOperationCount == 0 && LoadOperationCount == 0)
            {
                UnloadLoadingScene();
                SceneChangeService.IsLoading = false;
            }
        }

        private void UnloadLoadingScene()
        {
            if (!SceneManager.GetSceneByName("DefaultLoadingScene").IsValid()) { return; }
            SceneManager.UnloadSceneAsync("DefaultLoadingScene");
        }
        
        /*private void UnloadScenes(List<string> sceneNames)
        {
            foreach (var scene in sceneNames)
            {
                if (!SceneChangeService.SceneIsLoaded(scene)) continue;

                SceneManager.UnloadSceneAsync(scene);
        }*/
        
    }
}