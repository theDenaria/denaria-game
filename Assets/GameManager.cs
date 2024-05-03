using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject playerPrefab; // Assign this in the Inspector

    private Camera mainCamera;
    public CinemachineFreeLook cinemachineFreeLook; // Assign this in the Inspector

    [SerializeField] private ClientBehaviour clientBehaviour;

    [SerializeField] private PlayerInputHandler inputHandler;

    private Dictionary<string, Player> players = new Dictionary<string, Player>();

    public TMP_InputField playerIdInput;
    public Canvas connectCanvas;

    public string ownPlayerId;

    public bool isSpawned = false;

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
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isSpawned)
        {
            HandleRotation();
            SendInputToServer();
        }

    }

    // IEnumerator UpdatePosition(Vector3 start, Vector3 end, float duration)
    // {
    //     float elapsed = 0;
    //     while (elapsed < duration)
    //     {
    //         playerTransform.position = Vector3.Lerp(start, end, elapsed / duration);
    //         elapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     playerTransform.position = end; // Ensure the final position is set precisely
    // }

    public void ConnectButtonClicked()
    {
        Debug.Log($"Connect button pressed by {playerIdInput.text}");
        clientBehaviour.SendConnectEvent(playerIdInput.text);
        ownPlayerId = playerIdInput.text;
    }

    public void SpawnPlayer(string playerId, Vector2 spawnLocation, float yRotation)
    {
        // Instantiate the player at the desired position with default rotation
        if (players.ContainsKey(playerId)) return;

        GameObject newPlayerObj = Instantiate(playerPrefab, new Vector3(spawnLocation.x, 5.2f, spawnLocation.y), Quaternion.Euler(0f, yRotation, 0f));

        Player newPlayer = newPlayerObj.GetComponent<Player>();
        newPlayer.SetPlayerId(playerId);

        players.Add(playerId, newPlayer);

        if (newPlayerObj != null && playerId == ownPlayerId)
        {
            isSpawned = true;
            SetupCamera(newPlayerObj);
            connectCanvas.gameObject.SetActive(false);
        }

    }

    void SetupCamera(GameObject player)
    {
        // Set the Cinemachine camera's follow and look at targets
        cinemachineFreeLook.Follow = player.transform;
        cinemachineFreeLook.LookAt = player.transform;

        // Optionally, enable the Cinemachine Brain if it was disabled
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
    }

    void SendInputToServer()
    {
        if (inputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found in the scene!");
            return;
        }

        if (inputHandler.MoveInput != Vector2.zero)
        {
            Vector2 inputDirection = new Vector2(inputHandler.MoveInput.x, inputHandler.MoveInput.y);

            var rotatedMove = GetRotatedInput(inputDirection.normalized);

            clientBehaviour.SendMovement(rotatedMove);
        }
    }

    Vector2 GetRotatedInput(Vector2 normalizedInput)
    {
        float angleDegrees = players[ownPlayerId].transform.eulerAngles.y;
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // Convert degrees to radians
        float cosAngle = Mathf.Cos(angleRadians);
        float sinAngle = Mathf.Sin(angleRadians);

        float rotatedX = normalizedInput.x * cosAngle + normalizedInput.y * sinAngle;
        float rotatedY = -normalizedInput.x * sinAngle + normalizedInput.y * cosAngle;

        return new Vector2(rotatedX, rotatedY);
    }

    public void SendRotationToServer(float rotation)
    {
        clientBehaviour.SendRotation(rotation);
    }

    public void UpdatePlayerPosition(string playerId, Vector2 newPosition)
    {
        if (players.ContainsKey(playerId))
        {
            var newPositionn = new Vector3(newPosition.x, players[playerId].transform.position.y, newPosition.y);
            // players[playerId].transform.position = newPositionn;

            players[playerId].transform.position = Vector3.Lerp(transform.position, newPositionn, 5000.0f);
        }
        else
        {
            Debug.LogError("Player ID not found: " + playerId);
        }
    }

    void HandleRotation()
    {
        // Capture the Y-axis rotation of the camera.
        float yRotation = mainCamera.transform.eulerAngles.y;

        if (players.ContainsKey(ownPlayerId))
        {
            players[ownPlayerId].transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        }
        else
        {
            Debug.LogError("Player ID not found: " + ownPlayerId);
        }
    }

    public void UpdatePlayerRotation(string playerId, float newRotation)
    {
        if (playerId != ownPlayerId)
        {
            if (players.ContainsKey(playerId))
            {
                players[playerId].transform.rotation = Quaternion.Euler(0f, newRotation, 0f);

            }
            else
            {
                Debug.LogError("Player ID not found: " + playerId);
            }
        }

    }
}
