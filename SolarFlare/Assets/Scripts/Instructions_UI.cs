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
    bool runTimer = false;
    int stateNum;
    GameObject textObj;
    GameObject timer;
    GameObject[] coins;
	bool ranOnce;
    InstructionState[] states;
    public GameObject enemy;

    InstructionState state;

    public enum InstructionState
    {
        WelcomeInstruction,
        PauseInstruction,
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
        states = 
            new InstructionState[]{
            InstructionState.WelcomeInstruction,
            InstructionState.MoveCameraInstruction,
            InstructionState.PauseInstruction,
            InstructionState.MovePlayerInstruction,
            InstructionState.GrappleInstruction,
            InstructionState.CoinInstruction,
            InstructionState.EvadeEnemiesInstruction,
            InstructionState.AttackSwordInstruction,
            InstructionState.ScoreInstruction};

        state = InstructionState.WelcomeInstruction;
        stateNum = 0;

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
        if (stateNum < states.Length)
        {
            state = states[stateNum];
        }
       
        switch (state) {
            case InstructionState.WelcomeInstruction :
                if (instructionTime < 5)
                {
                    textObj.GetComponent<Text>().text = "Welcome to Solar Flare!";
                }
                else if (instructionTime < 15)
                {
                    textObj.GetComponent<Text>().text = "This is a game where you race to the level endpoint before time runs out!";
                    runTimer = true;
                }
                else if (instructionTime < 30)
                {
                    textObj.GetComponent<Text>().text = "First we'll explain some quick instructions. When you're ready to move on, just press tab!";
                    runTimer = true;
                }
                break;

            case InstructionState.MoveCameraInstruction :
                textObj.GetComponent<Text>().text = "Move your mouse to rotate the game camera.";
                break;

            case InstructionState.PauseInstruction:
                textObj.GetComponent<Text>().text = "Press escape to pause the game and then pause again to resume. The pause menu also has a recap of game instructions!";
                break;

            case InstructionState.MovePlayerInstruction :
                textObj.GetComponent<Text>().text = "Use WASD or arrows to move your game character and space to jump. Using shift will allow you to sprint and right click (or left-alt) will make you dash mid-air.";
                break;das

            case InstructionState.GrappleInstruction:
                textObj.GetComponent<Text>().text = "Click to launch a grappling hook and click again to release yourself! Q and E will shorten your grapple hook.";
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
					ranOnce = false;
				}
                break;

            case InstructionState.EvadeEnemiesInstruction :
                textObj.GetComponent<Text>().text = "You might also have to fight or evade enemies. Press 1 to switch between your grapple hook and your sword";
				if (!ranOnce)
				{
					enemy.SetActive(true);
					ranOnce = true;
				}
                break;

			case InstructionState.AttackSwordInstruction:
				textObj.GetComponent<Text>().text = "Go to the enemy and click in order to attack";
				
				break;

			case InstructionState.ScoreInstruction :
                textObj.GetComponent<Text>().text = "Good Luck!!";
                break;

        }
        if (runTimer)
        {
            levelTime -= Time.deltaTime;
        }
        instructionTime += Time.deltaTime;

        timer = GameObject.FindGameObjectWithTag("timer");
        if (timer != null) timer.GetComponent<Text>().text = "Time Remaining :  " + ((int)levelTime).ToString();
        
        if (Input.GetKeyDown("tab"))
        {
            stateNum++;
        } else if (Input.GetKeyDown("backspace") && stateNum > 0)
        {
            stateNum--;
        }

        if (stateNum == states.Length)
        {
            SceneManager.LoadSceneAsync("MainScene");
            stateNum++;
        }
    }

}
