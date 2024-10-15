using _Project.NetworkManagement.Scripts.Signals;
using _Project.SceneManagementUtilities.Scripts.Signals;
using strange.extensions.mediation.impl;
using UnityEngine.SceneManagement;

namespace _Project.TownSquareLoadingScreen.Scripts.Views
{
    public class WaitForTownSquareSceneLoadHandlerMediator : Mediator
    {
        [Inject] public WaitForTownSquareSceneLoadHandlerView View { get; set; }
        [Inject] public OnSceneLoadAndUnloadAreCompletedSignal OnSceneLoadAndUnloadAreCompletedSignal { get; set; }
        [Inject] public TownSquareLoadedSignal TownSquareLoadedSignal { get; set; }

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
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        [ListensTo(typeof(OnSceneLoadAndUnloadAreCompletedSignal))]
        public void OnSceneLoadAndUnloadAreCompleted()
        {
            View.IsSceneLoadUnloadCompleted = true;
            TownSquareLoadedSignal.Dispatch();
        }
        
        [ListensTo(typeof(OwnPlayerSpawnedSignal))]
        public void OnPlayerSpawnCompleted()
        {
            View.IsPlayerSpawnCompleted = true;
        }
    }
}