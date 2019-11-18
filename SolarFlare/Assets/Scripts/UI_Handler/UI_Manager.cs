﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    float levelTime;
    float LEVEL_TOTAL_TIME = 200;

    public bool debug;

    GameObject textObj;
    GameObject timer;
    GameObject coinHud;
    GameObject endScreen;
    GameObject character;
    GameObject score;

    // Start is called before the first frame update

    void Start()
    {
        debug = true;
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

        string message = "";
        if (character != null && debug) message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();
        

        // makle the text updates
        if (textObj != null) textObj.GetComponent<Text>().text = message;
        if (timer != null) timer.GetComponent<Text>().text = "Time Remaining : " + ((int)levelTime).ToString();
        if (coinHud != null) coinHud.GetComponent<Text>().text = "Score : " + score.GetComponent<Score_Tracker>().getScore().ToString();

        int finalScore = score.GetComponent<Score_Tracker>().getScore() + (int)levelTime;

        endScreen.GetComponent<Text>().text =
            "Congratulations!!\n" +
            "You won the game with score of " + finalScore + ".\n" +
            "Try again to beat this score!";

        levelTime -= Time.deltaTime;

    }
}
