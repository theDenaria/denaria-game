using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyCheckManager : MonoBehaviour
{
    public GameObject readyCheckPanel;
    public Button readyButton;
    public Image[] playerReadyCircles;

    public Texture notReadyTexture;
    public Texture readyTexture;

    private bool[] playerReadyStatuses = new bool[10];

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        readyButton.onClick.AddListener(OnReadyButtonClick);
        readyCheckPanel.SetActive(false);

        // Initialize the circles to default (not ready) state
        foreach (var circle in playerReadyCircles)
        {
            circle.texture = notReadyTexture;
        }
    }

    public void StartReadyCheck()
    {
        readyCheckPanel.SetActive(true);

        // Reset all player ready statuses
        for (int i = 0; i < playerReadyStatuses.Length; i++)
        {
            playerReadyStatuses[i] = false;
            UpdatePlayerCircle(i, false);
        }
    }

    private void OnReadyButtonClick()
    {
        //TODO:
        SendReadyStatusToServer();
    }

    public void UpdatePlayerReadyStatuses(int playerId, bool isReady)
    {
        playerReadyStatuses[playerId] = isReady;
        UpdatePlayerCircle(playerId, isReady);
    }

    private void UpdatePlayerCircle(int playerId, bool isReady)
    {
        // Update the image based on the ready status
        if (isReady)
        {
            playerReadyCircles[playerId].texture = readyTexture;
        }
        else
        {
            playerReadyCircles[playerId].texture = notReadyTexture;
        }
    }

    private void SendReadyStatusToServer()
    {
        //TODO:
    }

    public void OnServerUpdateReadyStatus(int playerId, bool isReady)
    {
        UpdatePlayerReadyStatuses(playerId, isReady);
    }
}
