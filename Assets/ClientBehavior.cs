using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System.Text;
using System;

public class ClientBehaviour : MonoBehaviour
{
    NetworkDriver m_Driver;
    NetworkConnection m_Connection;

    public ThirdPersonMovement thirdPersonMovement;
    private bool isConnected = false;
    readonly string playerId = "Player1";

    void Start()
    {
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
        if (!m_Driver.IsCreated)
        {
            Debug.Log("Network driver not created");
            return;
        }
        m_Driver.ScheduleUpdate().Complete();
        if (!m_Connection.IsCreated)
        {
            Debug.Log("Connection not created or lost");
            return;
        }
        NetworkEvent.Type cmd;
        while ((cmd = m_Connection.PopEvent(m_Driver, out DataStreamReader stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect && !isConnected)
            {
                isConnected = true;
                m_Driver.BeginSend(m_Connection, out var writer);
                var connectMessage = CreateConnectMessage(playerId);
                DebugHelper.LogBytes(connectMessage);
                writer.WriteBytes(connectMessage);
                m_Driver.EndSend(writer);
                connectMessage.Dispose();
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
    public void SendMovement(Vector2 movement)
    {
        m_Driver.BeginSend(m_Connection, out var writer);
        var movementMessage = CreateMovementMessage(playerId, movement);
        DebugHelper.LogBytes(movementMessage);
        writer.WriteBytes(movementMessage);
        m_Driver.EndSend(writer);
        movementMessage.Dispose();
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
                    ProcessLocation(ref reader);
                    break;
                case 0: // Spawn Update
                    ProcessLocation(ref reader);
                    var connectConfirmMessage = CreateConnectConfirmMessage(playerId);
                    m_Driver.BeginSend(m_Connection, out var writer);
                    writer.WriteBytes(connectConfirmMessage);
                    m_Driver.EndSend(writer);
                    connectConfirmMessage.Dispose();
                    break;
            }
        }
    }

    void ProcessLocation(ref DataStreamReader reader)
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

            thirdPersonMovement.HandleMovement(new Vector2(x, y));
        }
    }

    NativeArray<byte> CreateMovementMessage(string playerId, Vector2 movement)
    {
        byte eventType = 2;  // Different type for movement message
        byte[] data = new byte[8];
        Buffer.BlockCopy(BitConverter.GetBytes(movement.x), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(movement.y), 0, data, 4, 4);

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
        DebugHelper.LogBytes(messagePacket);
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