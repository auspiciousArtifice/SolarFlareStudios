using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bridge;
    private Animator bridgeAnimator;
    public GameObject player;
    private Animator playerAnimator;
    private bool pushingButton;

    void Start()
    {
        bridgeAnimator = bridge.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IsPushingButton()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ButtonPush"))
        {
            pushingButton = true;
        }
        else
        {
            pushingButton = false;
        }
    }

   // private void OnCollisionEnter(Collision collision)
   // {
       // Debug.Log("Bridge is drawing");
    //    if (pushingButton)
     //   {
            //bridgeAnimator.SetBool("ButtonPressed", true);
    //    }
       
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            bridgeAnimator.SetBool("ButtonPressed", true);
        }
    }
}
