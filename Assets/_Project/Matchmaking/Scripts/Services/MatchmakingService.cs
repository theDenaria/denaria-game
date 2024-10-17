using System.Collections.Generic;
using _Project.Matchmaking.Scripts.Commands;
using _Project.Matchmaking.Scripts.Enums;
using _Project.Matchmaking.Scripts.Signals;
using _Project.PlayerSessionInfo.Scripts.Models;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using _Project.Utilities;
using PimDeWitte.UnityMainThreadDispatcher;
using WebSocketSharp;

namespace _Project.Matchmaking.Scripts.Services
{
    public class MatchmakingService : IMatchmakingService
    {

        private WebSocket _websocket;
        [Inject] public IRoutineRunner RoutineRunner { get; set; }
        [Inject] public IPlayerSessionInfoModel PlayerSessionInfoModel { get; set; }

        [Inject] public QueueStartedSignal QueueStartedSignal { get; set; }
        [Inject] public MatchFoundSignal MatchFoundSignal { get; set; }
        [Inject] public QueueFinishedSignal QueueFinishedSignal { get; set; }
        [Inject] public PlayerReadyMessageReceivedSignal PlayerReadyMessageReceivedSignal { get; set; }
        [Inject] public MatchStartReceivedSignal MatchStartReceivedSignal { get; set; }

        private MatchmakingPlatformEnum _platform = MatchmakingPlatformEnum.None;

        // private string _websocketUrl = "ws://127.0.0.1:3000/queue";
        private string _websocketUrl = "ws://192.168.1.152:3000/queue";

        private int _matchSessionId;

        UnityMainThreadDispatcher _mainThreadDispatcher;

        public void Init()
        {
            _mainThreadDispatcher = UnityMainThreadDispatcher.Instance();
        }

        public void StartMatchmaking(MatchmakingPlatformEnum platform, MatchmakingServiceQueueMode queueMode)
        {
            _platform = platform;
            _websocket = new WebSocket(_websocketUrl);

            _websocket.OnOpen += (sender, e) =>
            {
                SendMatchmakingRequest(queueMode);
            };
            _websocket.OnError += (sender, e) =>
            {
                if (e.Exception != null)
                {
                    Debug.LogException(e.Exception);
                }
            };
            _websocket.OnClose += (sender, e) => Debug.Log("Connection closed!");
            _websocket.OnMessage += (sender, e) =>
            {
                HandleMessage(e.Data);
            };

            _websocket.Connect();
        }

        private void SendMatchmakingRequest(MatchmakingServiceQueueMode queueMode)
        {
            if (_websocket.ReadyState == WebSocketState.Open)
            {
                string command = "JOIN_QUEUE";
                switch (queueMode)
                {
                    case MatchmakingServiceQueueMode._1v1:
                        _websocket.Send(command + "," + PlayerSessionInfoModel.PlayerId + ",1v1," + PlayerSessionInfoModel.SessionTicket);
                        break;
                    case MatchmakingServiceQueueMode._2v2:
                        _websocket.Send(command + "," + PlayerSessionInfoModel.PlayerId + ",2v2," + PlayerSessionInfoModel.SessionTicket);
                        break;
                }
            }
        }

        public void SendPlayerReady()
        {
            if (_websocket.ReadyState == WebSocketState.Open)
            {
                string command = "PLAYER_READY";
                _websocket.Send(command + "," + _matchSessionId);
            }
        }

        public void CancelMatchmaking()
        {
            if (_websocket != null && _websocket.ReadyState == WebSocketState.Open)
            {
                _websocket.Close();
            }
        }
        public void Disconnect()
        {
            CancelMatchmaking();
        }

        private void HandleMessage(string message)
        {
            (string opCode, string remainingMessage) = GetMessageElements(message);

            if (string.IsNullOrEmpty(opCode))
            {
                Debug.LogWarning("Received empty message");
                return;
            }
            _mainThreadDispatcher.Enqueue(() => DispatchOnMainThread(opCode, remainingMessage));
        }

        private void DispatchOnMainThread(string opCode, string remainingMessage)
        {
            switch (opCode)
            {
                case "QUEUE_JOINED":
                    QueueStartedSignal.Dispatch();
                    break;
                case "MATCH_FOUND":
                    MatchFoundInfo matchFoundInfo = JSONUtilityZeitnot.TryDeserializeObject<MatchFoundInfo>(remainingMessage);
                    _matchSessionId = matchFoundInfo.session_id;
                    MatchFoundSignal.Dispatch(new MatchFoundCommandData(matchFoundInfo.session_id, matchFoundInfo.players));
                    break;
                case "PLAYER_READY":
                    PlayerReadyMessageReceivedSignal.Dispatch(remainingMessage);
                    break;
                case "MATCH_START":
                    MatchStartInfo matchStartInfo = JSONUtilityZeitnot.TryDeserializeObject<MatchStartInfo>(remainingMessage);
                    MatchStartReceivedSignal.Dispatch(new ConnectMatchCommandData(matchStartInfo, _platform));
                    break;
                case "QUEUE_FINISHED":
                    QueueFinishedSignal.Dispatch();
                    break;
                default:
                    Debug.LogWarning($"Unrecognized opCode: {opCode}");
                    break;
            }
        }

        private (string, string) GetMessageElements(string message)
        {
            int commaIndex = message.IndexOf(',');
            if (commaIndex >= 0)
            {
                string firstPart = message.Substring(0, commaIndex);
                string secondPart = message.Substring(commaIndex + 1);
                return (firstPart, secondPart);
            }
            else
            {
                // If no comma is found, return the whole message as the first part
                // and an empty string as the second part
                return (message, "");
            }
        }


    }
}

public class MatchFoundInfo
{
    public int session_id;
    public List<Player> players;

    public struct Player
    {
        public string player_id;
        public byte team;

        public Player(string id, byte teamNumber)
        {
            player_id = id;
            team = teamNumber;
        }
    }
}

public class MatchStartInfo
{
    public int session_id;
    public string tps_server_address;
    public ushort tps_server_port;
}