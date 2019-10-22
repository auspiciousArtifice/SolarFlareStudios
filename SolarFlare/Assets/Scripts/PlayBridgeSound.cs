using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBridgeSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip bridgeDraw;
    private AudioSource bridgeAudioSource;
    private Animator animator;
    private bool hasBeenPlayed = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        bridgeAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(animator.GetBool("ButtonPressed"));
        if (animator.GetBool("ButtonPressed") && !hasBeenPlayed)
        {
            Debug.Log("Audio for bridge.");
            bridgeAudioSource.clip = bridgeDraw;
            bridgeAudioSource.Play();
            hasBeenPlayed = true;
        }
    }
}
