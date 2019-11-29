using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GameManager
{
    public class GameManager_ToggleCursor : MonoBehaviour
    {

        private bool isCursorLocked = true;

        private void OnEnable()
        {
            GameManager_Master.Instance.MenuToggleEvent += ToggleCursorState;
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.MenuToggleEvent -= ToggleCursorState;
        }

        private void ToggleCursorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        private void CheckIfCursorLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (GameManager_Master.Instance.isStartMenuScene || GameManager_Master.Instance.isMenuOn)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            /*if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }*/
        }

        private void Update()
        {
            CheckIfCursorLocked();
        }
    }

}