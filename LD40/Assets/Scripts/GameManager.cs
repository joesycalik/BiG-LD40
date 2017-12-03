using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int playerCount = 2;
    GameState state = GameState.MainMenu;
    public int[] gameResults;


    private void Update()
    {
        if (state == GameState.InGame && LevelManager.timeLeft <= 0)
        {
            Time.timeScale = 0;

        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    } //End Awake
} //End class

enum GameState
{
    MainMenu, InGame, Results 
}

