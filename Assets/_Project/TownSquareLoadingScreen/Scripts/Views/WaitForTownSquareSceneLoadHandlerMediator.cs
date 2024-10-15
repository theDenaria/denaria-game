using _Project.SceneManagementUtilities.Signals;
using _Project.TownSquareLoadingScreen.Scripts.Signals;
using strange.extensions.mediation.impl;
using UnityEngine.SceneManagement;

namespace _Project.TownSquareLoadingScreen.Scripts.Views
{
    public class WaitForTownSquareSceneLoadHandlerMediator : Mediator
    {
        [Inject] public WaitForTownSquareSceneLoadHandlerView View { get; set; }
        [Inject] public OnSceneLoadAndUnloadAreCompletedSignal OnSceneLoadAndUnloadAreCompletedSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            //View.onNextScreenReady.AddListener(HandleOnNextScreenReady);
            View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            //View.onNextScreenReady.RemoveListener(HandleOnNextScreenReady);
        }

        private void HandleOnNextScreenReady()
        {
            UnityEngine.Debug.Log("yyy HandleOnNextScreenReady");
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        [ListensTo(typeof(OnSceneLoadAndUnloadAreCompletedSignal))]
        public void OnSceneLoadAndUnloadAreCompleted()
        {
            UnityEngine.Debug.Log("yyy OnSceneLoadAndUnloadAreCompleted");
            View.IsSceneLoadUnloadCompleted = true;
        }
        
        [ListensTo(typeof(OnPlayerSpawnCompletedSignal))]
        public void OnPlayerSpawnCompleted()
        {
            UnityEngine.Debug.Log("yyy OnPlayerSpawnCompleted");
            View.IsPlayerSpawnCompleted = true;
        }
    }
}