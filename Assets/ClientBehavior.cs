using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System.Text;
using System;

public class ClientBehaviour : MonoBehaviour
{
    public static ClientBehaviour Instance { get; private set; }
    NetworkDriver m_Driver;
    NetworkConnection m_Connection;
    private SystemManager systemManager;
    private GameManager gameManager = null;
    public bool isConnected = false;
    NetworkPipeline reliablePipeline;
    NetworkEndpoint endpoint = NetworkEndpoint.LoopbackIpv4.WithPort(5000);

    public enum SendOpCode : byte
    {
        Connect = 0,
    }

    public enum ReceiveOpcode : byte
    {
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        systemManager = SystemManager.Instance;
        m_Driver = NetworkDriver.Create();
        reliablePipeline = m_Driver.CreatePipeline(typeof(ReliableSequencedPipelineStage));
        m_Connection = default;
    }

    public void Connect()
    {
        m_Connection = m_Driver.Connect(endpoint);
    }

    public void Disconnect()
    {
        isConnected = false;
        m_Connection.Disconnect(m_Driver);
    }

    public void InitGameManager()
    {
        gameManager = GameManager.Instance;
    }

    void OnDestroy()
    {
        m_Driver.Dispose();
    }

    void Update()
    {
        CheckConnection();
        m_Driver.ScheduleUpdate().Complete();

        NetworkEvent.Type cmd;
        while ((cmd = m_Connection.PopEvent(m_Driver, out DataStreamReader stream, out var receivePipeline)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect && !isConnected)
            {
                isConnected = true;
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                if (receivePipeline.Equals(reliablePipeline))
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
                isConnected = false;
                m_Connection = default;
            }
        }
    }

    void CheckConnection()
    {
        if (!m_Driver.IsCreated)
        {
            Debug.Log("Network driver not created");
            return;
        }
        if (!m_Connection.IsCreated)
        {
            return;
        }
    }

    public void SendPlayerConnectMessage()
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreatePlayerConnectMessage();
        SendMessages(ref messages);
    }

    public void SendMovement(Vector2 movement)
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreateMovementMessage(movement);
        SendMessages(ref messages);
    }

    public void SendRotation(float rotation)
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreateRotationMessage(rotation);
        SendMessages(ref messages);
    }

    void ProcessReliableData(ref DataStreamReader stream)
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
                    case 1: // Location Update
                        Debug.Log("WARNING! Location message sent in unreliable channel!");
                        break;
                    case 0: // Spawn Update
                        ProcessSpawnUpdate(ref messageStream);
                        break;
                    case 10:
                        ProcessDisconnectUpdate(ref messageStream);
                        break;
                }
            }
        }
    }

    void ProcessUnreliableData(ref DataStreamReader stream)
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

    void ProcessPositionMessage(ref DataStreamReader reader)
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

            gameManager.UpdatePlayerPosition(playerId, newPosition);
        }
    }

    void ProcessRotationMessage(ref DataStreamReader reader)
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

            Vector3 newRotation = new(x, y, z);

            gameManager.UpdatePlayerRotation(playerId, newRotation);
        }
    }

    void ProcessSpawnUpdate(ref DataStreamReader reader)
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
            float newRotation = reader.ReadFloat();
            Vector2 spawnLocation = new(x, y);
            gameManager.SpawnPlayer(playerId, spawnLocation, newRotation);
        }
    }

    void ProcessDisconnectUpdate(ref DataStreamReader reader)
    {
        if (!gameManager) return;
        ulong playerNum = reader.ReadULong();
        for (ulong i = 0; i < playerNum; i++)
        {
            NativeArray<byte> stringBytes = new(16, Allocator.Temp);
            reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

            string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
            stringBytes.Dispose();

            gameManager.HandleDisconnectedPlayer(playerId);
        }
    }

    NativeArray<byte> CreateMovementMessage(Vector2 movement)
    {
        byte eventType = 2;  // EventIn Type 2 for Move
        byte[] data = new byte[8];
        Buffer.BlockCopy(BitConverter.GetBytes(movement.x), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(movement.y), 0, data, 4, 4);

        NativeArray<byte> messagePacket = AddEventHeader(eventType, data);
        return messagePacket;
    }

    NativeArray<byte> CreateRotationMessage(float rotation)
    {
        byte eventType = 3;  // EventIn Type 3 for Rotation
        byte[] data = new byte[4];
        Buffer.BlockCopy(BitConverter.GetBytes(rotation), 0, data, 0, 4);

        NativeArray<byte> messagePacket = AddEventHeader(eventType, data);
        return messagePacket;
    }

    NativeArray<byte> CreatePlayerConnectMessage()
    {
        byte messageType = (byte)SendOpCode.Connect;

        byte[] tempBytes = Encoding.UTF8.GetBytes(gameManager.ownPlayerId);
        byte[] playerIdBytes = new byte[16];

        Buffer.BlockCopy(tempBytes, 0, playerIdBytes, 0, tempBytes.Length);

        var messagePacket = AddEventHeader(messageType, playerIdBytes);
        return messagePacket;
    }


    NativeArray<byte> AddEventHeader(byte opCode, byte[] data)
    {
        NativeArray<byte> messagePacket = new(1 + data.Length, Allocator.Persistent);
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

    void SendMessages(ref NativeArray<byte>[] messages, NetworkPipeline pipeline = default)
    {
        if (pipeline == default)
        {
            m_Driver.BeginSend(m_Connection, out var writer);
            writer.WriteUShort((ushort)messages.Length);
            foreach (var message in messages)
            {
                writer.WriteUShort((ushort)message.Length);
                writer.WriteBytes(message);
                message.Dispose();
            }
            m_Driver.EndSend(writer);
        }
        else
        {
            m_Driver.BeginSend(pipeline, m_Connection, out var writer);
            writer.WriteUShort((ushort)messages.Length);
            foreach (var message in messages)
            {
                writer.WriteULong(0);
                writer.WriteUShort((ushort)message.Length);
                writer.WriteBytes(message);
                message.Dispose();
            }
            m_Driver.EndSend(writer);
        }
    }
}



public class DebugHelper : MonoBehaviour
{
    public static void LogBytes(NativeArray<byte> data)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var b in data)
        {
            sb.Append(b + ", ");
        }
        Debug.Log(sb.ToString().TrimEnd(',', ' ')); // Remove the last comma and space
    }
}