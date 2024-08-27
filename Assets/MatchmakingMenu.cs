using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    public TextMeshProUGUI queueTimerText;

    private bool isMatchmaking = false;
    private float elapsedTime = 0f;

    private Coroutine timerCoroutine;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        queueTimerText.gameObject.SetActive(false);  // Hide the timer at the start
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

        // Show the timer and start it
        queueTimerText.gameObject.SetActive(true);
        elapsedTime = 0f;
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private void CancelMatchmaking()
    {
        Debug.Log("Matchmaking cancelled.");
        isMatchmaking = false;
        UpdatePlayButton("PLAY");

        // Stop the timer and hide it
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        queueTimerText.gameObject.SetActive(false);
        queueTimerText.text = "Queue Time: 00:00";  // Reset the timer display
    }

    private void UpdatePlayButton(string newLabel)
    {
        playButtonText.text = newLabel;
    }

    private IEnumerator UpdateTimer()
    {
        while (isMatchmaking)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            queueTimerText.text = string.Format("Queue Time: {0:00}:{1:00}", minutes, seconds);

            yield return null;  // Wait for the next frame
        }
    }
}