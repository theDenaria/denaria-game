using _Project.NetworkManagement.Scripts.Controllers;
using _Project.NetworkManagement.Scripts.Signals;
using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using CBS.Models;
using strange.extensions.command.impl;

namespace _Project.Authorization.Scripts.Controllers
{
    public class UserLoginCompletedCommand : Command
    {
        [Inject]
        public CBSLoginResult Result
        {
            get;
            set;
        }
        [Inject]
        public ChangeSceneGroupSignal ChangeSceneGroupSignal
        {
            private get;
            set;
        }
        private ConnectDenariaServerSignal _connectDenariaServerSignal;

        [Inject]
        public ConnectDenariaServerSignal ConnectDenariaServerSignal
        {
            get
            {
                Debug.Log("UUU Get ConnectDenariaServerSignal");
                return _connectDenariaServerSignal;
            }
            set
            {
                Debug.Log("UUU Set ConnectDenariaServerSignal");
                _connectDenariaServerSignal = value;
            }
        }

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
                Debug.Log("UUU Connecting to denaria server");
                var connectDenariaServerCommandData = new ConnectDenariaServerCommandData("asdasd");
                ConnectDenariaServerSignal.Dispatch();
                Debug.Log("UUU Connected to denaria server");
                ChangeSceneGroupSignal.Dispatch(SceneGroupType.TownSquare, new LoadingOptions());

            }
            else
            {
                Debug.Log(Result.Error.Message);
            }


        }
    }
}