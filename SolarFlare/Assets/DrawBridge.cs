using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bridge;
    private Animator bridgeAnimator;

    void Start()
    {
        bridgeAnimator = bridge.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bridge is drawing");
        bridgeAnimator.SetBool("ButtonPressed", true);
    }
}
