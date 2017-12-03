using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public const int MaxPlayers = 4;
    public Player playerPrefab;
    public GameObject[] playerSpawnpoints = new GameObject[MaxPlayers];
    public Player[] players = new Player[MaxPlayers];

    void Start()
    {
        for (int i = 0; i < MaxPlayers; i++)
        {
            playerSpawnpoints[i].SetActive(false);
        }
        for (int i = 0; i < GameManager.instance.playerCount; i++)
        {
            players[i] = Instantiate(playerPrefab, new Vector3(playerSpawnpoints[i].transform.position.x, playerSpawnpoints[i].transform.position.y, 0), playerSpawnpoints[i].transform.rotation);
            players[i].SetPlayer(i + 1);
        }
    }

    public void calculateResults()
    {
        int[,] gameResults = new int[GameManager.instance.playerCount, 2];
        
        for (int i = 0; i < GameManager.instance.playerCount; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (gameResults[j, 0] == 0)
                {
                    gameResults[j, 0] = players[i].playerID;
                    gameResults[j, 1] = players[i].gems.Count;
                } else if (players[i].gems.Count > gameResults[j, 1])
                {
                    gameResults[j + 1, 0] = gameResults[j, 0];
                    gameResults[j + 1, 1] = gameResults[j, 1];

                    gameResults[j, 0] = players[i].playerID;
                    gameResults[j, 1] = players[i].gems.Count;
                }
            }
        }

        GameManager.instance.gameResults = gameResults;
    }
}
