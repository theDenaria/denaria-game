using UnityEngine;
using Unity.Collections;

namespace _Project.NetworkManagement.TPSServer.Scripts.Services
{
    public interface ITPSServerService
    {
        public void Init(int sessionId, string serverEndPoint, ushort serverPort);
        public void ConnectToTPSServer();
        public void DisconnectFromTPSServer();

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
