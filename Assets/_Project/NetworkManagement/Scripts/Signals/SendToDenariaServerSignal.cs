using _Project.NetworkManagement.Scripts.Controllers;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.NetworkManagement.Scripts.Signals
{
    public class SendMoveSignal : Signal<Vector2> { }
    public class SendLookSignal : Signal<Vector4> { }
    public class SendFireSignal : Signal<SendFireCommandData> { }
    public class SendJumpSignal : Signal { }
}