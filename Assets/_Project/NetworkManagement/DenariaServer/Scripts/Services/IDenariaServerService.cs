using UnityEngine;
using Unity.Collections;

namespace _Project.NetworkManagement.DenariaServer.Scripts.Services
{
    public interface IDenariaServerService
    {
        public float TickRate { get; }
        public void Init();
        public void ConnectToDenariaServer();
        public void DisconnectFromDenariaServer();

        public ushort ServerTick { get; }
        public ushort InterpolationTick { get; }
        // --- SEND
        public void StartSendingConnectMessage();
        public void StopSendingConnectMessage();
        public void SendMove(Vector2 moveInput);
        public void SendRotation(Vector4 axisAngles);
        public void SendJump();
        public void SendFire(Vector3 cam_origin, Vector3 direction, Vector3 barrel_origin);
        public void SendUnreliableMessages(NativeArray<byte>[] messages);
        public void SendReliableMessages(NativeArray<byte>[] messages);
    }
}
