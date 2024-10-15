using System;

//using PlayFab;
//using PlayFab.ClientModels;

namespace _Project.ServerTimeStamp.Scripts.Services
{
    public class RequestServerTimeStampService : IRequestServerTimeStampService
    {
        private int retryAgain = 0;
        private Action<long, bool> serviceAction;
        public void GetCurrentEpochSecondsFromServer(Action<long, bool> currentEpochSeconds)
        {
            serviceAction = currentEpochSeconds;
            /*PlayFabClientAPI.GetTime(new GetTimeRequest(), onGetTimeSuccess =>
            {
                currentEpochSeconds?.Invoke(Convert.ToInt64(
                    (onGetTimeSuccess.Time - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds), true);
            }, OnGetTimeFail);*/
        }
        
        /*private void OnGetTimeFail(PlayFabError error)
        {
            Debug.Log("Get Current Date Failed: " + error.ToString());
            if (retryAgain < Constants.RETRY_CONNECTION_MAX_ATTEMPT)
            {
                GetCurrentEpochSecondsFromServer(serviceAction);
            }
            else
            {
                Debug.Log("Impossible to fetch server time");
                serviceAction?.Invoke(0, false);
            }
        }*/
    }
}