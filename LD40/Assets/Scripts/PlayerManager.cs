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
        Results[] gameResults = new Results[GameManager.instance.playerCount];

        for (int i = 0; i < GameManager.instance.playerCount; i++)
        {
            gameResults[i] = new Results(players[i].playerID, players[i].gems.Count);
        }

        Results temp = null;

        for (int i = 0; i < gameResults.Length; i++)
        {
            for (int j = 0; j < gameResults.Length - 1; j++)
            {
                if (gameResults[j].gemCount < gameResults[j + 1].gemCount)
                {
                    temp = gameResults[j + 1];
                    gameResults[j + 1] = gameResults[j];
                    gameResults[j] = temp;
                }
            }
        }
        GameManager.instance.gameResults = gameResults;
    }
}

public class Results{
    public int playerID, gemCount;

    public Results(int ID, int count)
    {
        playerID = ID;
        gemCount = count;
    }
}
