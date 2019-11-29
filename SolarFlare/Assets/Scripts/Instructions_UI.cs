using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions_UI : MonoBehaviour
{
    float levelTime;
    float instructionTime;
    float LEVEL_TOTAL_TIME = 65;
    //int coinNum = 0;
    bool runTimer = false;
    GameObject textObj;
    GameObject timer;
    //GameObject coinHud;
    GameObject[] coins;
    //GameObject character;
	bool ranOnce;

    public GameObject enemy;
    /*
    GameObject endScreen;
    */

    InstructionState state;

    public enum InstructionState
    {
        WelcomeInstruction,
        MoveCameraInstruction,
        MovePlayerInstruction,
        GrappleInstruction,
        CoinInstruction,
        EvadeEnemiesInstruction,
		AttackSwordInstruction,
		ScoreInstruction
    };

    void Start()
    {
        state = InstructionState.WelcomeInstruction;
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        //coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        //character = GameObject.FindGameObjectWithTag("Player");
        coins = GameObject.FindGameObjectsWithTag("PickUp");
        levelTime = LEVEL_TOTAL_TIME;
        instructionTime = 0;
        enemy.SetActive(false);
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].SetActive(false);
        }
        if (textObj == null)
        {
            Debug.Log("Didn't find notifications bar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case InstructionState.WelcomeInstruction :
                if (instructionTime < 5)
                {
                    textObj.GetComponent<Text>().text = "Welcome to Solar Flare Studios!";
                }
                else if (instructionTime < 10)
                {
                    textObj.GetComponent<Text>().text = "This is a game where you race to the level endpoint before time runs out!";
                    runTimer = true;
                }
                else if (instructionTime < 20)
                {
                    textObj.GetComponent<Text>().text = "First we'll explain some quick instructions. When you're ready to move on, just press tab!";
                    runTimer = true;
                }

                if (Input.GetKeyDown("tab"))
                {
                    state = InstructionState.MoveCameraInstruction;
                }
                break;

            case InstructionState.MoveCameraInstruction :
                textObj.GetComponent<Text>().text = "Move your mouse to rotate the game camera.";
                if (Input.GetKeyDown("tab"))
                {
                   state = InstructionState.MovePlayerInstruction;
                }
                break;

            case InstructionState.MovePlayerInstruction :
                textObj.GetComponent<Text>().text = "Use arrows to move your game character and space to jump. Using shift will allow you to sprint and alt will make you dash mid-air.";
                if (Input.GetKeyDown("tab"))
                {
                    state = InstructionState.GrappleInstruction;
                }
                break;

            case InstructionState.GrappleInstruction:
                textObj.GetComponent<Text>().text = "Click to launch a grappling hook and click again to release yourself! Q and E will shorten your grapple hook.";
                if (Input.GetKeyDown("tab"))
                {
                    state = InstructionState.CoinInstruction;
                }
                break;

            case InstructionState.CoinInstruction :
                textObj.GetComponent<Text>().text = "Collect coins along the way to add to your final score!";
				if (!ranOnce)
				{
					for (int i = 0; i < coins.Length; i++)
					{
						coins[i].SetActive(true);
					}
					ranOnce = true;
				}
                if (Input.GetKeyDown("tab"))
                {
                    state = InstructionState.EvadeEnemiesInstruction;
					ranOnce = false;
				}
                break;

            case InstructionState.EvadeEnemiesInstruction :
                textObj.GetComponent<Text>().text = "You might also have to fight or evade enemies. Click 1 to switch between your grapple hook and your sword";
				if (!ranOnce)
				{
					enemy.SetActive(true);
					ranOnce = true;
				}
                if (Input.GetKeyDown("tab"))
                {
                    state = InstructionState.AttackSwordInstruction;
                }
                break;

			case InstructionState.AttackSwordInstruction:
				textObj.GetComponent<Text>().text = "Go to the enemy and click in order to attack";
				if (Input.GetKeyDown("tab"))
				{
					state = InstructionState.ScoreInstruction;
				}
				break;

			case InstructionState.ScoreInstruction :
                textObj.GetComponent<Text>().text = "Good Luck!!";
                if (Input.GetKeyDown("tab"))
                {
                    SceneManager.LoadSceneAsync("MainScene");
                }
                break;

        }
        if (runTimer)
        {
            levelTime -= Time.deltaTime;
        }
        instructionTime += Time.deltaTime;

        timer = GameObject.FindGameObjectWithTag("timer");
        if (timer != null) timer.GetComponent<Text>().text = "Time Remaining :  " + ((int)levelTime).ToString();

        /*
        // this shouldn't be necessary but... it was buggy so here we are
        textObj = GameObject.FindGameObjectWithTag("Notifications");

        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        endScreen = GameObject.FindGameObjectWithTag("end_screen");
        character = GameObject.FindGameObjectWithTag("Player");

        string message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();


        // makle the text updates
        if (textObj != null) textObj.GetComponent<Text>().text = message;

        if (character != null) coinNum = character.GetComponent<Coin_Counter>().getCoinCount() * 5;
        if (coinHud != null) coinHud.GetComponent<Text>().text = coinNum.ToString();

        endScreen.GetComponent<Text>().text =
            "Congratulations!!\n" +
            "You won the game with score of " + finalScore() + ".\n" +
            "Try again to beat this score!";

        levelTime -= Time.deltaTime;

        if (levelTime < LEVEL_END_TIME)
        {
            // restart level
        }
        */
    }

}
