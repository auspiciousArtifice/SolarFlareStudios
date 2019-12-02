using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenUpdater : MonoBehaviour
{
    public GameObject subtitle;
    public GameObject breakdown;

    Text subtitleText;
    Text breakdownText;

    // Start is called before the first frame update
    void Start()
    {
        subtitleText = subtitle?.GetComponent<Text>();
        breakdownText = breakdown?.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int timeRemaining = (int) UI_Manager.Instance.levelTime;
        int livesRemaining = GameManager_Master.Instance.playerLives;
        int gameScore = Score_Tracker.Instance.getScore();
        int totalPoints = (timeRemaining / 10) + (livesRemaining * 5) + gameScore;
        subtitleText.text = "You won the game with " + totalPoints + " points!";
        breakdownText.text =
            "Current Game Score : " + gameScore + "\n" +
            "Time Remaining Bonus : " + (timeRemaining / 10) + "  << Time Remaining / 10 >>\n" +
            "Lives Remaining Bonus : "+ (livesRemaining * 5)  + " << Lives Remaining * 5 >>";
    }
}
