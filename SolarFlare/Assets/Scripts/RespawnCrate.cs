using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCrate : MonoBehaviour
{
    private Vector3 originalTransform;
    // Start is called before the first frame update
    void Start()
    {
        originalTransform = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathFloor")
        {
            transform.position = originalTransform;
        }
    }
}
