using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_SceneLoaded : MonoBehaviour
    {
        public Transform SpawnPoint;
        public GameObject PlayerPrefab;
        private void OnEnable()
        {
            if (SpawnPoint == null)
            {
                Debug.LogWarning("spawn point not set");
            }

            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            if (PlayerPrefab == null)
            {
                Debug.LogWarning("player prefab not set");
            }
            DontDestroyOnLoad(SpawnPoint.gameObject);
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
