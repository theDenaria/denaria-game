using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.WaitingCanvas.Scripts.Enums;
using _Project.WaitingCanvas.Scripts.WaitHandlers;
using Cysharp.Threading.Tasks;
using strange.extensions.signal.impl;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace _Project.TownSquareLoadingScreen.Scripts.Views
{
    public class WaitForTownSquareSceneLoadHandlerView : ViewZeitnot, IWaitHandler
    {
        public WaitHandlerStateTypes WaitHandlerState { get; private set; } = WaitHandlerStateTypes.NotStarted;
        public string Message { get; private set; } = "";
        
        internal bool IsPlayerSpawnCompleted { get; set; } = false;
        internal bool IsSceneLoadUnloadCompleted { get; set; } = false;
        
        private float CustomOperationProgress { get; set; } = 0.0f;
        
        internal bool IsNextScreenReady
        {
            get
            { 
                return IsPlayerSpawnCompleted && IsSceneLoadUnloadCompleted;
            }
        }
        
        public void SetMessage(string message)
        {
            Message = message;
        }

        public void Init()
        {
            StartWaiting();
        }

        public async void StartWaiting()
        {
            await PlayAndWait();
        }
        
        public async UniTask PlayAndWait()
        {
            UnityEngine.Debug.Log("yyy WaitEntered");

            //Reusing a wait handler is not supported yet.
            if (WaitHandlerState != WaitHandlerStateTypes.NotStarted) { return; }

            WaitHandlerState = WaitHandlerStateTypes.Running;

            //SubscribeToEventsToWait();

            await UniTask.WaitUntil(() => IsNextScreenReady || WaitHandlerState == WaitHandlerStateTypes.Canceled);
            
            //UnsubscribeFromEventsToWait();
            
            if (WaitHandlerState != WaitHandlerStateTypes.Canceled)
            {
                WaitHandlerState = WaitHandlerStateTypes.Completed;
            }
            
            UnityEngine.Debug.Log("yyy WaitEnded");
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
        
        public void Dispose()
        {
            WaitHandlerState = WaitHandlerStateTypes.Disposed;
            Message = null;
        }

        private void SubscribeToEventsToWait()
        {
        }

        private void UnsubscribeFromEventsToWait()
        {
        }
        
        public void Cancel()
        {
            if (WaitHandlerState == WaitHandlerStateTypes.Completed || WaitHandlerState == WaitHandlerStateTypes.Disposed)
            {
                return;
            }
            WaitHandlerState = WaitHandlerStateTypes.Canceled;
        }
        
        private void OnCustomOperationLoadProgress(float progress)
        {
            CustomOperationProgress = progress;
        }
        
        private void OnCustomLoadingOperationFinished()
        {
            IsPlayerSpawnCompleted = true;
        }
    }
}