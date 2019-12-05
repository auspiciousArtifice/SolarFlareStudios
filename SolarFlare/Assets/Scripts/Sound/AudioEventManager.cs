using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;

    public AudioClip enemyDamageAudio = null;
    public AudioClip enemyAttackAudio = null;
    public AudioClip grappleLandAudio = null;


    private UnityAction<Vector3> enemyAttackEventListener;
    private UnityAction<Vector3> enemyDamageEventListener;
    private UnityAction<Vector3> grappleLandEventListener;


    void Awake()
    {

        enemyAttackEventListener = new UnityAction<Vector3>(enemyAttackEventHandler);
        enemyDamageEventListener = new UnityAction<Vector3>(enemyDamageSoundEventHandler);
        grappleLandEventListener = new UnityAction<Vector3>(grappleLandSoundEventHandler);
    }


    // Use this for initialization
    void Start()
    {
     
    }


    void OnEnable()
    {

        EventManager.StartListening<EnemyAttackEvent, Vector3>(enemyAttackEventListener);
        EventManager.StartListening<EnemyDamageEvent, Vector3>(enemyDamageEventListener);
        EventManager.StartListening<GrappleLandEvent, Vector3>(grappleLandEventListener);
    }

    void OnDisable()
    {

        EventManager.StopListening<EnemyAttackEvent, Vector3>(enemyAttackEventListener);
        EventManager.StopListening<EnemyDamageEvent, Vector3>(enemyDamageEventListener);
        EventManager.StopListening<GrappleLandEvent, Vector3>(grappleLandEventListener);
    }


    void grappleLandSoundEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.grappleLandAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }

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

    void enemyDamageSoundEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.enemyDamageAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }

    }
}