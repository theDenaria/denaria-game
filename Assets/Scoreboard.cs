using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using System.Linq;

public class Scoreboard: MonoBehaviour
{
    public bool testMode = false;

    private Dictionary<string, ScoreDetails> teamA;
    private Dictionary<string, ScoreDetails> teamB;
    
    public List<TextMeshProUGUI> playerNameTextsA;   // UI Texts to display player names
    public List<TextMeshProUGUI> playerKillTextsA;   // UI Texts to display kills
    public List<TextMeshProUGUI> playerAssistTextsA; // UI Texts to display assists
    public List<TextMeshProUGUI> playerDeathTextsA;  // UI Texts to display deaths
    public List<TextMeshProUGUI> playerScoreTextsA;  // UI Texts to display scores
    
    public List<TextMeshProUGUI> playerNameTextsB;   // UI Texts to display player names
    public List<TextMeshProUGUI> playerKillTextsB;   // UI Texts to display kills
    public List<TextMeshProUGUI> playerAssistTextsB; // UI Texts to display assists
    public List<TextMeshProUGUI> playerDeathTextsB;  // UI Texts to display deaths
    public List<TextMeshProUGUI> playerScoreTextsB;  // UI Texts to display scores


    private void Start()
    {
        Debug.Log("Start called!!!");
        this.teamA = new Dictionary<string, ScoreDetails>();
        this.teamB = new Dictionary<string, ScoreDetails>();
        
        string[] teamASample = { "player1", "player2", "player3", "player4", "player5"};
        string[] teamBSample = {"player6", "player7", "player8", "player9", "player0"};
        
        InitializePlayers(teamASample, teamBSample);
    }
    
    private void Update()
    {
        if (!testMode) return;
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                PlayerUpdate("player1", "kill");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            
        }
        if (Input.GetKey(KeyCode.Alpha3) )
        {
            Debug.Log("W and Left Shift are being held down.");
        }
        if (Input.GetKey(KeyCode.Alpha4) )
        {
            Debug.Log("W and Left Shift are being held down.");
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                PlayerUpdate("player5", "kill");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                
            }
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Debug.Log("W and Left Shift are being held down.");
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            Debug.Log("W and Left Shift are being held down.");
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            Debug.Log("W and Left Shift are being held down.");
        }
        if (Input.GetKey(KeyCode.Alpha9) )
        {
            Debug.Log("W and Left Shift are being held down.");
        }
        if (Input.GetKey(KeyCode.Alpha0) )
        {
            Debug.Log("W and Left Shift are being held down.");
        }
    
    }


    public void InitializePlayers(string[] teamAInput, string[] teamBInput)
    {
        foreach (var playerName in teamAInput)
        {
            this.teamA.Add(playerName, new ScoreDetails(playerName));
        }
        
        foreach (var playerName in teamBInput)
        {
            this.teamB.Add(playerName, new ScoreDetails(playerName));
        }
        UpdateScoreboardUI();
    }

    public void PlayerUpdate(string playerName, string action)
    {
        if (teamA.TryGetValue(playerName, out ScoreDetails playerA))
        {
            switch (action)
            {
                case "kill":
                    playerA.addKill();
                    break;
                case "death":
                    playerA.addDeath();
                    break;
                case "assist":
                    playerA.addAssist();
                    break;
                default: 
                    Debug.Log("invalid action");
                    break;
            }
        } else if (teamB.TryGetValue(playerName, out ScoreDetails playerB))
        {
            switch (action)
            {
                case "kill":
                    playerB.addKill();
                    break;
                case "death":
                    playerB.addDeath();
                    break;
                case "assist":
                    playerB.addAssist();
                    break;
                default: 
                    Debug.Log("invalid action");
                    break;
            }
        }

        UpdateScoreboardUI();
    }
    
    void UpdateScoreboardUI()
    {
        var teamASorted = teamA.OrderByDescending(kvp => kvp.Value.score).ToList();
        var teamBSorted  = teamB.OrderByDescending(kvp => kvp.Value.score).ToList();

        for (int i = 0; i < teamASorted.Count; i++)
        {
            if (i < playerNameTextsA.Count)
            {
                playerNameTextsA[i].text = teamASorted[i].Value.playerName;
                playerKillTextsA[i].text = teamASorted[i].Value.kill.ToString();
                playerAssistTextsA[i].text = teamASorted[i].Value.assist.ToString();
                playerDeathTextsA[i].text = teamASorted[i].Value.death.ToString();
                playerScoreTextsA[i].text = teamASorted[i].Value.score.ToString();
            }
        }
        
        for (int i = 0; i < teamBSorted.Count; i++)
        {
            if (i < playerNameTextsB.Count)
            {
                playerNameTextsB[i].text = teamBSorted[i].Value.playerName;
                playerKillTextsB[i].text = teamBSorted[i].Value.kill.ToString();
                playerAssistTextsB[i].text = teamBSorted[i].Value.assist.ToString();
                playerDeathTextsB[i].text = teamBSorted[i].Value.death.ToString();
                playerScoreTextsB[i].text = teamBSorted[i].Value.score.ToString();
            }
        }
    }
    
    
    

    
}

// var scoreboard = new Scoreboard(......)