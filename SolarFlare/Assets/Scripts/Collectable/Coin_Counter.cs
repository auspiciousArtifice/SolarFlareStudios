using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Counter : MonoBehaviour
{
    int coins = 0;

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
        }
    }


}
