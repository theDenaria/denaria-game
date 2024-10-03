using _Project.NetworkManagement.Scripts.Models;
using _Project.NetworkManagement.Scripts.Signals;

using UnityEngine;
using Unity.Collections;
using Unity.Networking.Transport;
using System.Text;
using _Project.GameSceneManager.Scripts.Controller;
using _Project.NetworkManagement.Scripts.Enums;
using System;
using _Project.StrangeIOCUtility.Models;
using System.Collections;
namespace _Project.NetworkManagement.Scripts.Services
{
    public interface IDenariaServerService
    {
        public void ConnectToDenariaServer(string playerId);
        public void DisconnectFromDenariaServer();

        // --- SEND
        public void SendConnectMessage();
        public void SendMove(Vector2 moveInput);
        public void SendRotation(Vector4 axisAngles);
        public void SendJump();
        public void SendFire(Vector3 cam_origin, Vector3 direction, Vector3 barrel_origin);
        public void SendUnreliableMessages(NativeArray<byte>[] messages);
        public void SendReliableMessages(NativeArray<byte>[] messages);

        // --- RECEIVE 
        public void ReceiveMessages();
    }
}
