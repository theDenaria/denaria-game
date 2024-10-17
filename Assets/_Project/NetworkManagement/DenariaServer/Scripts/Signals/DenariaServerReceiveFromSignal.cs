using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.NetworkManagement.DenariaServer.Scripts.Signals
{
    public class DenariaServerReceiveSpawnSignal : Signal<PlayerSpawnCommandData> { }
    public class DenariaServerReceivePositionUpdateSignal : Signal<PlayerPositionUpdateCommandData> { }
    public class DenariaServerReceiveRotationUpdateSignal : Signal<PlayerRotationUpdateCommandData> { }
    public class DenariaServerReceiveDisconnectSignal : Signal<string> { }

}