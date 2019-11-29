using GameManager;
using System.Collections;
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
    GameObject character;

    // Start is called before the first frame update

    void Start()
    {
        debug = false;
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
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
        if (levelTime <= 0)
        {
            GameManager_Master.Instance.CallEventGameOver();
        }

        // this shouldn't be necessary but... it was buggy so here we are
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        character = GameObject.FindGameObjectWithTag("Player");

        string message = "";
        if (character && debug) message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();
        

        // makle the text updates
        if (textObj) textObj.GetComponent<Text>().text = message;
        if (timer) timer.GetComponent<Text>().text = "Time Remaining : " + ((int)levelTime).ToString();
        if (coinHud) coinHud.GetComponent<Text>().text = "Score : " + Score_Tracker.Instance.getScore().ToString();
        levelTime -= Time.deltaTime;
    }
}