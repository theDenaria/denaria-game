using _Project.GameSceneManager.TPSSceneManager.Scripts.Controller;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.NetworkManagement.TPSServer.Scripts.Signals
{
    public class TPSServerReceiveSpawnSignal : Signal<PlayerSpawnCommandData> { }
    public class TPSServerReceivePositionUpdateSignal : Signal<PlayerPositionUpdateCommandData> { }
    public class TPSServerReceiveRotationUpdateSignal : Signal<PlayerRotationUpdateCommandData> { }
    public class TPSServerReceiveFireSignal : Signal<string, Vector3, Vector3> { }
    public class TPSServerReceiveHitSignal : Signal<string, string, Vector3> { }
    public class TPSServerReceiveHealthUpdateSignal : Signal<PlayerHealthUpdateCommandData> { }
    public class TPSServerReceiveDisconnectSignal : Signal<string> { }

}