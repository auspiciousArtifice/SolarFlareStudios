﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class restart_level : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync ("MainScene");
    }
}
