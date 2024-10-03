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
    public class DenariaServerService
    {
        [Inject] public INetworkManagerModel NetworkManagerModel { get; set; }
        [Inject] public ReceivePositionUpdateSignal ReceivePositionUpdateSignal { get; set; }
        [Inject] public ReceiveRotationUpdateSignal ReceiveRotationUpdateSignal { get; set; }
        [Inject] public ReceiveFireSignal ReceiveFireSignal { get; set; }
        [Inject] public ReceiveHitSignal ReceiveHitSignal { get; set; }
        [Inject] public ReceiveHealthUpdateSignal ReceiveHealthUpdateSignal { get; set; }
        [Inject] public ReceiveDisconnectSignal ReceiveDisconnectSignal { get; set; }

        [Inject] public IRoutineRunner RoutineRunner { get; set; }

        private bool _isListeningDenariaServer = false;

        private float interval = 1.0f / 30f; // 30 times per second
        private float nextExecutionTime;

        public void ConnectToDenariaServer()
        {
            NetworkManagerModel.ConnectToDenariaServer();
        }

        public void DisconnectFromDenariaServer()
        {
            NetworkManagerModel.DisconnectFromDenariaServer();
        }

        public IEnumerator StartListeningDenariaServer()
        {
            while (_isListeningDenariaServer)
            {
                nextExecutionTime = Time.realtimeSinceStartup + interval;
                ReceiveMessages();
                float waitTime = nextExecutionTime - Time.realtimeSinceStartup;
                if (waitTime > 0)
                {
                    yield return new WaitForSecondsRealtime(waitTime);
                }

                yield return new WaitForSeconds(interval);
            }
        }

        // --- SEND

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
            NetworkManagerModel.NetworkDriver.BeginSend(NetworkManagerModel.DenariaServerConnection.NetworkConnection, out var writer);
            writer.WriteUShort((ushort)messages.Length);
            foreach (var message in messages)
            {
                writer.WriteUShort((ushort)message.Length);
                writer.WriteBytes(message);
            }
        }
        public void SendReliableMessages(NativeArray<byte>[] messages)
        {
            NetworkManagerModel.NetworkDriver.BeginSend(
                NetworkManagerModel.DenariaServerConnection.ReliablePipeline,
                NetworkManagerModel.DenariaServerConnection.NetworkConnection,
                out var writer);
            writer.WriteUShort((ushort)messages.Length);
            foreach (var message in messages)
            {
                writer.WriteUShort((ushort)message.Length);
                writer.WriteBytes(message);
            }
        }





        // --- RECEIVE 

        // Run on every tick (1/30 seconds for DenariaServer)
        public void ReceiveMessages()
        {
            NetworkManagerModel.NetworkDriver.ScheduleUpdate().Complete();

            NetworkEvent.Type cmd;
            while ((cmd = NetworkManagerModel.DenariaServerConnection.NetworkConnection.PopEvent(NetworkManagerModel.NetworkDriver, out DataStreamReader stream, out var receivePipeline)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Connect && !NetworkManagerModel.DenariaServerConnection.IsConnectionAccepted)
                {
                    NetworkManagerModel.DenariaServerConnection.IsConnectionAccepted = true;
                }
                else if (cmd == NetworkEvent.Type.Data)
                {
                    if (receivePipeline.Equals(NetworkManagerModel.DenariaServerConnection.ReliablePipeline))
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
                    NetworkManagerModel.DenariaServerConnection.IsConnectionAccepted = false;
                    NetworkManagerModel.DenariaServerConnection.NetworkConnection = default;
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
            byte messageType = (byte)SendOpCode.Connect;

            byte[] tempBytes = Encoding.UTF8.GetBytes(NetworkManagerModel.PlayerId);
            byte[] playerIdBytes = new byte[16];

            Buffer.BlockCopy(tempBytes, 0, playerIdBytes, 0, tempBytes.Length);

            NativeArray<byte> messagePacket = AddEventHeader(messageType, playerIdBytes);
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
