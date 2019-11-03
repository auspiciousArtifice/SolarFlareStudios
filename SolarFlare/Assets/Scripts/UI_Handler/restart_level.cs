using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class restart_level : MonoBehaviour
{
    public string scene_to_load;

    public void StartGame()
    {
        if (scene_to_load == null || scene_to_load == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        SceneManager.LoadScene(scene_to_load);
    }
}
