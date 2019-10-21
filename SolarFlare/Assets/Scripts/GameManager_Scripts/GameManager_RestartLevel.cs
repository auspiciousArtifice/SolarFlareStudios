using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_RestartLevel : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager_Master.Instance.RestartLevelEvent += RestartLevel;
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.RestartLevelEvent -= RestartLevel;
        }

        private void RestartLevel()
        {
            if (GameManager_Master.Instance.isMenuOn)
            {
                GameManager_Master.Instance.CallEventMenuToggle();
            }
        }
    }
}
