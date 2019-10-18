using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_Master : MonoBehaviour
    {
        public static GameManager_Master Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler RestartLevelEvent;
        public event GameManagerEventHandler GoToMenuSceneEvent;
        public event GameManagerEventHandler GameOverEvent;
        public event GameManagerEventHandler QuitGameEvent;
        public event GameManagerEventHandler PlayerDiedEvent;
        public event GameManagerEventHandler LivesUIEvent;

        public bool isGameOver;
        public bool isMenuOn;
        public int  playerLives = 5;


        public void CallEventMenuToggle()
        {
            if (MenuToggleEvent != null)
            {
                MenuToggleEvent();
            }
        }

        public void CallEventRestartLevel()
        {
            if (RestartLevelEvent != null)
            {
                RestartLevelEvent();
            }
        }

        public void CallEventGoToMenuScene()
        {
            if (GoToMenuSceneEvent != null)
            {
                GoToMenuSceneEvent();
            }
        }

        public void CallEventGameOver()
        {
            if (GameOverEvent != null)
            {
                isGameOver = true;
                GameOverEvent();
            }
        }

        public void CallQuitGame()
        {
            if (QuitGameEvent != null)
            {
                QuitGameEvent();
            }
        }

        public void CallPlayerDied()
        {
            if (PlayerDiedEvent != null)
            {
                playerLives--;
                PlayerDiedEvent();
            }
        }

        public void CallLivesUI()
        {
            if (LivesUIEvent != null)
            {
                LivesUIEvent();
            }
        }
    }
}
