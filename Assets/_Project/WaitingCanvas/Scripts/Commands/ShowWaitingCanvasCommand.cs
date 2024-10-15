using _Project.WaitingCanvas.Scripts.Signals;
using _Project.WaitingCanvas.Scripts.WaitHandlers;
using strange.extensions.command.impl;

namespace _Project.WaitingCanvas.Scripts.Commands
{
    public class ShowWaitingCanvasCommand : Command
    {
        [Inject] public IWaitHandler WaitHandler { get; set; }
        [Inject] public AddWaitHandlerSignal AddWaitHandlerSignal { get; set; }

        public override void Execute()
        {
            AddWaitHandlerSignal.Dispatch(WaitHandler);
        }
    }
}
