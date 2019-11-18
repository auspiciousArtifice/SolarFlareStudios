using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Tracker : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
