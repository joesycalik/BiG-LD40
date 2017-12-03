﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public Text[] GemCounts;
    public float timeLeft = 5;
    public Text timerText;
    public PlayerManager playerManager;
    public GemSpawn[] gemSpawns;
    public bool gemWinConReached;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 || gemWinConReached)
        {
            timeLeft = 0;
            playerManager.calculateResults();
            SceneManager.LoadScene("Results");
        }
        timerText.text = timeLeft.ToString("f0");
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}