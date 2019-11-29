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
            
            CanvasGroup canvasGroup = panelGameOver.GetComponent<CanvasGroup>();
            if (canvasGroup)
            {  
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            } else
            {
                Debug.LogWarning("Missing End Game Panel Canvas Group reference");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
