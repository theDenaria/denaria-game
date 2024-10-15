//using _Project.Login.Models;

using _Project.ServerTimeStamp.Scripts.Models;
using _Project.ServerTimeStamp.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.ServerTimeStamp.Scripts.Commands
{
    public class SetTimeStampDifferenceCommand : Command
    {
        [Inject] public SetTimeStampDifferenceCommandData CommandData { get; set; }
        [Inject] public IServerTimeStampModel ServerTimeStampModel { get; set; }
        [Inject] public IRequestServerTimeStampService RequestServerTimeStampService { get; set; }
        //[Inject] public ILoginSucceedDataProgress LoginSucceedDataProgress { get; set; }
        public override void Execute()
        {
            if(CommandData.LoginChain)
                //LoginSucceedDataProgress.InProgressJobs.Add(Constants.LoginSucceedFetcherController.ServerTimeFetchCompleteKey);
            Retain();
            RequestServerTimeStampService.GetCurrentEpochSecondsFromServer(OnGetEpochSeconds);
        }

        private void OnGetEpochSeconds(long epochSeconds, bool isSucceed)
        {
            ServerTimeStampModel.DifferenceInSeconds = (int)(DateUtility.GetCurrentEpochSeconds() - epochSeconds);
            CommandData.IsSucceed?.Invoke(isSucceed);
            if(CommandData.LoginChain)
                //LoginSucceedDataProgress.InProgressJobs.Remove(Constants.LoginSucceedFetcherController.ServerTimeFetchCompleteKey);
            Release();
        }
    }
}