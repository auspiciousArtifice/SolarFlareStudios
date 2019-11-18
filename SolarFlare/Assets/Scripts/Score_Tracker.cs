using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Tracker : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;

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
        this.decrementScoreBy(0);
        return score;
     }


}
