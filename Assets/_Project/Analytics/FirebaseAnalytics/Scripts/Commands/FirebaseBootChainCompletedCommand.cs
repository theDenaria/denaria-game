/*using _Project.Login.Models;
using _Project.Utilities;
using strange.extensions.command.impl;

namespace _Project.Analytics.Commands
{
    public class FirebaseBootChainCompletedCommand : Command
    {
        [Inject] public IGameStartDataProgress GameStartDataProgress { get; set; }
        public override void Execute()
        {
            GameStartDataProgress.InProgressJobs.Remove(Constants.GameStartDataFetcherController.FirebaseProgressKey);
        }
    }
}*/