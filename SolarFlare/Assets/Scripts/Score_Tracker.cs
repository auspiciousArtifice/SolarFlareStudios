using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Tracker : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;

    public static Score_Tracker Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void incrementScoreBy(int amt)
    {
        score += amt;
    }

    public void decrementScoreBy(int amt)
    {
        score -= amt;
    }

    public int getScore()
    {
        return score;
    }

    public int finalScore()
     {
        // incorporate time
        return score;
     }


}
