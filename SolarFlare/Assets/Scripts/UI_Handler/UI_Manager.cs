using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    float levelTime;
    float LEVEL_TOTAL_TIME = 200;
    float LEVEL_END_TIME = 0;
    int coinNum = 0;

    GameObject textObj;
    GameObject timer;
    GameObject coinHud;
    GameObject endScreen;
    GameObject character;
    GameObject score;

    // Start is called before the first frame update

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("score");
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        endScreen = GameObject.FindGameObjectWithTag("end_screen");
        character = GameObject.FindGameObjectWithTag("Player");

        levelTime = LEVEL_TOTAL_TIME;

        if (textObj == null)
        {
            Debug.Log("Didn't find notifications bar");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        // this shouldn't be necessary but... it was buggy so here we are
        score = GameObject.FindGameObjectWithTag("score");
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        endScreen = GameObject.FindGameObjectWithTag("end_screen");
        character = GameObject.FindGameObjectWithTag("Player");
        
        string message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();
        

        // makle the text updates
        if (textObj != null) textObj.GetComponent<Text>().text = message;
        if (timer != null) timer.GetComponent<Text>().text = ((int)levelTime).ToString();
        if (coinHud != null) coinHud.GetComponent<Text>().text = score.GetComponent<Score_Tracker>().getScore().ToString();

        endScreen.GetComponent<Text>().text =
            "Congratulations!!\n" +
            "You won the game with score of " + score.GetComponent<Score_Tracker>().getScore() + ".\n" +
            "Try again to beat this score!";

        int oldLevelTime = (int) levelTime;
        levelTime -= Time.deltaTime;
        score.GetComponent<Score_Tracker>().decrementScoreBy(oldLevelTime - (int)levelTime);

        if (levelTime < LEVEL_END_TIME)
        {
            // restart level
        }
    }
}
