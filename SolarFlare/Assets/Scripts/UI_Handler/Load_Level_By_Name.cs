using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Load_Level_By_Name : MonoBehaviour
{
    public string scene_to_load;

    public void StartGame()
    {
        if (scene_to_load == null || scene_to_load == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        GameManager_Master.Instance.isStartMenuScene = false;
        SceneManager.LoadScene(scene_to_load);
    }
}
