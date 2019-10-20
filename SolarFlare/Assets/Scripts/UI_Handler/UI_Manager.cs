﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    float levelTime = 0;
    float LEVEL_END_TIME = 300;
    GameObject textObj;
    GameObject timer;

    // Start is called before the first frame update

    void Start()
    {
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        if (textObj == null)
        {
            Debug.Log("Didn't find notifications bar");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        // this shouldn't be necessary but...
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");

        string message = "";
        if (levelTime < 5)
        {
           message = "Race to the level endpoint by navigating floating islands... ";
        } else if (levelTime < 10)
        {
            message = "Use the arrow keys to move your character... ";
        } else if (levelTime < 15)
        {
            message = "Move the mouse to aim your camera and grappling hook... ";
        } else if (levelTime < 20)
        {
            message = "Press spacebar to jump... ";
        } else if (levelTime < 30) 
        {
            message = "Good Luck!";
        } else
        {
            message = "";
        }


        // makle the text updates
        textObj.GetComponent<Text>().text = message;
        timer.GetComponent<Text>().text = ((int)levelTime).ToString();

        levelTime += Time.deltaTime;
        Debug.Log(levelTime);
        
        if (levelTime > LEVEL_END_TIME)
        {
            // restart level
        }
    }
}
