using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetails
{
    public string playerName;
    public int kill;
    public int assist;
    public int death;
    public int score;

    public ScoreDetails(string playerName)
    {
        this.playerName = playerName;
        kill = 0;
        assist = 0;
        death = 0;
        CalculateScore();
    }

    public void addKill()
    {
        kill += 1;
        CalculateScore();
    }

    public void addAssist()
    {
        assist += 1;
        CalculateScore();
    }

    public void addDeath()
    {
        death += 1;
        CalculateScore();
    }
    private void CalculateScore()
    {
        // Define your scoring logic here. For example:
        score = (kill * 2) + (assist) - (death);
    }
    
    
}

