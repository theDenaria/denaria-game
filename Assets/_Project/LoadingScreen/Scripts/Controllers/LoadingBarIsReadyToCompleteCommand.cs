//using _Project.Login.Models; //TODO: Uncomment 21 August
using _Project.Utilities;
using Cysharp.Threading.Tasks;
using strange.extensions.command.impl;

namespace _Project.LoadingScreen.Scripts.Controllers
{
    public class LoadingBarIsReadyToCompleteCommand : Command
    {
        [Inject] public CompleteLoadingSignal CompleteLoadingSignal { get; set; }
        //[Inject] public IGameStartDataProgress GameStartDataProgress { get; set; } //TODO: Uncomment 21 August
        private bool IsDataFetchComplete = false;
        public override async void Execute()
        {
            while (!IsDataFetchComplete)
            {
                await ControlDataFetchComplete();
            }
            CompleteLoadingSignal.Dispatch();
        }
        private async UniTask ControlDataFetchComplete()
        {
            await UniTask.Delay(Constants.GameStartDataFetcherController.CheckControllerDelayMS);
            /*if (GameStartDataProgress.InProgressJobs.Count == 0)
            {
                IsDataFetchComplete = true;
            }*///TODO: Uncomment 21 August
        }
    }
}
