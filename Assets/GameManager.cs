using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SystemManager systemManager;
    private ClientBehaviour clientBehaviour;
    private PlayerInputHandler playerInputHandler;
    public GameObject pauseMenuCanvas;

    private Camera mainCamera;
    private CinemachineFreeLook cinemachineFreeLook;

    public GameObject playerPrefab;
    private Dictionary<string, Player> players = new();

    public string ownPlayerId;

    public bool isSpawned = false;
    private bool isMenuOpen = false;

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
        playerInputHandler = PlayerInputHandler.Instance;
        systemManager = SystemManager.Instance;
        clientBehaviour = ClientBehaviour.Instance;
        systemManager.InitGameManager();
    }

    void Start()
    {
        clientBehaviour.Connect();
        mainCamera = Camera.main;
        ownPlayerId = systemManager.ownPlayerId;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawned && !isMenuOpen)
        {
            HandleRotation();
            SendInputToServer();
        }
        if (!isSpawned)
        {
            clientBehaviour.SendPlayerConnectMessage();
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

    void SendInputToServer()
    {
        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found in the scene!");
            return;
        }
        if (playerInputHandler.MoveInput != Vector2.zero)
        {
            Vector2 inputDirection = new Vector2(playerInputHandler.MoveInput.x, playerInputHandler.MoveInput.y);
            var rotatedMove = GetRotatedInput(inputDirection.normalized);
            clientBehaviour.SendMovement(rotatedMove);
        }
    }

    public void SendRotationToServer(float rotation)
    {
        clientBehaviour.SendRotation(rotation);
    }

    public void UpdatePlayerPosition(string playerId, Vector3 newPosition)
    {
        if (players.ContainsKey(playerId))
        {
            players[playerId].transform.position = Vector3.Lerp(transform.position, newPosition, 5000.0f);
        }
        else
        {
            SpawnPlayer(playerId, newPosition, 0.0f);
        }
    }

    public void UpdatePlayerRotation(string playerId, Vector3 newRotation)
    {
        if (playerId != ownPlayerId)
        {
            if (players.ContainsKey(playerId))
            {
                players[playerId].transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);

            }
            else
            {
                SpawnPlayer(playerId, new Vector3(5.0f, 5.0f, 5.0f), newRotation.y);
            }
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
    //
    public void DisconnectButtonClicked()
    {
        systemManager.Disconnect();
    }

    public void SpawnPlayer(string playerId, Vector3 spawnLocation, float yRotation)
    {
        // Instantiate the player at the desired position with default rotation
        if (players.ContainsKey(playerId)) return;

        GameObject newPlayerObj = Instantiate(playerPrefab, spawnLocation, Quaternion.Euler(0f, yRotation, 0f));

        Player newPlayer = newPlayerObj.GetComponent<Player>();
        newPlayer.SetPlayerId(playerId);

        players.Add(playerId, newPlayer);

        if (newPlayerObj != null && playerId == ownPlayerId)
        {
            isSpawned = true;
            SetupCamera(newPlayerObj);
        }

    }

    public void HandleDisconnectedPlayer(string playerId)
    {
        // Instantiate the player at the desired position with default rotation
        if (!players.ContainsKey(playerId)) return;
        Destroy(players[playerId].gameObject);
        players.Remove(playerId);
    }

    void SetupCamera(GameObject player)
    {
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        // Set the Cinemachine camera's follow and look at targets
        cinemachineFreeLook.Follow = player.transform;
        cinemachineFreeLook.LookAt = player.transform;

        // Optionally, enable the Cinemachine Brain if it was disabled
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
    }

    private void OnEnable()
    {
        // Ensure the action is enabled
        playerInputHandler.escMenuAction.Enable();
        playerInputHandler.escMenuAction.performed += _ => ToggleMenu();
    }

    private void OnDisable()
    {
        // Clean up to avoid memory leaks
        playerInputHandler.escMenuAction.performed -= _ => ToggleMenu();
        playerInputHandler.escMenuAction.Disable();
    }

    private void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        pauseMenuCanvas.SetActive(isMenuOpen);

        cinemachineFreeLook.enabled = !isMenuOpen;

        // Control the time scale of the game based on menu state
        Time.timeScale = isMenuOpen ? 0 : 1;
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