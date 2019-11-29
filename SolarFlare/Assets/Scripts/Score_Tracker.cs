using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Tracker : MonoBehaviour
{
    // Start is called before the first frame update
    public static int score = 0;

    public static void incrementScoreBy(int amt)
    {
        score += amt;
    }

    public static void decrementScoreBy(int amt)
    {
        score -= amt;
    }

    public static int getScore()
    {
        return score;
    }

    public static int finalScore()
     {
        // incorporate time
        return score;
     }


}
