using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
			if (playerHealth)
			{
				playerHealth.Death();
			}
		}
	}
}
