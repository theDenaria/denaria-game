using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System.Text;
using System;
using System.Collections.Generic;

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
    private Dictionary<string, PlayerInterpolation> playerEntities = new Dictionary<string, PlayerInterpolation>();

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
        if (!CheckConnection()) return;

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

    public bool CheckConnection()
    {
        if (!m_Driver.IsCreated)
        {
            Debug.Log("Network driver not created");
            return false;
        }
        if (!m_Connection.IsCreated)
        {
            return false;
        }
        return true;
    }

    public void SendPlayerConnectMessage()
    {
        if (CheckConnection())
        {
            NativeArray<byte>[] messages = new NativeArray<byte>[1];
            messages[0] = CreatePlayerConnectMessage();
            SendMessages(messages);
        }
    }

    public void SendMovement(Vector2 movement)
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreateMovementMessage(movement);
        Debug.Log("Sending movement");
        SendMessages(messages);
    }

    public void SendRotation(Vector4 axisAngles)
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreateRotationMessage(axisAngles);
        SendMessages(messages);
    }

    public void SendJump()
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreateJumpMessage();
        SendMessages(messages);
    }

    public void SendFire(Vector3 cam_origin, Vector3 direction, Vector3 barrel_origin)
    {
        NativeArray<byte>[] messages = new NativeArray<byte>[1];
        messages[0] = CreateFireMessage(cam_origin, direction, barrel_origin);
        SendMessages(messages);
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
                    case 0:
                        ProcessLevelObjects(ref messageStream);
                        break;
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
            float w = reader.ReadFloat();

            Vector4 newRotation = new(x, y, z, w);

            gameManager.UpdatePlayerRotation(playerId, newRotation);
        }
    }

    void ProcessFireUpdate(ref DataStreamReader reader)
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

        gameManager.Fire(playerId, origin, direction);
    }

    void ProcessHitUpdate(ref DataStreamReader reader)
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

        gameManager.Hit(playerId, targetId, hitPoint);
    }

    void ProcessHealthUpdate(ref DataStreamReader reader)
    {
        ulong playerNum = reader.ReadULong();
        for (ulong i = 0; i < playerNum; i++)
        {
            NativeArray<byte> stringBytes = new(16, Allocator.Temp);
            reader.ReadBytes(stringBytes);  // Ensure this method exists or is correctly implemented

            string playerId = Encoding.UTF8.GetString(stringBytes.ToArray()).TrimEnd('\0');
            stringBytes.Dispose();

            float hp = reader.ReadFloat();

            gameManager.UpdateHealth(playerId, hp);
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

    void ProcessLevelObjects(ref DataStreamReader reader)
    {
        ulong objectsNum = reader.ReadULong();
        for (ulong i = 0; i < objectsNum; i++)
        {
            byte type = reader.ReadByte();
            byte color = reader.ReadByte();

            float position_x = reader.ReadFloat();
            float position_y = reader.ReadFloat();
            float position_z = reader.ReadFloat();

            float size_x = reader.ReadFloat();
            float size_y = reader.ReadFloat();
            float size_z = reader.ReadFloat();

            switch (type)
            {
                case 0: // Sphere
                    break;
                case 1: // Cube
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    // Set the position and rotation of the spawned cube
                    cube.transform.position = new Vector3(position_x, position_y, position_z);

                    // Optionally, you can customize the cube's properties, like its scale
                    cube.transform.localScale = new Vector3(size_x, size_y, size_z);
                    Renderer cubeRenderer = cube.GetComponent<Renderer>();
                    if (color == 0)
                    {
                        cubeRenderer.enabled = false;
                    }
                    else
                    {
                        cubeRenderer.material.color = Uint8ToColor(color);
                    }
                    break;
                case 2: // Capsule
                    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);

                    // Set the position and rotation of the spawned cube
                    capsule.transform.position = new Vector3(position_x, position_y, position_z);

                    // Optionally, you can customize the cube's properties, like its scale
                    capsule.transform.localScale = new Vector3(size_x, size_y, size_z);

                    Renderer capsuleRenderer = capsule.GetComponent<Renderer>();
                    capsuleRenderer.material.color = Uint8ToColor(color);
                    break;
            }
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

        byte[] tempBytes = Encoding.UTF8.GetBytes(gameManager.ownPlayerId);
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

    void SendMessages(NativeArray<byte>[] messages, NetworkPipeline pipeline = default)
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
    Color Uint8ToColor(byte color)
    {
        switch (color)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.green;
            case 3:
                return Color.blue;
            case 4:
                return Color.white;
            case 5:
                return Color.black;
            case 6:
                return Color.gray;
            default:
                break;
        }
        return default;
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


