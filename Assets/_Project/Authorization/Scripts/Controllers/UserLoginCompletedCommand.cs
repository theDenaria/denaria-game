using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using strange.extensions.command.impl;
using CBS.Models;

namespace _Project.Login.Controllers
{
    public class UserLoginCompletedCommand : Command
    {
        [Inject] public CBSLoginResult Result { get; set; }
        [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { private get; set; }

        public override void Execute()
        {
            if (Result.IsSuccess)
            {
                var isNew = Result.IsNew;
                var profileID = Result.ProfileID;
                var playfabLoginResult = Result.Result;
                var sessionTicket = playfabLoginResult.SessionTicket;
                var playfabId = playfabLoginResult.PlayFabId;
                Debug.Log($"SessionTicket: {sessionTicket}, PlayFabId: {playfabId}");
                ChangeSceneGroupSignal.Dispatch(SceneGroupType.TownSquare, new LoadingOptions());

            }
            else
            {
                Debug.Log(Result.Error.Message);
            }


        }
    }
}