using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachPlayer : MonoBehaviour
{

	private GrapplingHook grappling;
	private GameObject player;
	private bool attached;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (player)
		{
			grappling = player.GetComponent<GrapplingHook>();
		}
		if (!grappling)
		{
			Debug.LogWarning("No grappling hook script found");
		}
	}

	private void Update()
	{
		if (attached)
		{
			if (grappling)
			{
				if (grappling.hooked)
				{
					player.transform.parent = null;
					attached = false;
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.parent = transform;
			attached = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.parent = null;
			attached = false;
		}
	}
}
