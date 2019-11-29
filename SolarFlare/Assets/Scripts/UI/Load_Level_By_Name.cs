using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Load_Level_By_Name : MonoBehaviour
{
    public string scene_to_load;
    public GameObject toHide;

    public void StartGame()
    {
        if (scene_to_load == null || scene_to_load == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (toHide)
        {
            CanvasGroup canvasGroup = toHide.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.LogWarning("Missing Canvas Group reference to hide on level change");
            } else
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
            }
        }

        if (scene_to_load.Equals("Menu"))
        {
            GameManager_Master.Instance.isGameOver = false;
            GameManager_Master.Instance.isStartMenuScene = true;
            GameManager_Master.Instance.playerLives = 5;
        } else
        {
            GameManager_Master.Instance.isStartMenuScene = false;
        }
        
  
        SceneManager.LoadScene(scene_to_load);
    }
}
