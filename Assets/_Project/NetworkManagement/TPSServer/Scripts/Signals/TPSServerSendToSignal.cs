using _Project.NetworkManagement.TPSServer.Scripts.Commands;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.NetworkManagement.TPSServer.Scripts.Signals
{
    public class TPSServerSendMoveSignal : Signal<Vector2> { }
    public class TPSServerSendLookSignal : Signal<Vector4> { }
    public class TPSServerSendFireSignal : Signal<TPSServerSendFireCommandData> { }
    public class TPSServerSendJumpSignal : Signal { }
}