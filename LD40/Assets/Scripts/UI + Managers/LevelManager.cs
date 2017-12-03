using System.Collections;
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
    public bool SinglePlayerMode = false;
    public int GemsToWin = 10;
    public bool gemWinConReached;

    private void Update()
    {
        var hit5Seconds = (timeLeft >= 5) && (timeLeft - Time.deltaTime <= 5);
        if (hit5Seconds) GameSoundManager.instance.PlayCountdown();
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 || gemWinConReached)
        {
            timeLeft = 0;
            playerManager.calculateResults();
            SceneManager.LoadScene("Results");
        }
        timerText.text = timeLeft.ToString("f0");
    }


    public void ResetGems()
    {
        foreach (GemSpawn gemSpawn in gemSpawns)
        {
            gemSpawn.SpawnGem();
        }
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
