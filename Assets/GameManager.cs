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

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public GameObject playerPrefab;
    private Dictionary<string, Player> players = new();

    public string ownPlayerId;

    public bool isSpawned = false;
    public bool isMenuOpen = false;

    public GameObject bloodEffectPrefab;

    public HUDManager hudManager;

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
        if (!isSpawned)
        {
            clientBehaviour.SendPlayerConnectMessage();
        }
    }

    public void RegisterPlayer(string playerId, Player player)
    {
        if (!players.ContainsKey(playerId))
        {
            players.Add(playerId, player);
        }
    }

    public void UpdatePlayerPosition(string playerId, Vector3 newPosition)
    {
        if (players.ContainsKey(playerId))
        {
            PlayerInterpolation playerInterpolation = players[playerId].GetComponent<PlayerInterpolation>();
            playerInterpolation.OnServerStateUpdate(newPosition);
        }
        else
        {
            SpawnPlayer(playerId, newPosition, 0.0f);
        }
    }

    public void UpdatePlayerRotation(string playerId, Vector4 newRotation)
    {
        if (playerId != ownPlayerId)
        {
            if (players.ContainsKey(playerId))
            {
                // Create quaternion from axis and angle
                Quaternion rotation = new(newRotation.x, newRotation.y, newRotation.z, newRotation.w);
                // Apply the quaternion directly to the player's transform
                players[playerId].transform.rotation = rotation;
            }
            else
            {
                SpawnPlayer(playerId, new Vector3(5.0f, 5.0f, 5.0f), newRotation.y);
            }
        }
    }

    public void Fire(string playerId, Vector3 origin, Vector3 direction)
    {
        if (true || playerId != ownPlayerId) // Todo handle
        {
            if (players.ContainsKey(playerId))
            {
                Shooting shootingScript = players[playerId].GetComponent<Shooting>();
                shootingScript.Shoot(origin, Quaternion.LookRotation(direction));
            }
            else
            {
                Debug.Log("No Player");
            }
        }
    }

    public void Hit(string playerId, string targetId, Vector3 hitPoint)
    {
        if (playerId != ownPlayerId)
        {
            if (players.ContainsKey(playerId))
            {
                Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
            }
            else
            {
                Debug.Log("No Player");
            }
        }
    }

    public void UpdateHealth(string playerId, float hp)
    {

        if (players.ContainsKey(playerId))
        {
            players[playerId].health = hp;
            if (playerId == ownPlayerId)
            {
                hudManager.UpdateHealth(hp);
            }

        }
        else
        {
            Debug.Log("No Player");
        }
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
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        // Set the Cinemachine camera's follow and look at targets

        Transform childTransform = player.transform.Find("PlayerLookAt");
        cinemachineVirtualCamera.Follow = childTransform;

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
        cinemachineVirtualCamera.enabled = !isMenuOpen;

        // Control the time scale of the game based on menu state
        Time.timeScale = isMenuOpen ? 0 : 1;
    }
}