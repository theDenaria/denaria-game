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

    private GameManager gameManager;
    public bool isConnected = false;

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
        gameManager = GameManager.Instance;
        m_Driver = NetworkDriver.Create();
        m_Connection = default;
        var endpoint = NetworkEndpoint.LoopbackIpv4.WithPort(5000);
        m_Connection = m_Driver.Connect(endpoint);
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
        while ((cmd = m_Connection.PopEvent(m_Driver, out DataStreamReader stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect && !isConnected)
            {
                isConnected = true;

            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                ProcessData(ref stream);
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
            Debug.Log("Connection not created or lost");
            return;
        }
    }

    public void SendConnectEvent(string playerId)
    {
        m_Driver.BeginSend(m_Connection, out var writer);
        var connectMessage = CreateConnectMessage(playerId);
        DebugHelper.LogBytes(connectMessage);
        writer.WriteBytes(connectMessage);
        m_Driver.EndSend(writer);
        connectMessage.Dispose();
    }

    public void Disconnect()
    {
        // m_Driver.BeginSend(m_Connection, out var writer);
        // var connectMessage = CreateConnectMessage(playerId);
        // DebugHelper.LogBytes(connectMessage);
        // writer.WriteBytes(connectMessage);
        // m_Driver.EndSend(writer);
        // connectMessage.Dispose();
        m_Driver.Disconnect(m_Connection);
        // m_Connection.Close(m_Driver);
        // m_Connection.Disconnect(m_Driver);
    }


    public void SendMovement(Vector2 movement)
    {
        m_Driver.BeginSend(m_Connection, out var writer);
        var movementMessage = CreateMovementMessage(gameManager.ownPlayerId, movement);
        // DebugHelper.LogBytes(movementMessage);
        writer.WriteBytes(movementMessage);
        m_Driver.EndSend(writer);
        movementMessage.Dispose();
    }

    public void SendRotation(float rotation)
    {
        m_Driver.BeginSend(m_Connection, out var writer);
        var rotationMessage = CreateRotationMessage(gameManager.ownPlayerId, rotation);
        // DebugHelper.LogBytes(movementMessage);
        writer.WriteBytes(rotationMessage);
        m_Driver.EndSend(writer);
        rotationMessage.Dispose();
    }

    void ProcessData(ref DataStreamReader stream)
    {
        var reader = stream;
        while (reader.GetBytesRead() < reader.Length)
        {
            byte packetType = reader.ReadByte();
            switch (packetType)
            {
                case 1: // Location Update
                    ProcessLocationUpdate(ref reader);
                    break;
                case 0: // Spawn Update
                    ProcessSpawnUpdate(ref reader);
                    var connectConfirmMessage = CreateConnectConfirmMessage(gameManager.ownPlayerId);
                    m_Driver.BeginSend(m_Connection, out var writer);
                    writer.WriteBytes(connectConfirmMessage);
                    m_Driver.EndSend(writer);
                    connectConfirmMessage.Dispose();
                    break;
            }
        }
    }

    void ProcessLocationUpdate(ref DataStreamReader reader)
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

            Vector2 newPosition = new(x, y);

            gameManager.UpdatePlayerPosition(playerId, newPosition);
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

    NativeArray<byte> CreateMovementMessage(string playerId, Vector2 movement)
    {
        byte eventType = 2;  // EventIn Type 2 for Move
        byte[] data = new byte[8];
        Buffer.BlockCopy(BitConverter.GetBytes(movement.x), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(movement.y), 0, data, 4, 4);

        NativeArray<byte> messagePacket = AddEventHeader(eventType, playerId, data);
        return messagePacket;
    }

    NativeArray<byte> CreateRotationMessage(string playerId, float rotation)
    {
        byte eventType = 3;  // EventIn Type 3 for Rotation
        byte[] data = new byte[4];
        Buffer.BlockCopy(BitConverter.GetBytes(rotation), 0, data, 0, 4);

        NativeArray<byte> messagePacket = AddEventHeader(eventType, playerId, data);
        return messagePacket;
    }

    NativeArray<byte> CreateConnectMessage(string playerId)
    {
        byte eventType = 0;

        string connectMessage = "Player1 Connecting!!!";
        byte[] data = Encoding.UTF8.GetBytes(connectMessage);

        var messagePacket = AddEventHeader(eventType, playerId, data);
        return messagePacket;
    }

    NativeArray<byte> CreateConnectConfirmMessage(string playerId)
    {
        byte eventType = 1;
        string connectMessage = "Player1 Confirmed Connect!!!";
        byte[] data = Encoding.UTF8.GetBytes(connectMessage);
        var messagePacket = AddEventHeader(eventType, playerId, data);
        return messagePacket;
    }

    NativeArray<byte> AddEventHeader(byte eventType, string playerId, byte[] data)
    {
        byte[] tempBytes = Encoding.UTF8.GetBytes(playerId);

        NativeArray<byte> playerIdBytes = new(16, Allocator.Temp);
        int bytesToCopy = Mathf.Min(tempBytes.Length, 16);
        for (int i = 0; i < bytesToCopy; i++)
        {
            playerIdBytes[i] = tempBytes[i];
        }
        NativeArray<byte> messagePacket = new(1 + 16 + data.Length, Allocator.Persistent);
        messagePacket[0] = eventType;

        NativeArray<byte>.Copy(playerIdBytes, 0, messagePacket, 1, playerIdBytes.Length);

        if (data.Length > 0)
        {
            NativeArray<byte> nativeData = new(data.Length, Allocator.Temp);
            for (int i = 0; i < data.Length; i++)
            {
                nativeData[i] = data[i];
            }

            NativeArray<byte>.Copy(nativeData, 0, messagePacket, 16 + 1, nativeData.Length);
            nativeData.Dispose();
        }

        playerIdBytes.Dispose();
        // DebugHelper.LogBytes(messagePacket);
        return messagePacket;
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