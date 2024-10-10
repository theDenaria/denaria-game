using _Project.ABTesting.Scripts.Models;
using _Project.LoggingAndDebugging;
using strange.extensions.command.impl;

namespace _Project.ABTesting.Scripts.Commands
{
    public class PlayfabIdReceivedCommand : Command
    {
        [Inject] public string PlayfabIdReceivedSignalData { get; set; }
        [Inject] public IPlayfabIdModel PlayfabIdModel { get; set; }

        public override void Execute()
        {
            PlayfabIdModel.PlayfabId = PlayfabIdReceivedSignalData;
            //Firebase.Analytics.FirebaseAnalytics.SetUserProperty("playfab_id", PlayfabIdModel.PlayfabId);
            DebugLoggerMuteable.Log("playfab_id is set to user_properties as: " + PlayfabIdModel.PlayfabId);
        }
    }
}