﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameManager
{
    public class GameManager_PlayerDied : MonoBehaviour
    {
        public GameObject LivesUI;

        private void OnEnable()
        {
            GameManager_Master.Instance.PlayerDiedEvent += PlayerDied;
            GameManager_Master.Instance.LivesUIEvent += UpdateUI;

			if (LivesUI == null)
			{
				Debug.LogWarning("missing UI reference for life");
			}
			else
			{
				DontDestroyOnLoad(LivesUI.gameObject);
			}
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.PlayerDiedEvent -= PlayerDied;
            GameManager_Master.Instance.LivesUIEvent -= UpdateUI;

        }

        private void PlayerDied()
        {
            GameManager_Master.Instance.CallLivesUI();
            StartCoroutine(Dead());
        }

        IEnumerator Dead()
        {
            Debug.Log("dead");
            yield return new WaitForSeconds(5);
            GameManager_Master.Instance.CallEventRestartLevel();
        }

        private void UpdateUI()
        {
			if (LivesUI != null && LivesUI.GetComponent<Text>() != null)
			{
				LivesUI.GetComponent<Text>().text = "Lives : " + GameManager_Master.Instance.playerLives.ToString();
			}
        }
    }
}
