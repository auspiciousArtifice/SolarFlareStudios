using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_GameWin : MonoBehaviour
    {
        public GameObject panelGameWin;

        private void OnEnable()
        {
            GameManager_Master.Instance.GameWinEvent += GameWin;
            if (panelGameWin != null)
            {
                DontDestroyOnLoad(panelGameWin);
            }
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.GameWinEvent -= GameWin;
        }

        private void GameWin()
        {
            Debug.Log("GameWin Called");
            if (panelGameWin != null)
            {
                StartCoroutine(GameWinPanel());
            }
        }

        IEnumerator GameWinPanel()
        {
            yield return new WaitForSeconds(1.5f);
            CanvasGroup canvasGroup = panelGameWin.GetComponent<CanvasGroup>();
            if (canvasGroup)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogWarning("Missing End Game Panel Canvas Group reference");
            }
        }
    }
}
