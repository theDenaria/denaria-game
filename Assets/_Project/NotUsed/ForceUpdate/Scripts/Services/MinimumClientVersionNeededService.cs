using System.Threading.Tasks;
using _Project.ForceUpdate.Scripts.TitleData;
using _Project.LoggingAndDebugging;
using _Project.Utilities;
//using CBS;//TODO: Uncomment 20 august

namespace _Project.ForceUpdate.Scripts.Services
{
    public class MinimumClientVersionNeededService : IMinimumClientVersionNeededService
    {
        //private CBSTitleDataModule TitleDataModule { get; set; } = CBSModule.Get<CBSTitleDataModule>();//TODO: Uncomment 20 august
        private MinimumClientVersionNeededTitleData TitleDataCollection { get; set; }
        public async Task GetTitleData(string titleDataKey, System.Action<MinimumClientVersionNeededTitleData> callbackOnComplete)
        {
            //if(TitleDataModule == null) TitleDataModule = CBSModule.Get<CBSTitleDataModule>();//TODO: Uncomment 20 august
            await GetTitleDataTask(titleDataKey);
            callbackOnComplete?.Invoke(TitleDataCollection);
        }

        private async Task GetTitleDataTask(string titleDataKey)
        {
            int tryAgain = 0;
            bool isSuccess = false;
            /*while (tryAgain < Constants.RETRY_CONNECTION_MAX_ATTEMPT && !isSuccess)
            {
                CBSModule.Get<CBSTitleDataModule>().GetTitleDataByKey<MinimumClientVersionNeededTitleData>(titleDataKey,  onGetTitleData =>
                {
                    if (onGetTitleData.IsSuccess)
                    {
                        TitleDataCollection = onGetTitleData.Data;
                        DebugLoggerMuteable.Log("MinimumClientVersionNeededService GetTitleDataTask succeed");
                        isSuccess = true;
                    }
                    else
                    {
                        DebugLoggerMuteable.Log("MinimumClientVersionNeededService GetTitleDataTask: " + onGetTitleData.Error.FabCode);
                    }
                });
                tryAgain++;
                await Task.Delay(Constants.RETRY_CONNECTION_DELAY_MS);
            }*///TODO: Uncomment 20 august

            if (tryAgain >= Constants.RETRY_CONNECTION_MAX_ATTEMPT)
            {
                DebugLoggerMuteable.Log("Max attempt reached on MinimumClientVersionNeededService.GetTitleDataTask");
            }

        }
    }
}