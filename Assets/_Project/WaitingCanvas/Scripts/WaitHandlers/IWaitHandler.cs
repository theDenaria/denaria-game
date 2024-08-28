using _Project.WaitingCanvas.Scripts.Enums;
using Cysharp.Threading.Tasks;
using System;

namespace _Project.WaitingCanvas.Scripts.WaitHandlers
{
    public interface IWaitHandler : IDisposable
    {
        /// <summary>
        ///     Current state of the wait handler.
        /// </summary>
        WaitHandlerStateTypes WaitHandlerState { get; }
        
        /// <summary>
        ///     Message text to show on WaitingCanvas UI
        /// </summary>
        string Message { get; }

        UniTask PlayAndWait();
        void Cancel();
    }
}
