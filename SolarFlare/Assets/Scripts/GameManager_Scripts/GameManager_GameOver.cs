using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_GameOver : MonoBehaviour
    {
        public GameObject panelGameOver;

        private void OnEnable()
        {
            GameManager_Master.Instance.GameOverEvent += GameOver;
			if (panelGameOver != null)
			{
				DontDestroyOnLoad(panelGameOver);
			}
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.GameOverEvent -= GameOver;
        }

        private void GameOver()
        {
            if (panelGameOver != null)
            {
                StartCoroutine(GameOverPanel());
            }
        }

        IEnumerator GameOverPanel()
        {
            yield return new WaitForSeconds(0.5f);
            panelGameOver.SetActive(true);
        }
    }
}
