using _Project.WaitingCanvas.Scripts.Enums;
using _Project.WaitingCanvas.Scripts.Signals;
using _Project.WaitingCanvas.Scripts.WaitHandlers;
using Cysharp.Threading.Tasks;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

namespace _Project.WaitingCanvas.Scripts.Views
{
    public class WaitingCanvasMediator : Mediator
    {
        [Inject] public WaitingCanvasView View { get; set; }
        [Inject] public AddWaitHandlerSignal AddWaitHandlerSignal { get; set; }

        private List<IWaitHandler> ActiveWaitHandlers { get; set; } = new List<IWaitHandler>();
        
        public override void OnRegister()
        {
            base.OnRegister();
            AddWaitHandlerSignal.AddListener(OnAddWaitHandler);
            View.InitializeView();
        }

        public override void OnRemove()
        {
            AddWaitHandlerSignal.RemoveListener(OnAddWaitHandler);
            foreach (IWaitHandler waitHandler in ActiveWaitHandlers)
            {
                RemoveWaitHandler(waitHandler);
            }
            ActiveWaitHandlers.Clear();
            View.FinalizeView();
            base.OnRemove();
        }

        private void OnDestroy()
        {
            ActiveWaitHandlers = null;
        }

        private void OnAddWaitHandler(IWaitHandler waitHandler)
        {
            PlayAndWaitForWaitHandler(waitHandler);
            View.AddWaitHandler(waitHandler);
        }

        private void RemoveWaitHandler(IWaitHandler waitHandler)
        {
            ActiveWaitHandlers.Remove(waitHandler);
            if (waitHandler.WaitHandlerState != WaitHandlerStateTypes.Completed)
            {
                waitHandler.Dispose();
            }
            View.RemoveWaitHandler(waitHandler);
        }

        private async UniTask PlayAndWaitForWaitHandler(IWaitHandler waitHandler)
        {
            ActiveWaitHandlers.Add(waitHandler);
            await waitHandler.PlayAndWait();
            RemoveWaitHandler(waitHandler);
        }

    }
}
