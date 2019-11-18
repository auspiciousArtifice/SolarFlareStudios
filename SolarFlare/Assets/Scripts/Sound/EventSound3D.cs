using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventSound3D : MonoBehaviour
{

    public AudioSource audioSrc;

    // Use this for initialization
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (!audioSrc.isPlaying)
        {
            Destroy(this.gameObject);
        }

    }
}
