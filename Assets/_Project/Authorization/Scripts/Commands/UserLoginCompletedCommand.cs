using _Project.NetworkManagement.DenariaServer.Scripts.Signals;
using _Project.PlayerSessionInfo.Scripts.Models;
using _Project.SceneManagementUtilities.Scripts.Signals;
using CBS.Models;
using strange.extensions.command.impl;

namespace _Project.Authorization.Scripts.Commands
{
    public class UserLoginCompletedCommand : Command
    {
        [Inject] public CBSLoginResult Result { get; set; }
        [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { private get; set; }

        [Inject] public IPlayerSessionInfoModel PlayerSessionInfoModel { get; set; }

        [Inject] public DenariaServerConnectSignal DenariaServerConnectSignal { get; set; }

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

                // TODO add session ticket to signal
                // Concat playfabid to be maximum 16 characters
                var playfabIdShort = playfabId[..16];
                PlayerSessionInfoModel.Init(playfabIdShort, sessionTicket);
                DenariaServerConnectSignal.Dispatch();
                // ChangeSceneGroupSignal.Dispatch(SceneGroupType.TownSquare, new LoadingOptions());

            }
            else
            {
                Debug.Log(Result.Error.Message);
            }


        }
    }
}