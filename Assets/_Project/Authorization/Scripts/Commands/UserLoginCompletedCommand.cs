using _Project.NetworkManagement.Scripts.Commands;
using _Project.NetworkManagement.Scripts.Signals;
using _Project.SceneManagementUtilities.Scripts.Signals;
using CBS.Models;
using strange.extensions.command.impl;

namespace _Project.Authorization.Scripts.Commands
{
    public class UserLoginCompletedCommand : Command
    {
        [Inject] public CBSLoginResult Result { get; set; }
        [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { private get; set; }

        [Inject] public ConnectDenariaServerSignal ConnectDenariaServerSignal { get; set; }

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
                var connectDenariaServerCommandData = new ConnectDenariaServerCommandData(playfabIdShort, sessionTicket);
                ConnectDenariaServerSignal.Dispatch(connectDenariaServerCommandData);
            }
            else
            {
                Debug.Log(Result.Error.Message);
            }


        }
    }
}