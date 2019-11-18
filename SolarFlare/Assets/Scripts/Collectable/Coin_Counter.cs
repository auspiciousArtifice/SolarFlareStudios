using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Counter : MonoBehaviour
{
    int coins = 0;
    public AudioClip cashGrabAudio;
    private AudioSource coinAudio;
    public GameObject score;

    void Start()
    {
        coinAudio = GetComponent<AudioSource>();
        score = GameObject.FindGameObjectWithTag("score");
    }
    public int getCoinCount()
    {
        return coins;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            coins++;
            coinAudio.clip = cashGrabAudio;
            coinAudio.Play();
            score.GetComponent<Score_Tracker>().incrementScoreBy(5);
        }
    }


}
