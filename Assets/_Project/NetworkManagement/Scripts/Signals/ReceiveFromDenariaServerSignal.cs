using _Project.GameSceneManager.Scripts.Controller;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.NetworkManagement.Scripts.Signals
{

    public class ReceivePositionUpdateSignal : Signal<PlayerPositionUpdateCommandData> { }
    public class ReceiveRotationUpdateSignal : Signal<PlayerRotationUpdateCommandData> { }
    public class ReceiveFireSignal : Signal<string, Vector3, Vector3> { }
    public class ReceiveHitSignal : Signal<string, string, Vector3> { }
    public class ReceiveHealthUpdateSignal : Signal<PlayerHealthUpdateCommandData> { }
    public class ReceiveDisconnectSignal : Signal<string> { }

}