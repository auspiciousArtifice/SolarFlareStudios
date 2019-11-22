using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bridge;
    private Animator bridgeAnimator;
    private Animator playerAnimator;
    private bool pushingButton = false;

    void Start()
    {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player)
		{
			playerAnimator = player.GetComponent<Animator>();
			if (!playerAnimator)
			{
				Debug.LogWarning("no player animator");
			}
		}
		else
		{
			Debug.LogWarning("no player found");
		}
        bridgeAnimator = bridge.GetComponent<Animator>();
		if (!bridgeAnimator)
		{
			Debug.LogWarning("no bridge animator");
		}
    }

    private void OnTriggerStay(Collider other)
    {
		if (other.CompareTag("Player") && Input.GetButtonDown("PushButton"))
		{
			bridgeAnimator.SetBool("ButtonPressed", pushingButton);
		}
    }
}
