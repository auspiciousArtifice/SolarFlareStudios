using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_SceneLoaded : MonoBehaviour
    {
        private void OnEnable()
        {

            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        }
        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            GameManager_Master.Instance.CallLivesUI();
        }

    }
}
