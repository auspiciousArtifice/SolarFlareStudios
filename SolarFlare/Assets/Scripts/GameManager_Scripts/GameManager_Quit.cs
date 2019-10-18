using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_Quit : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager_Master.Instance.QuitGameEvent += QuitGame;
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.QuitGameEvent -= QuitGame;
        }

        private void QuitGame()
        {
            Debug.Log("Quit Succefully");
            Application.Quit();
        }
    }
}