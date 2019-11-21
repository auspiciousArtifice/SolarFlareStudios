using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_On_Collision : MonoBehaviour
{
    public AudioClip cashGrabAudio;
    private AudioSource coinAudio;

    // Start is called before the first frame update
    void Start()
    {
        coinAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cashGrabAudio != null)
        {
            coinAudio.clip = cashGrabAudio;
            coinAudio.Play();
        }
    }
}
