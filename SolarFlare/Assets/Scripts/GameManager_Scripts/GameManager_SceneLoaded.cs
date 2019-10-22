﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_SceneLoaded : MonoBehaviour
    {
        private Transform SpawnPoint;
        public GameObject PlayerPrefab;
        private void OnEnable()
        {
            SpawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            if (PlayerPrefab == null)
            {
                Debug.LogWarning("player prefab not set");
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        }
        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
			if (SpawnPoint != null && PlayerPrefab != null)
			{
				Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
			}
 
            GameManager_Master.Instance.CallLivesUI();
        }

    }
}
