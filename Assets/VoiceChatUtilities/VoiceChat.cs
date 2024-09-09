using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.WebRTC;
using Unity.Networking.Transport;
using Unity.Collections;

using System.Text;
using Newtonsoft.Json.Linq;

using Adrenak.UniVoice;
using Adrenak.UniVoice.UniMicInput;
using Adrenak.UniVoice.AudioSourceOutput;


[System.Serializable]
public class CreateRoomData
{
    public string rType;
}
[System.Serializable]
public class JoinRoomData
{
    public string rId;
    public string pId;
}
[System.Serializable]
public class QuitRoomData
{
    public string rId;
}

[System.Serializable]
public class RtcOfferData
{
    public string pId;
    public string ofr;
    public string ofT;
}

[System.Serializable]
public class RtcIceCandidateData
{
    public string pId;
    public string icd;
    public int icI;
}

[System.Serializable]
public class RtcAnswerData
{
    public string pId;
    public string ans;
    public string anT;
}

[System.Serializable]
public class RtcPeerStatusData
{
    public string pId;
    public string pS;
}

[System.Serializable]
public class RtcIntroductionData
{
    public string pN;
}


public class VoiceChat : MonoBehaviour
{
    // These settings get from Unity Object

    public string playerName = "Tester";
    public bool isRoomHost = false; // Debug functions
    public string roomId = ""; // example: x1y2z3
    public bool voiceTransmitting = false;
    public bool voiceReceive = false;
    public bool enableNotificationSounds = false;

    private bool testMessageSent = false;
    private bool introductionMessageSent = false;


    private readonly string serverIp = "159.146.67.181";
    private readonly string serverPort = "2428";
    // Voice Server
    private NetworkDriver driver;
    private NetworkConnection connection;
    private bool isConnected = false;
    private bool isConnectionAccepted = false;
    private string ownPlayerId = "";
    private string ownCurrentRoom = "";
    private bool roomNewcomer = false;
    private Dictionary<string, string> roommatesData = new Dictionary<string, string>();
    private Dictionary<string, string> roommatesPeerStatus = new Dictionary<string, string>();

    // UniVoice & Audio
    private UniVoiceUniMicInput micInput;
    private UniVoiceAudioSourceOutput.Factory audioOutputFactory;
    private AudioSource notificationAudioSource;
    public AudioClip userJoinClip;
    public AudioClip userDropClip;

    // WebRTC
    private RTCPeerConnection localConnection;
    private Dictionary<string, List<RTCIceCandidate>> storedIceCandidates = new Dictionary<string, List<RTCIceCandidate>>();

    private Dictionary<string, RTCPeerConnection> remoteConnections = new Dictionary<string, RTCPeerConnection>();
    private Dictionary<string, RTCDataChannel> dataChannels = new Dictionary<string, RTCDataChannel>();
    private Dictionary<string, IAudioOutput> peerOutputs = new Dictionary<string, IAudioOutput>();
    private bool rtcSetupCompleted = false;

    void Start()
    {
        InitializeAudio();

        InitializeServerConnection();

        InitializeWebRTC();
    }

    void InitializeServerConnection() {
        // Initialize NetworkDriver for Unity Transport
        driver = NetworkDriver.Create();
        connection = default(NetworkConnection);

        // Set up the endpoint using the serverIp and serverPort
        if (NetworkEndpoint.TryParse(serverIp, ushort.Parse(serverPort), out NetworkEndpoint endpoint))
        {
            // Connect to the server
            connection = driver.Connect(endpoint);
            isConnected = true;
            Debug.Log($"Connecting to server at {endpoint.Address}");
        }
        else
        {
            Debug.LogError("Failed to parse server endpoint with IP: " + serverIp + " and Port: " + serverPort);
        }
    }

    void InitializeAudio()
    {
        micInput = new UniVoiceUniMicInput(0, 16000, 100);
        audioOutputFactory = new UniVoiceAudioSourceOutput.Factory(10, 5);

        notificationAudioSource = gameObject.AddComponent<AudioSource>();
        notificationAudioSource.playOnAwake = false;
        notificationAudioSource.volume = 1f;
        
        micInput.OnSegmentReady += OnSegmentReady;
    }

    void InitializeWebRTC()
    {
        // Create local peer
        localConnection = new RTCPeerConnection();
        
        // Set up ICE candidate handling
        localConnection.OnIceCandidate = e => 
        {
            if (!string.IsNullOrEmpty(e.Candidate))
            {
                Debug.Log($"Local ICE candidate: {e.Candidate}");
            }
        };

        localConnection.OnIceConnectionChange = state => 
        {
            Debug.Log($"ICE Connection State: {state}");
        };

        localConnection.OnConnectionStateChange = state => 
        {
            Debug.Log($"Connection State: {state}");
        };

        rtcSetupCompleted = true;

    }

    void Update()
    {
        if (!isConnected){return;}

        // Update the network driver
        driver.ScheduleUpdate().Complete();

        // Check for connection events
        if (connection.IsCreated)
        {
            NetworkEvent.Type cmd;
            while ((cmd = connection.PopEvent(driver, out DataStreamReader stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    // Read the data received from the server
                    NativeArray<byte> receivedData = new(stream.Length, Allocator.Temp);
                    stream.ReadBytes(receivedData);

                    DigestServerData(receivedData);

                    receivedData.Dispose();
                }
                else if (cmd == NetworkEvent.Type.Connect)
                {
                    Debug.Log("Connected to server");
                    isConnectionAccepted = true;

                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Disconnected from server");
                    connection = default(NetworkConnection);
                    isConnectionAccepted = false;
                    isConnected = false;
                }
            }

            SendIntroductionCommand();
            // Debug functions
            // Send commands once after connection is established
            if (isConnectionAccepted && !testMessageSent && rtcSetupCompleted)
            {
                if (isRoomHost)
                {
                    SendCreateRoomCommand();
                }
                else
                {
                    SendJoinRoomCommand(roomId);
                }
            }
        }
        else
        {
            Debug.Log("Connection is not created or is disconnected.");
        }
    }


    public void PlayNotification(string notificationType)
    {
        if (!enableNotificationSounds) return;

        AudioClip clipToPlay = null;
        if (notificationType == "Join")
        {
            clipToPlay = userJoinClip;
        }
        else if (notificationType == "Drop")
        {
            clipToPlay = userDropClip;
        }

        if (clipToPlay != null)
        {
            notificationAudioSource.clip = clipToPlay;
            notificationAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"No audio clip found for notification type: {notificationType}");
        }
    }

    // Incoming Server Data Processing
    JObject ParseJsonData(NativeArray<byte> data)
    {
        NativeArray<byte> jsonData = data.GetSubArray(1, data.Length - 1);
        string jsonString = System.Text.Encoding.UTF8.GetString(jsonData.ToArray());
        jsonData.Dispose();
        return JObject.Parse(jsonString);
    }

    void DigestServerData(NativeArray<byte> data)
    {
        try
        {
            byte data_type = data[0];

            // Debug.Log($"Received server data type: {data_type}");

            switch (data_type)
            {
                case 0x01:     // CreateRoom
                    JObject createRoomJsonObject = ParseJsonData(data);
                    if (createRoomJsonObject.ContainsKey("rId") && createRoomJsonObject.ContainsKey("rSt"))
                    {
                        string create_status = createRoomJsonObject["rSt"].ToString();
                        if (create_status == "ok") {
                            string room_id = createRoomJsonObject["rId"].ToString();
                            Debug.Log($"Created Room Id: {room_id}");
                            ownCurrentRoom = room_id;
                        }
                        else {
                            Debug.LogWarning("Failed to create Room.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Invalid CreateRoom response JSON data.");
                    }

                    break;

                case 0x02:      // JoinRoom
                    JObject joinRoomJsonObject = ParseJsonData(data);
                    if (joinRoomJsonObject.ContainsKey("rId") && joinRoomJsonObject.ContainsKey("rSt"))
                    {
                        string join_status = joinRoomJsonObject["rSt"].ToString();
                        if (join_status == "ok") {
                            string room_id = joinRoomJsonObject["rId"].ToString();
                            Debug.Log($"Joined Room successfully: {room_id}");
                            ownCurrentRoom = room_id;
                            roomNewcomer = true;
                        }
                        else {
                            Debug.LogWarning("Failed to join Room.");
                        }
                    } 
                    else
                    {
                        Debug.LogWarning("Invalid JoinRoom response JSON data.");
                    }
                    break;

                case 0x03:      // Leave Room
                    JObject leaveRoomJsonObject = ParseJsonData(data);
                    if (leaveRoomJsonObject.ContainsKey("rId") && leaveRoomJsonObject.ContainsKey("rSt"))
                    {
                        string leave_status = leaveRoomJsonObject["rSt"].ToString();
                        if (leave_status == "ok") {
                            string room_id = leaveRoomJsonObject["rId"].ToString();
                            foreach (var player in roommatesData)
                            {
                                PeerDisconnection(player.Key);
                            }
                            roommatesData.Clear();
                            ownCurrentRoom = "";
                            Debug.Log($"Left Room successfully: {room_id}");
                        }
                        else {
                            Debug.LogWarning($"Failed to leave Room: {leave_status}");
                        }
                    } 
                    else
                    {
                        Debug.LogWarning("Invalid LeaveRoom response JSON data.");
                    }
                    break;

                case 0x04:      // Introduction Response
                    JObject introductionResponseJsonObject = ParseJsonData(data);
                    if (introductionResponseJsonObject.ContainsKey("pId"))
                    {
                        ownPlayerId = introductionResponseJsonObject["pId"].ToString();
                        Debug.Log($"Player registered: {ownPlayerId}");
                    }
                    else
                    {
                        Debug.LogWarning("Invalid Introduction Response JSON data.");
                    }
                    break;

                case 0x05:      // Offer
                    JObject offerJsonObject = ParseJsonData(data);
                    if (offerJsonObject.ContainsKey("ofr") && offerJsonObject.ContainsKey("ofT") && offerJsonObject.ContainsKey("pId"))
                    {
                        string offerSdp = offerJsonObject["ofr"].ToString();
                        string offerType = offerJsonObject["ofT"].ToString();
                        string playerId = offerJsonObject["pId"].ToString();
                        AcceptPeerRequest(playerId, offerSdp);
                    }
                    break;

                case 0x06:      // ICE Candidate
                    JObject iceCandidateJsonObject = ParseJsonData(data);
                    if (iceCandidateJsonObject.ContainsKey("icd") && iceCandidateJsonObject.ContainsKey("pId"))
                    {
                        string candidate = iceCandidateJsonObject["icd"].ToString();
                        string playerId = iceCandidateJsonObject["pId"].ToString();
                        // Create RTCIceCandidateInit with sdpMid
                        RTCIceCandidateInit candidateInit = new RTCIceCandidateInit
                        {
                            candidate = candidate,
                            sdpMid = "0"  // Provide a default value, or extract from your data if available
                        };
                        
                        RTCIceCandidate iceCandidate = new RTCIceCandidate(candidateInit);
                        HandleIncomingIceCandidate(playerId, iceCandidate);
                    }
                    break;

                case 0x07:      // Answer
                    JObject answerJsonObject = ParseJsonData(data);
                    if (answerJsonObject.ContainsKey("ans") && answerJsonObject.ContainsKey("anT") && answerJsonObject.ContainsKey("pId"))
                    {
                        string answerSdp = answerJsonObject["ans"].ToString();
                        string answerType = answerJsonObject["anT"].ToString();
                        string playerId = answerJsonObject["pId"].ToString();
                        HandleIncomingAnswer(playerId, answerSdp);
                    }
                    break;

                case 0x08:      //  Empty Room
                    PeerManager(new JObject(), true);
                    break;

                case 0x09:      // Room Player Addr's
                    JObject roomPlayerAddrsJsonObject = ParseJsonData(data);
                    PeerManager(roomPlayerAddrsJsonObject);
                    break;

                default:
                    Debug.LogWarning($"Unknown data type: {data_type}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to process Server data: " + ex.Message);
            // Log the exception using Debug.LogException for better visibility
            Debug.LogException(ex, this);
            // Debug.Log(System.Text.Encoding.UTF8.GetString(data.ToArray()));
        }
    }


    // Peer connection management
    private void PeerManager(JObject peerData, bool emptyRoom = false)
    {
        if (emptyRoom) {
            // Check roommatesData is empty or not
            if (roommatesData.Count != 0) {
                Debug.Log($"Disconnecting {roommatesData.Count} players");
                // Disconnect all players in roommatesData
                foreach (var player in roommatesData)
                {
                    Debug.Log($"Disconnecting {player.Key} - {player.Value}");
                    PeerDisconnection(player.Key);
                }
                roommatesData.Clear();

            }
            return;
        }
        Dictionary<string, string> newPlayersData = new Dictionary<string, string>();
        Dictionary<string, string> quittedPlayersData = new Dictionary<string, string>();
        bool hasNewPlayer = false;
        bool hasPlayerLeft = false;
        // Check for new players and updated data
        foreach (var player in peerData)
        {
            string playerId = player.Key;
            string playerData = player.Value.ToString();
            if (!roommatesData.ContainsKey(playerId))
            {
                newPlayersData[playerId] = playerData;
                hasNewPlayer = true;
            }
        }
        // Check for players who quit
        foreach (var player in roommatesData)
        {
            if (!peerData.ContainsKey(player.Key))
            {
                quittedPlayersData[player.Key] = player.Value;
                hasPlayerLeft = true;
            }
        }

        if (hasNewPlayer)
        {
            if (!roomNewcomer)
            {
                PlayNotification("Join");
            }
            foreach (var player in newPlayersData)
            {
                roommatesPeerStatus[player.Key] = "Connecting";
                peerOutputs[player.Key] = audioOutputFactory.Create(16000, 1, 1600);
                roommatesData[player.Key] = player.Value;
                if (roomNewcomer)
                {
                    NewPeerConnection(player.Key);
                }
            }
            roomNewcomer = false;

        }


        if (hasPlayerLeft)
        {
            if (!roomNewcomer)
            {
                PlayNotification("Drop");
            }
            foreach (var player in quittedPlayersData)
            {
                PeerDisconnection(player.Key);
                roommatesData.Remove(player.Key);
                roommatesPeerStatus.Remove(player.Key);
                peerOutputs[player.Key].Dispose();
                peerOutputs.Remove(player.Key);
            }
        }
    }


    // Callback for microphone input
    private void OnSegmentReady(int segmentIndex, float[] segment)
    {
        
        // audioOutput?.Feed(segmentIndex, micInput.Frequency, micInput.ChannelCount, segment);     // Test mic loopback to output

        if (!voiceTransmitting) {return;}

        // Iterate through peer connections and send segment to peers
        foreach (var kvp in remoteConnections)
        {
            string playerId = kvp.Key;
            RTCPeerConnection peerConnection = kvp.Value;

            // Check if the connection is established and the data channel exists
            if (peerConnection.ConnectionState == RTCPeerConnectionState.Connected && 
                dataChannels.TryGetValue(playerId, out RTCDataChannel dataChannel))
            {
                // Convert float array to byte array
                byte[] byteArray = new byte[segment.Length * 4];
                Buffer.BlockCopy(segment, 0, byteArray, 0, byteArray.Length);

                // Create a simple audio packet structure
                byte[] packet = new byte[byteArray.Length + 4];
                BitConverter.GetBytes(segmentIndex).CopyTo(packet, 0);
                byteArray.CopyTo(packet, 4);

                // Send the audio data through the data channel
                dataChannel.Send(packet);
            }
        }
    }

    // Modify SetupDataChannel to handle a single channel per peer
    private void SetupDataChannel(RTCDataChannel channel, string playerId)
    {
        if (!dataChannels.ContainsKey(playerId))
        {
            channel.OnOpen = () => 
            {
                Debug.Log($"Data channel opened for player: {playerId}");

            };
            channel.OnClose = () => 
            {
                Debug.Log($"Data channel closed for player: {playerId}");
                dataChannels.Remove(playerId);
            };
            channel.OnMessage = bytes => 
            {
                // Extract segment index
                int segmentIndex = BitConverter.ToInt32(bytes, 0);

                // Extract audio data
                float[] audioSegment = new float[(bytes.Length - 4) / 4];
                Buffer.BlockCopy(bytes, 4, audioSegment, 0, bytes.Length - 4);

                // Process the received audio data
                ProcessReceivedAudio(playerId, segmentIndex, audioSegment);
            };
            dataChannels[playerId] = channel;
        }
        else
        {
            Debug.Log($"Data channel already exists for player: {playerId}. Ignoring new channel.");
        }
    }

    private void ProcessReceivedAudio(string playerId, int segmentIndex, float[] audioSegment)
    {
        if (!voiceReceive) {return;}

        // Find the audio output for this player
        if (peerOutputs.TryGetValue(playerId, out var audioOutput))
        {
            audioOutput.Feed(segmentIndex, 16000, 1, audioSegment);
        }
        else
        {
            Debug.LogWarning($"No audio output found for player: {playerId}");
        }
    }

    // Request Peer Connection
    void NewPeerConnection(string playerId)
    {
        // Configure ICE servers
        RTCConfiguration config = new RTCConfiguration
        {
            iceServers = new[] { new RTCIceServer { urls = new[] { "stun:stun.l.google.com:19302" } } }
        };

        // Create remote peer connection with config
        var remoteConnection = new RTCPeerConnection(ref config);
        remoteConnections[playerId] = remoteConnection;

        // Set up ICE candidate handling for remote peer
        remoteConnection.OnIceCandidate = e => 
        {
            // We'll handle this in the coroutine
        };

        // Add more logging for connection state changes
        remoteConnection.OnConnectionStateChange = state => // Connecting, Connected
        {
            if (state == RTCPeerConnectionState.Connected)
            {
                Debug.Log($"Connection established with {playerId}");
                roommatesPeerStatus[playerId] = "Connected";

                // Send server peer status
                var peerStatusData = new RtcPeerStatusData
                {
                    pId = playerId,
                    pS = "Connected"
                };
                string jsonString = JsonUtility.ToJson(peerStatusData);
                SendSinglePacketToServer(8, jsonString);
            }
        };

        remoteConnection.OnIceConnectionChange = state =>   // Checking , Completed
        {
            if (state == RTCIceConnectionState.Completed)
            {
                Debug.Log($"ICE connection completed for {playerId}");
            }
        };

        remoteConnection.OnIceGatheringStateChange = state =>
        {
            if (state == RTCIceGatheringState.Complete)
            {
                Debug.Log($"ICE gathering completed for {playerId}");
                StartCoroutine(SendStoredIceCandidates(playerId));
            }
        };

        // Create data channel only if it doesn't exist
        if (!dataChannels.ContainsKey(playerId))
        {
            var dataChannel = remoteConnection.CreateDataChannel($"dc_{playerId}", new RTCDataChannelInit());
            SetupDataChannel(dataChannel, playerId);
        }

        // Set up remote peer data channel callback
        remoteConnection.OnDataChannel = (channel) => 
        {
            Debug.Log($"Received data channel from {playerId}");
            SetupDataChannel(channel, playerId);
        };

        // Start signaling process 
        StartCoroutine(CreateOfferAndSetLocalDescription(playerId));
    }

    private IEnumerator CreateOfferAndSetLocalDescription(string playerId)
    {
        var remoteConnection = remoteConnections[playerId];

        // Create offer
        RTCOfferAnswerOptions offerOptions = new RTCOfferAnswerOptions();
        var offerOp = remoteConnection.CreateOffer(ref offerOptions);
        yield return offerOp;

        if (offerOp.IsError)
        {
            Debug.LogError($"Error creating offer for {playerId}: {offerOp.Error.message}");
            yield break;
        }

        // Set local description
        var offerDesc = offerOp.Desc;
        var setLocalDescOp = remoteConnection.SetLocalDescription(ref offerDesc);
        yield return setLocalDescOp;

        if (setLocalDescOp.IsError)
        {
            Debug.LogError($"Error setting local description for {playerId}: {setLocalDescOp.Error.message}");
            yield break;
        }

        // Send offer to server
        var offerData = new RtcOfferData
        {
            pId = playerId,
            ofr = offerDesc.sdp,
            ofT = offerDesc.type.ToString()
        };
        string jsonString = JsonUtility.ToJson(offerData);
        SendSinglePacketToServer(5, jsonString);

        Debug.Log($"Offer with description sent to {playerId}. SDP: {offerDesc.sdp}");

        remoteConnection.OnIceCandidate = e => 
        {
            if (e.Candidate != null)
            {
                // Store the ICE candidate instead of sending it immediately
                if (!storedIceCandidates.ContainsKey(playerId))
                {
                    storedIceCandidates[playerId] = new List<RTCIceCandidate>();
                }
                storedIceCandidates[playerId].Add(e);
                Debug.Log($"Stored ICE candidate for {playerId}: {e.Candidate}");
            }
        };
    }

    // Coroutine to send stored ICE candidates
    private IEnumerator SendStoredIceCandidates(string playerId)
    {
        if (storedIceCandidates.TryGetValue(playerId, out var candidates))
        {
            int sentCandidateCount = 0;
            for (int i = 0; i < candidates.Count; i++)
            {
                var iceCandidate = candidates[i];
                var candidateData = new RtcIceCandidateData
                {
                    pId = playerId,
                    icd = iceCandidate.Candidate,
                    icI = i  // Adding the index of the candidate
                };
                string jsonString = JsonUtility.ToJson(candidateData);
                SendSinglePacketToServer(6, jsonString);
                sentCandidateCount++;

                // Debug.Log($"Sent stored ICE candidate to {playerId}: {iceCandidate.Candidate}");
                yield return null; // Yield to avoid blocking the main thread
            }

            // Clear the stored candidates after sending
            storedIceCandidates.Remove(playerId);
            Debug.Log($"Sent {sentCandidateCount} ICE candidates to {playerId}");
        }
    }

    // Accept Peer Request
    public void AcceptPeerRequest(string playerId, string offerSdp)
    {
        // Debug.Log($"Accepting peer request from player: {playerId}");

        // Configure ICE servers
        RTCConfiguration config = new RTCConfiguration
        {
            iceServers = new[] { new RTCIceServer { urls = new[] { "stun:stun.l.google.com:19302" } } }
        };

        // Create a new RTCPeerConnection for this remote peer
        var remoteConnection = new RTCPeerConnection(ref config);
        remoteConnections[playerId] = remoteConnection;

        // Set up ICE candidate handling for the remote peer
        remoteConnection.OnIceCandidate = e => 
        {
            // We'll handle this in the coroutine
        };

        // Add more logging for connection state changes
        remoteConnection.OnConnectionStateChange = state => // Connecting, Connected
        {
            if (state == RTCPeerConnectionState.Connected)
            {
                Debug.Log($"Connection established with {playerId}");
                roommatesPeerStatus[playerId] = "Connected";
            }
        };

        remoteConnection.OnIceConnectionChange = state => // Checking , Connected
        {
            if (state == RTCIceConnectionState.Connected)
            {
                Debug.Log($"ICE connection completed for {playerId}");
            }
        };

        remoteConnection.OnIceGatheringStateChange = state =>
        {
            if (state == RTCIceGatheringState.Complete)
            {
                Debug.Log($"ICE gathering completed for {playerId}");
                StartCoroutine(SendStoredIceCandidates(playerId));
            }
        };

        // Be prepared to receive a data channel
        remoteConnection.OnDataChannel = channel => 
        {
            Debug.Log($"Received data channel from {playerId}");
            SetupDataChannel(channel, playerId);
        };

        // Start the process of accepting the offer and creating an answer
        StartCoroutine(AcceptOfferAndCreateAnswer(playerId, offerSdp));
    }

    private IEnumerator AcceptOfferAndCreateAnswer(string playerId, string offerSdp)
    {
        var remoteConnection = remoteConnections[playerId];

        // Create and set the remote description from the received offer
        RTCSessionDescription offerDesc = new RTCSessionDescription();
        offerDesc.type = RTCSdpType.Offer;
        offerDesc.sdp = offerSdp;
        var setRemoteDescOp = remoteConnection.SetRemoteDescription(ref offerDesc);
        yield return setRemoteDescOp;

        if (setRemoteDescOp.IsError)
        {
            Debug.LogError($"Error setting remote description for {playerId}: {setRemoteDescOp.Error.message}");
            yield break;
        }

        // Create an answer
        var answerOp = remoteConnection.CreateAnswer();
        yield return answerOp;

        if (answerOp.IsError)
        {
            Debug.LogError($"Error creating answer for {playerId}: {answerOp.Error.message}");
            yield break;
        }

        // Set the local description to our answer
        RTCSessionDescription answerDesc = answerOp.Desc;
        var setLocalDescOp = remoteConnection.SetLocalDescription(ref answerDesc);
        yield return setLocalDescOp;

        if (setLocalDescOp.IsError)
        {
            Debug.LogError($"Error setting local description for {playerId}: {setLocalDescOp.Error.message}");
            yield break;
        }

        // Send the answer back to the offering peer
        var answerData = new RtcAnswerData
        {
            pId = playerId,
            ans = answerDesc.sdp,
            anT = answerDesc.type.ToString()
        };
        string jsonString = JsonUtility.ToJson(answerData);
        SendSinglePacketToServer(7, jsonString);

        Debug.Log($"Answer with description sent to {playerId}. SDP: {answerDesc.sdp}");

        // Set up ICE candidate handling for the remote peer
        remoteConnection.OnIceCandidate = e => 
        {
            if (e.Candidate != null)
            {
                // Store the ICE candidate instead of sending it immediately
                if (!storedIceCandidates.ContainsKey(playerId))
                {
                    storedIceCandidates[playerId] = new List<RTCIceCandidate>();
                }
                storedIceCandidates[playerId].Add(e);
                // Debug.Log($"Stored ICE candidate for {playerId}: {e.Candidate}");
            }
        };


    }

    // Handle incoming Peer
    public void HandleIncomingIceCandidate(string playerId, RTCIceCandidate candidate)
    {
        // Debug.Log($"Received ICE candidate for {playerId}: {candidate.Candidate}");
        if (remoteConnections.TryGetValue(playerId, out var remoteConnection))
        {
            remoteConnection.AddIceCandidate(candidate);
        }
        else
        {
            Debug.LogWarning($"Received ICE candidate for unknown player: {playerId}");
        }
    }

    public void HandleIncomingAnswer(string playerId, string answerSdp)
    {
        Debug.Log($"Received answer from {playerId}");
        if (remoteConnections.TryGetValue(playerId, out var remoteConnection))
        {
            StartCoroutine(SetRemoteDescription(remoteConnection, answerSdp));
        }
        else
        {
            Debug.LogWarning($"Received answer for unknown player: {playerId}");
        }
    }

    private IEnumerator SetRemoteDescription(RTCPeerConnection connection, string sdp)
    {
        RTCSessionDescription answerDesc = new RTCSessionDescription
        {
            type = RTCSdpType.Answer,
            sdp = sdp
        };

        var op = connection.SetRemoteDescription(ref answerDesc);
        yield return op;

        if (op.IsError)
        {
            Debug.LogError($"Error setting remote description: {op.Error.message}");
        }
        else
        {
            // Debug.Log("Remote description set successfully");
        }
    }

    // Disconnect Peer
    void PeerDisconnection(string playerId)
    {
        Debug.Log($"Disconnecting peer with player: {playerId}");

        if (remoteConnections.TryGetValue(playerId, out var remoteConnection))
        {
            remoteConnection.Close();
            remoteConnections.Remove(playerId);
        }

        if (dataChannels.TryGetValue(playerId, out var dataChannel))
        {
            dataChannel.Close();
            dataChannels.Remove(playerId);
        }
    }

    // Send Single Packet to Server
    void SendSinglePacketToServer(byte command, string data)
    {
        if (!connection.IsCreated || connection.GetState(driver) != NetworkConnection.State.Connected)
        {
            Debug.LogError("Connection not fully established. !SendSinglePacketToServer.");
            return;
        }

        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        NativeArray<byte> nativeJsonBytes = new(dataBytes, Allocator.Temp);

        if (driver.BeginSend(connection, out DataStreamWriter writer) != 0)
        {
            Debug.LogError("Failed to begin send operation.");
            nativeJsonBytes.Dispose();
            return;
        }

        writer.WriteByte(command);
        writer.WriteBytes(nativeJsonBytes);
        driver.EndSend(writer);

        nativeJsonBytes.Dispose();
    }

    // Debug functions
    void SendIntroductionCommand()
    {

        if (introductionMessageSent) { return; }
        if (!connection.IsCreated || connection.GetState(driver) != NetworkConnection.State.Connected)
        {
            // Debug.LogError("Connection not fully established. Cannot send data.");
            return;
        }

        var introductionData = new RtcIntroductionData
        {
            pN = playerName
        };
        string jsonString = JsonUtility.ToJson(introductionData);

        SendSinglePacketToServer(4, jsonString);
        introductionMessageSent = true;
    }


    void SendCreateRoomCommand()
    {
        if (testMessageSent) { return; }

        if (!connection.IsCreated || connection.GetState(driver) != NetworkConnection.State.Connected)
        {
            // Debug.LogError("Connection not fully established. Cannot send data.");
            return;
        }
        var createRoomData = new CreateRoomData
        {
            rType = "public"    // unused feature
        };
        string jsonString = JsonUtility.ToJson(createRoomData);

        SendSinglePacketToServer(1, jsonString);

        testMessageSent = true;
    }

    void SendJoinRoomCommand(string roomId)
    {
        if (testMessageSent){return;}
        

        if (!connection.IsCreated || connection.GetState(driver) != NetworkConnection.State.Connected)
        {
            // Debug.LogError("Connection not fully established. Cannot send data.");
            return;
        }

        var joinRoomData = new JoinRoomData
        {
            rId = roomId
        };
        
        string jsonString = JsonUtility.ToJson(joinRoomData);

        SendSinglePacketToServer(2, jsonString);

        testMessageSent = true;
    }

    void SendQuitRoomCommand(string roomId)
    {
        if (!connection.IsCreated || connection.GetState(driver) != NetworkConnection.State.Connected)
        {
            // Debug.LogError("Connection not fully established. Cannot send data.");
            return;
        }

        var quitRoomData = new QuitRoomData
        {
            rId = roomId
        };
        string jsonString = JsonUtility.ToJson(quitRoomData);

        SendSinglePacketToServer(3, jsonString);

    }
    

    void OnDestroy()
    {
        if (micInput != null)
        {
            micInput.OnSegmentReady -= OnSegmentReady;
            micInput.Dispose();
        }


        // Close all WebRTC connections
        foreach (var dataChannel in dataChannels.Values)
        {
            dataChannel.Close();
        }

        foreach (var remoteConnection in remoteConnections.Values)
        {
            remoteConnection.Close();
        }

        localConnection.Close();


        if (connection.IsCreated)
        {
            connection.Disconnect(driver);
        }
        driver.Dispose();

    }
}