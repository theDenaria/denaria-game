using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameMode
{
    NormalGame,
    RankedGame
}

public class MatchmakingMenu : MonoBehaviour
{
    public Toggle normalGameToggle;
    public Toggle rankedGameToggle;
    public Button playButton;
    public TextMeshProUGUI playButtonText;

    private bool isMatchmaking = false;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        if (isMatchmaking)
        {
            // Cancel matchmaking
            CancelMatchmaking();
        }
        else
        {
            // Start matchmaking
            if (normalGameToggle.isOn)
            {
                Debug.Log("Starting matchmaking for Normal Game...");
                StartMatchmaking(GameMode.NormalGame);
            }
            else if (rankedGameToggle.isOn)
            {
                Debug.Log("Starting matchmaking for Ranked Game...");
                StartMatchmaking(GameMode.RankedGame);
            }
        }
    }

    private void StartMatchmaking(GameMode gameMode)
    {
        Debug.Log("Matchmaking initiated for: " + gameMode);
        isMatchmaking = true;
        UpdatePlayButton("CANCEL");

        switch (gameMode)
        {
            case GameMode.NormalGame:
                break;
            case GameMode.RankedGame:
                break;
        }
    }

    private void CancelMatchmaking()
    {
        Debug.Log("Matchmaking cancelled.");
        isMatchmaking = false;
        UpdatePlayButton("PLAY");
    }

    private void UpdatePlayButton(string newLabel)
    {
        playButtonText.text = newLabel;
    }
}