using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    float levelTime = 0;
    float LEVEL_END_TIME = 300;
    int coinNum = 0;
    GameObject textObj;
    GameObject timer;
    GameObject coinHud;
    GameObject endScreen;
    GameObject character;

    // Start is called before the first frame update

    void Start()
    {
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        endScreen = GameObject.FindGameObjectWithTag("end_screen");
        character = GameObject.FindGameObjectWithTag("Player");

        if (textObj == null)
        {
            Debug.Log("Didn't find notifications bar");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        // this shouldn't be necessary but... it was buggy so here we are
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        endScreen = GameObject.FindGameObjectWithTag("end_screen");
        character = GameObject.FindGameObjectWithTag("Player");

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
            message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();
        }

        // makle the text updates
        if (textObj != null) textObj.GetComponent<Text>().text = message;
        if (timer != null) timer.GetComponent<Text>().text = ((int)levelTime).ToString();
        if (character != null) coinNum = character.GetComponent<Coin_Counter>().getCoinCount();
        if (coinHud != null) coinHud.GetComponent<Text>().text = coinNum.ToString();

        endScreen.GetComponent<Text>().text =
            "Congratulations!!\n" +
            "You won the game with " + coinNum + " coins and in " + ((int)levelTime) + " seconds.\n" +
            "Try again to beat this score!";

        levelTime += Time.deltaTime;
        
        if (levelTime > LEVEL_END_TIME)
        {
            // restart level
        }
    }
}
