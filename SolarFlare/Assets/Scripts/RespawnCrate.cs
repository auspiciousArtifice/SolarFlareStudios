using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCrate : MonoBehaviour
{
    private Vector3 originalTransform;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalTransform = transform.position;
        originalRotation = transform.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathFloor")
        {
            transform.position = originalTransform;
            transform.rotation = originalRotation;
        }
    }
}
