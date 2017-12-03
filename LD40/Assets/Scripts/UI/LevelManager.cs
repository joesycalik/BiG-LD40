using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    GameManager manager;
    public Text[] GemCounts;
    public static float timeLeft = 120;
    public Text timerText;
    PlayerManager playerManager;
    public GemSpawn[] gemSpawns;

    private void Start()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        //manager.gameResults = new int[manager.playerCount];
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            //playerManager.calculateResults();
            SceneManager.LoadScene("Results");
        }
        timerText.text = timeLeft.ToString("f0");
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
