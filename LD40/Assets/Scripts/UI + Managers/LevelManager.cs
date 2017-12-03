using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public Text[] GemCounts;
    public Text GemsLeft;
    public float timeLeft = 5;
    public Text timerText;
    public PlayerManager playerManager;
    public GemSpawn[] gemSpawns;
    public bool SinglePlayerMode = false;
    public int GemsToWin = 10;
    public bool gemWinConReached;

    private void Start()
    {
        if (GemsLeft != null) GemsLeft.text = GemsToWin.ToString();
        for (int i = 0; i < 4; i++)
        {
            var ui = GameObject.Find("Level UI/Panel/Player " + (i + 1));
            if (ui != null) ui.SetActive(i < GameManager.instance.playerCount);
        }
    }

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

    public void UpdateGemsLeft(int gems)
    {
        if (GemsLeft != null) GemsLeft.text = (GemsToWin - gems).ToString();
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
