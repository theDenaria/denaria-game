using _Project.LoggingAndDebugging;
using _Project.WaitingCanvas.Scripts.Enums;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace _Project.WaitingCanvas.Scripts.WaitHandlers
{
    public class WaitForRelevantSceneLoadHandler : IWaitHandler
    {
        public WaitHandlerStateTypes WaitHandlerState { get; private set; } = WaitHandlerStateTypes.NotStarted;
        public string Message { get; private set; } = "";
        
        private bool IsCustomInitializationFinished { get; set; } = false;
        private bool IsCustomGameLoadFinished { get; set; } = false;
        
        private float CustomOperationProgress { get; set; } = 0.0f;

        private bool IsCustomOperationLoadProgressCompleted
        {
            get { return CustomOperationProgress >= 1; }
        }        
        
        private bool IsRelevantSceneReady
        {
            get
            { 
                return IsCustomOperationLoadProgressCompleted;
            }
        }
        
        public WaitForRelevantSceneLoadHandler(string message)
        {
            Message = message;
        }
        
        public void Dispose()
        {
            WaitHandlerState = WaitHandlerStateTypes.Disposed;
            Message = null;
        }
        
        public async UniTask PlayAndWait()
        {
            // Reusing a wait handler is not supported yet.
            if (WaitHandlerState != WaitHandlerStateTypes.NotStarted) { return; }

            WaitHandlerState = WaitHandlerStateTypes.Running;

            SubscribeToEventsToWait();

            await UniTask.WaitUntil(() => IsRelevantSceneReady || WaitHandlerState == WaitHandlerStateTypes.Canceled);
            
            UnsubscribeFromEventsToWait();
            
            if (WaitHandlerState != WaitHandlerStateTypes.Canceled)
            {
                WaitHandlerState = WaitHandlerStateTypes.Completed;
            }
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
        
        private void OnCustomLoadingOperationFinished(string customLoadingOperationArgs)
        {
            IsCustomGameLoadFinished = true;
        }
        
        private void OnCustomOperationInitializationFinished()
        {
            IsCustomInitializationFinished = true;
        }
        
        private void OnCustomOperationLoadProgress(float progress)
        {
            CustomOperationProgress = progress;
        }
    }
}