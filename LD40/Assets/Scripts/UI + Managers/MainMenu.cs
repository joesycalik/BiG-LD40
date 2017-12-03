﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    GameManager manager;

    public Text playerCountText;


    void Start()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        manager.playerCount = 2;
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void SetPlayerCount(float val)
    {
        manager.playerCount = (int) val;
        playerCountText.text = "Player Count: " + val;
    }
}