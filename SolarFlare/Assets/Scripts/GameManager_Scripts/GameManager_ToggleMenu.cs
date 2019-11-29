using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_ToggleMenu : MonoBehaviour
    {

        public GameObject PauseMenu;

        private void OnEnable()
        {
			//ToggleMenu();
			if (PauseMenu == null)
			{
				Debug.LogWarning("Missing Pause Menu reference");
			}
            DontDestroyOnLoad(PauseMenu);
            Time.timeScale = 1;
            GameManager_Master.Instance.MenuToggleEvent += ToggleMenu;

        }

        private void Update()
        {
            CheckForMenuToggleRequest();
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.MenuToggleEvent -= ToggleMenu;
        }

        private void CheckForMenuToggleRequest()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !GameManager_Master.Instance.isStartMenuScene && !GameManager_Master.Instance.isGameOver && SceneManager.GetActiveScene().buildIndex != 0)
            {
                GameManager_Master.Instance.CallEventMenuToggle();
            }
        }

        private void ToggleMenu()
        {
            if (PauseMenu != null)
            {
                //PauseMenu.SetActive(!PauseMenu.activeSelf);
                CanvasGroup canvasGroup = PauseMenu.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    Debug.LogWarning("Missing Pause Menu Canvas Group reference");
                    return;
                }
                if (canvasGroup.interactable)
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;
                    Time.timeScale = 1f;
                }
                else
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                    Time.timeScale = 0f;
                }
                GameManager_Master.Instance.isMenuOn = !GameManager_Master.Instance.isMenuOn;
            }
            else
            {
                Debug.LogWarning("Need to assign UI GameObject to Toggle Menu script in the inspector");
            }
        }
    }
}