using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_GoToMenuScene : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager_Master.Instance.GoToMenuSceneEvent += GoToMenuScene;
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.GoToMenuSceneEvent -= GoToMenuScene;

        }

        private void GoToMenuScene()
        {
            if (GameManager_Master.Instance.isMenuOn)
            {
                GameManager_Master.Instance.CallEventMenuToggle();
            }
            SceneManager.LoadScene(0);
        }
    }
}