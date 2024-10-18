using _Project.NetworkManagement.TPSServer.Scripts.Signals;

using UnityEngine;
using Unity.Collections;
using Unity.Networking.Transport;
using System.Text;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Controller;
using _Project.NetworkManagement.TPSServer.Scripts.Enums;
using System;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using System.Collections;
using _Project.SceneManagementUtilities.Scripts.Signals;
using _Project.PlayerSessionInfo.Scripts.Models;

namespace _Project.NetworkManagement.TPSServer.Scripts.Services
{
    public class TPSServerService : ITPSServerService
    {
        [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }

        [Inject] public TPSServerConnectSuccessSignal TPSServerConnectSuccessSignal { get; set; }

        [Inject] public TPSServerReceiveSpawnSignal ReceiveSpawnSignal { get; set; }
        [Inject] public TPSServerReceivePositionUpdateSignal ReceivePositionUpdateSignal { get; set; }
        [Inject] public TPSServerReceiveRotationUpdateSignal ReceiveRotationUpdateSignal { get; set; }
        [Inject] public TPSServerReceiveFireSignal ReceiveFireSignal { get; set; }
        [Inject] public TPSServerReceiveHitSignal ReceiveHitSignal { get; set; }
        [Inject] public TPSServerReceiveHealthUpdateSignal ReceiveHealthUpdateSignal { get; set; }
        [Inject] public TPSServerReceiveDisconnectSignal ReceiveDisconnectSignal { get; set; }
        [Inject] public IRoutineRunner RoutineRunner { get; set; }
        [Inject] public IPlayerSessionInfoModel PlayerSessionInfoModel { get; set; }

        public NetworkDriver NetworkDriver { get; set; }
        public NetworkConnection NetworkConnection { get; set; }
        public NetworkPipeline ReliablePipeline { get; set; }

        public bool IsConnectionAccepted { get; set; }

        private bool _isListeningTPSServer = false;

        private float interval = 1.0f / 30.0f; // 30 times per second
        private float nextExecutionTime;

        private NetworkEndpoint _tpsEndpoint;

        private int _sessionId;

        private Coroutine _listenCoroutine;

        private bool _isSendingConnectMessage = false;

        public void Init(int sessionId, string serverEndPoint, ushort serverPort)
        {
            _sessionId = sessionId;
            _tpsEndpoint = NetworkEndpoint.Parse(serverEndPoint, serverPort);
            IsConnectionAccepted = false;
            NetworkDriver = NetworkDriver.Create();
            ReliablePipeline = NetworkDriver.CreatePipeline(typeof(ReliableSequencedPipelineStage));
            StartListeningTPSServer();
        }

        public void StartSendingConnectMessage()
        {
            _isSendingConnectMessage = true;
            RoutineRunner.StartCoroutine(SendConnectMessageCoroutine());
        }

        public void StopSendingConnectMessage()
        {
            _isSendingConnectMessage = false;
        }

        IEnumerator SendConnectMessageCoroutine()
        {
            while (_isSendingConnectMessage)
            {
                SendConnectMessage();
                yield return new WaitForSeconds(0.5f);
            }
        }


        public void ConnectToTPSServer()
        {
            NetworkConnection = NetworkDriver.Connect(_tpsEndpoint);
        }

        public void DisconnectFromTPSServer()
        {
            NetworkConnection.Disconnect(NetworkDriver);
            IsConnectionAccepted = false;
            _isListeningTPSServer = false;
            StopListeningTPSServer();
        }

        private void StartListeningTPSServer()
        {
            _listenCoroutine = RoutineRunner.StartCoroutine(StartListeningTPSServerCoroutine());
        }

        private void StopListeningTPSServer()
        {
            RoutineRunner.StopCoroutine(_listenCoroutine);
        }

        // Run on every tick (1/30 seconds for TPSServer)
        private IEnumerator StartListeningTPSServerCoroutine()
        {
            _isListeningTPSServer = true;
            while (_isListeningTPSServer)
            {
                nextExecutionTime = Time.realtimeSinceStartup + interval;
                ReceiveMessages();

                float waitTime = nextExecutionTime - Time.realtimeSinceStartup;
                if (waitTime > 0)
                {
                    yield return new WaitForSecondsRealtime(waitTime);
                }

                // yield return new WaitForSeconds(interval);
            }
        }

        // --- SEND

        public void SendConnectMessage()
        {
            NativeArray<byte>[] messages = new NativeArray<byte>[1];
            messages[0] = CreatePlayerConnectMessage();
            SendUnreliableMessages(messages);
        }

        public void SendMove(Vector2 moveInput)
        {
            NativeArray<byte>[] messages = new NativeArray<byte>[1];
            messages[0] = CreateMoveMessage(moveInput);
            SendUnreliableMessages(messages);
        }
        public void SendRotation(Vector4 axisAngles)
        {
            NativeArray<byte>[] messages = new NativeArray<byte>[1];
            messages[0] = CreateRotationMessage(axisAngles);
            SendUnreliableMessages(messages);
        }

        public void SendJump()
        {
            NativeArray<byte>[] messages = new NativeArray<byte>[1];
            messages[0] = CreateJumpMessage();
            SendUnreliableMessages(messages);
        }

        public void SendFire(Vector3 cam_origin, Vector3 direction, Vector3 barrel_origin)
        {
            NativeArray<byte>[] messages = new NativeArray<byte>[1];
            messages[0] = CreateFireMessage(cam_origin, direction, barrel_origin);
            SendUnreliableMessages(messages);
        }

        public void SendUnreliableMessages(NativeArray<byte>[] messages)
        {
            NetworkDriver.BeginSend(NetworkConnection, out var writer);
            writer.WriteUShort((ushort)messages.Length);
            foreach (var message in messages)
            {
                writer.WriteUShort((ushort)message.Length);
                writer.WriteBytes(message);
            }
            NetworkDriver.EndSend(writer);
        }
        public void SendReliableMessages(NativeArray<byte>[] messages)
        {
            NetworkDriver.BeginSend(
                ReliablePipeline,
                NetworkConnection,
                out var writer);
            writer.WriteUShort((ushort)messages.Length);
            foreach (var message in messages)
            {
                writer.WriteUShort((ushort)message.Length);
                writer.WriteBytes(message);
            }
            NetworkDriver.EndSend(writer);
        }





        // --- RECEIVE 


        private void ReceiveMessages()
        {
            NetworkDriver.ScheduleUpdate().Complete();
            NetworkEvent.Type cmd;
            while ((cmd = NetworkConnection.PopEvent(NetworkDriver, out DataStreamReader stream, out var receivePipeline)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Connect && !IsConnectionAccepted)
                {
                    IsConnectionAccepted = true;
                    TPSServerConnectSuccessSignal.Dispatch();
                }
                else if (cmd == NetworkEvent.Type.Data)
                {
                    if (receivePipeline.Equals(ReliablePipeline))
                    {
                        ProcessReliableData(ref stream);
                    }
                    else
                    {
                        ProcessUnreliableData(ref stream);
                    }
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    IsConnectionAccepted = false;
                    NetworkConnection = default;
                }
            }
        }

        private void ProcessReliableData(ref DataStreamReader stream)
        {
            byte[] data = new byte[stream.Length];
            for (int i = 0; i < stream.Length; i++)
            {
                data[i] = stream.ReadByte();
            }

            NativeArray<byte> copiedData = new(data, Allocator.Temp);
            var reader = new DataStreamReader(copiedData);

            while (reader.GetBytesRead() < reader.Length)
            {
                ushort messagesLength = reader.ReadUShort();
                for (ushort i = 0; i < messagesLength; i++)
                {
                    ulong messageId = reader.ReadULong();
                    ulong messageLength = reader.ReadUShort();

                    NativeArray<byte> messageBytes = new((int)messageLength, Allocator.Temp);
                    reader.ReadBytes(messageBytes);
                    DataStreamReader messageStream = new(messageBytes);

                    byte messageType = messageStream.ReadByte();
                    switch (messageType)
                    {
                        // case 0:
                        //     ProcessLevelObjects(ref messageStream);
                        //     break;
                        case 3: // Fire Update
                            ProcessFireUpdate(ref messageStream);
                            break;
                        case 4: // Hit Update
                            ProcessHitUpdate(ref messageStream);
                            break;
                        case 6: // Health Update
                            ProcessHealthUpdate(ref messageStream);
                            break;
                        case 0: // Spawn Update
                            ProcessSpawnUpdate(ref messageStream);
                            break;
                        case 10:
                            ProcessDisconnectUpdate(ref messageStream);
                            break;
                        case 1: // Location Update
                            Debug.Log("WARNING! Location message sent in unreliable channel!");
                            break;
                        case 2: // Location Update
                            Debug.Log("WARNING! Rotation message sent in unreliable channel!");
                            break;
                    }
                }
            }
        }

        private void ProcessUnreliableData(ref DataStreamReader stream)
        {
            var reader = stream;
            while (reader.GetBytesRead() < reader.Length)
            {
                ushort messagesLength = reader.ReadUShort();
                for (ushort i = 0; i < messagesLength; i++)
                {
                    ulong messageLength = reader.ReadUShort();
                    NativeArray<byte> messageBytes = new((int)messageLength, Allocator.Temp);
                    reader.ReadBytes(messageBytes);
                    DataStreamReader messageStream = new(messageBytes);
                    messageBytes.Dispose();

                    byte messageType = messageStream.ReadByte();
                    switch (messageType)
                    {
                        case 1: // Position Update
                            ProcessPositionMessage(ref messageStream);
                            break;
                        case 2: // Location Update
                            ProcessRotationMessage(ref messageStream);
                            break;
                        case 0: // Spawn Update
                            Debug.Log("WARNING! Spawn message sent in unreliable channel!");
                            break;
                        case 10: // Disconnect
                            Debug.Log("WARNING! Disconnect message sent in unreliable channel!");
                            break;
                    }
                }
            }
        }

        private void ProcessPositionMessage(ref DataStreamReader reader)
        {
            ulong playerNum = reader.ReadULong();
            for (ulong i = 0; i < playerNum; i++)
            {
                NativeArray<byte> stringBytes = new(16, Allocator.Temp);
                reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

                string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
                stringBytes.Dispose();

                float x = reader.ReadFloat();
                float y = reader.ReadFloat();
                float z = reader.ReadFloat();

                Vector3 newPosition = new(x, y, z);
                ReceivePositionUpdateSignal.Dispatch(new PlayerPositionUpdateCommandData(playerId, newPosition));

            }
        }

        private void ProcessRotationMessage(ref DataStreamReader reader)
        {
            ulong playerNum = reader.ReadULong();
            for (ulong i = 0; i < playerNum; i++)
            {
                NativeArray<byte> stringBytes = new(16, Allocator.Temp);
                reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

                string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
                stringBytes.Dispose();

                float x = reader.ReadFloat();
                float y = reader.ReadFloat();
                float z = reader.ReadFloat();
                float w = reader.ReadFloat();

                Vector4 newRotation = new(x, y, z, w);

                ReceiveRotationUpdateSignal.Dispatch(new PlayerRotationUpdateCommandData(playerId, newRotation));
            }
        }

        private void ProcessSpawnUpdate(ref DataStreamReader reader)
        {
            ulong playerNum = reader.ReadULong();
            for (ulong i = 0; i < playerNum; i++)
            {
                NativeArray<byte> stringBytes = new(16, Allocator.Temp);
                reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

                string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
                stringBytes.Dispose();

                float position_x = reader.ReadFloat();
                float position_y = reader.ReadFloat();
                float position_z = reader.ReadFloat();
                float rotation_x = reader.ReadFloat();
                float rotation_y = reader.ReadFloat();
                float rotation_z = reader.ReadFloat();
                float rotation_w = reader.ReadFloat();

                Vector3 position = new(position_x, position_y, position_z);
                Vector4 rotation = new(rotation_x, rotation_y, rotation_z, rotation_w);

                ReceiveSpawnSignal.Dispatch(new PlayerSpawnCommandData(playerId, position, rotation));
            }
        }



        private void ProcessFireUpdate(ref DataStreamReader reader)
        {
            NativeArray<byte> stringBytes = new(16, Allocator.Temp);
            reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

            string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
            stringBytes.Dispose();

            float origin_x = reader.ReadFloat();
            float origin_y = reader.ReadFloat();
            float origin_z = reader.ReadFloat();

            float direction_x = reader.ReadFloat();
            float direction_y = reader.ReadFloat();
            float direction_z = reader.ReadFloat();

            Vector3 origin = new(origin_x, origin_y, origin_z);
            Vector3 direction = new(direction_x, direction_y, direction_z);

            // TODO ReceiveFireSignal.Dispatch(playerId, origin, direction);
        }

        private void ProcessHitUpdate(ref DataStreamReader reader)
        {
            NativeArray<byte> stringBytes = new(16, Allocator.Temp);
            reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

            string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
            stringBytes.Dispose();

            stringBytes = new(16, Allocator.Temp);
            reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

            string targetId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
            stringBytes.Dispose();

            float x = reader.ReadFloat();
            float y = reader.ReadFloat();
            float z = reader.ReadFloat();

            Vector3 hitPoint = new(x, y, z);

            // TODOReceiveHitSignal.Dispatch(playerId, targetId, hitPoint);
        }

        private void ProcessHealthUpdate(ref DataStreamReader reader)
        {
            ulong playerNum = reader.ReadULong();
            for (ulong i = 0; i < playerNum; i++)
            {
                NativeArray<byte> stringBytes = new(16, Allocator.Temp);
                reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

                string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
                stringBytes.Dispose();

                float hp = reader.ReadFloat();

                ReceiveHealthUpdateSignal.Dispatch(new PlayerHealthUpdateCommandData(playerId, hp));
            }
        }

        private void ProcessDisconnectUpdate(ref DataStreamReader reader)
        {
            ulong playerNum = reader.ReadULong();
            for (ulong i = 0; i < playerNum; i++)
            {
                NativeArray<byte> stringBytes = new(16, Allocator.Temp);
                reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

                string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
                stringBytes.Dispose();

                ReceiveDisconnectSignal.Dispatch(playerId);
            }
        }

        // --- CREATE MESSAGES
        NativeArray<byte> CreateMoveMessage(Vector2 movement)
        {
            byte eventType = 2;  // EventIn Type 2 for Move
            byte[] data = new byte[8];
            Buffer.BlockCopy(BitConverter.GetBytes(movement.x), 0, data, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(movement.y), 0, data, 4, 4);

            NativeArray<byte> messagePacket = AddEventHeader(eventType, data);
            return messagePacket;
        }

        NativeArray<byte> CreateRotationMessage(Vector4 axisAngles)
        {
            byte eventType = 3;  // EventIn Type 3 for Rotation
            byte[] data = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(axisAngles.x), 0, data, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(axisAngles.y), 0, data, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(axisAngles.z), 0, data, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(axisAngles.w), 0, data, 12, 4);

            NativeArray<byte> messagePacket = AddEventHeader(eventType, data);
            return messagePacket;
        }

        NativeArray<byte> CreateJumpMessage()
        {
            byte eventType = 4;  // EventIn Type 3 for Rotation
            byte[] data = new byte[0];
            NativeArray<byte> messagePacket = AddEventHeader(eventType, data);
            return messagePacket;
        }

        NativeArray<byte> CreateFireMessage(Vector3 cam_origin, Vector3 direction, Vector3 barrel_origin)
        {
            byte eventType = 5;  // EventIn Type 5 for Fire
            byte[] data = new byte[36];
            Buffer.BlockCopy(BitConverter.GetBytes(cam_origin.x), 0, data, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(cam_origin.y), 0, data, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(cam_origin.z), 0, data, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(direction.x), 0, data, 12, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(direction.y), 0, data, 16, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(direction.z), 0, data, 20, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(barrel_origin.x), 0, data, 24, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(barrel_origin.y), 0, data, 28, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(barrel_origin.z), 0, data, 32, 4);

            NativeArray<byte> messagePacket = AddEventHeader(eventType, data);
            return messagePacket;
        }

        NativeArray<byte> CreatePlayerConnectMessage()
        {
            byte messageType = (byte)TPSServerSendOpCode.Connect;

            byte[] playerIdBytes = Encoding.UTF8.GetBytes(PlayerSessionInfoModel.PlayerId);
            byte[] sessionTicketBytes = Encoding.UTF8.GetBytes(PlayerSessionInfoModel.SessionTicket);


            byte[] connectMessageBodyBytes = new byte[16 + sessionTicketBytes.Length];

            Buffer.BlockCopy(playerIdBytes, 0, connectMessageBodyBytes, 0, playerIdBytes.Length);
            Buffer.BlockCopy(sessionTicketBytes, 0, connectMessageBodyBytes, 16, sessionTicketBytes.Length);

            NativeArray<byte> messagePacket = AddEventHeader(messageType, connectMessageBodyBytes);
            return messagePacket;
        }

        NativeArray<byte> AddEventHeader(byte opCode, byte[] data)
        {
            NativeArray<byte> messagePacket = new(1 + data.Length, Allocator.Temp);
            messagePacket[0] = opCode;
            if (data.Length > 0)
            {
                NativeArray<byte> nativeData = new(data.Length, Allocator.Temp);
                for (int i = 0; i < data.Length; i++)
                {
                    nativeData[i] = data[i];
                }

                NativeArray<byte>.Copy(nativeData, 0, messagePacket, 1, nativeData.Length);
                nativeData.Dispose();
            }
            return messagePacket;
        }
    }
}
