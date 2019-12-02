using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Collision : MonoBehaviour
{

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            Score_Tracker.Instance.incrementScoreBy(50);
        }
    }


}