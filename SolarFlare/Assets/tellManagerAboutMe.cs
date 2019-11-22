using UnityEngine;
using GameManager;

public class tellManagerAboutMe : MonoBehaviour
{
	private void Start()
	{
		GameManager_PlayerDied playerDied = GameManager_Master.Instance.GetComponent<GameManager_PlayerDied>();
		playerDied.LivesUI = this.gameObject;
		GameManager_Master.Instance.CallLivesUI();
	}
}
