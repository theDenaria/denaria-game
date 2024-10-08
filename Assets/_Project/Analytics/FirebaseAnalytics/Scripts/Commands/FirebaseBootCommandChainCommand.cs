/*using _Project.Analytics.Signals;
using _Project.Utilities;
using strange.extensions.command.impl;

namespace _Project.Analytics.Commands
{
    public class FirebaseBootCommandChainCommand : Command
    {
        [Inject] public FirebaseBootCommandChainSignal FirebaseBootCommandChainSignal { get; set; }
        [Inject] public IGameStartDataProgress GameStartDataProgress { get; set; }
        public override void Execute()
        {
            GameStartDataProgress.InProgressJobs.Add(Constants.GameStartDataFetcherController.FirebaseProgressKey);
            FirebaseBootCommandChainSignal.Dispatch();
        }
    }
}*/