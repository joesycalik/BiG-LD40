using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    GameManager manager;
    public Text[] GemCounts;
    public static float timeLeft = 5;
    public Text timerText;
    public PlayerManager playerManager;
    public GemSpawn[] gemSpawns;
    public bool gemWinConReached;

    private void Start()
    {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

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
