using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class PodScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Game Won");
			GameManager_Master.Instance.CallEventGameWin();
		}
	}
}
