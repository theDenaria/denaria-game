using _Project.NetworkManagement.DenariaServer.Scripts.Commands;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.NetworkManagement.DenariaServer.Scripts.Signals
{
    public class DenariaServerSendMoveSignal : Signal<Vector2> { }
    public class DenariaServerSendLookSignal : Signal<Vector4> { }
    public class DenariaServerSendJumpSignal : Signal { }
}