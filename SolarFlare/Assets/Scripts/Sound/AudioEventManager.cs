using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;

    public AudioClip enemyAttackAudio = null;


    private UnityAction<Vector3> enemyAttackEventListener;


    void Awake()
    {

        enemyAttackEventListener = new UnityAction<Vector3>(enemyAttackEventHandler);

    }


    // Use this for initialization
    void Start()
    {



    }


    void OnEnable()
    {

        EventManager.StartListening<EnemyAttackEvent, Vector3>(enemyAttackEventListener);

    }

    void OnDisable()
    {

        EventManager.StopListening<EnemyAttackEvent, Vector3>(enemyAttackEventListener);
    }





    void enemyAttackEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.enemyAttackAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }

    }
}