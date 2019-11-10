using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions_UI : MonoBehaviour
{
    float levelTime;
    float LEVEL_TOTAL_TIME = 200;
    float LEVEL_END_TIME = 0;
    int coinNum = 0;
    /*
    GameObject textObj;
    GameObject timer;
    GameObject coinHud;
    GameObject endScreen;
    GameObject character;
    */
    
    InstructionState state;

    public enum InstructionState
    {
        WelcomeInstruction,
        MoveCameraInstruction,
        MovePlayerInstruction,
        SprintInstruction,
        TimerInstruction,
        CoinInstruction,
        EvadeEnemiesInstruction,
        FightEnemiesInstruction,
        ScoreInstruction
    };

    // Start is called before the first frame update

    void Start()
    {
        state = InstructionState.WelcomeInstruction;
        /*
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
        */
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case InstructionState.WelcomeInstruction :

                break;

            case InstructionState.MoveCameraInstruction :

                break;

            case InstructionState.MovePlayerInstruction :

                break;

            case InstructionState.SprintInstruction :

                break;

            case InstructionState.TimerInstruction :

                break;

            case InstructionState.CoinInstruction :

                break;

            case InstructionState.EvadeEnemiesInstruction :

                break;

            case InstructionState.FightEnemiesInstruction :

                break;

            case InstructionState.ScoreInstruction :

                break;

        }

        /*
        // this shouldn't be necessary but... it was buggy so here we are
        textObj = GameObject.FindGameObjectWithTag("Notifications");
        timer = GameObject.FindGameObjectWithTag("timer");
        coinHud = GameObject.FindGameObjectWithTag("coin_hud");
        endScreen = GameObject.FindGameObjectWithTag("end_screen");
        character = GameObject.FindGameObjectWithTag("Player");

        string message = character.gameObject.GetComponent<Rigidbody>().velocity.ToString();


        // makle the text updates
        if (textObj != null) textObj.GetComponent<Text>().text = message;
        if (timer != null) timer.GetComponent<Text>().text = ((int)levelTime).ToString();
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
