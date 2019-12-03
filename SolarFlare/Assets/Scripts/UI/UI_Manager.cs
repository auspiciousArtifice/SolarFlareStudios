using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    public float levelTime;
    public float LEVEL_TOTAL_TIME = 300;

    public bool debug;
    private bool restarting;

    GameObject textObj;
    GameObject timer;
    GameObject coinHud;
    GameObject character;
    GameObject lives;

    // Start is called before the first frame update
    public static UI_Manager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        debug = false;
        restarting = false;

        lives = GameObject.FindGameObjectWithTag("Lives");
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        character = GameObject.FindGameObjectWithTag("Player");
        if (lives) lives.GetComponent<Text>().text = "Lives : " + GameManager_Master.Instance.playerLives.ToString();
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
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        character = GameObject.FindGameObjectWithTag("Player");
        lives = GameObject.FindGameObjectWithTag("Lives");

        string message = "";
        if (character && debug) message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();


        // makle the text updates
        if (textObj) textObj.GetComponent<Text>().text = message;
        if (timer) timer.GetComponent<Text>().text = "Time Remaining : " + ((int)levelTime).ToString();
        if (coinHud) coinHud.GetComponent<Text>().text = "Score : " + Score_Tracker.Instance.getScore().ToString();
       
        if (levelTime <= 0 && !restarting)
        {
            restarting = true;
            GameManager_Master.Instance.CallPlayerDied();
        }
        else if (!restarting)
        {
            levelTime -= Time.deltaTime;
        }
    }
}