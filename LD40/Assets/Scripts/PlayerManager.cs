using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Player[] players = new Player[4];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameManager.instance.playerCount; i++)
        {
            players[i].gameObject.SetActive(true);
        }
    }

    public void calculateResults()
    {
        int[] gameResults = new int[GameManager.instance.playerCount];
        gameResults[0] = players[1].playerID;
        gameResults[1] = players[0].playerID;

        GameManager.instance.gameResults = gameResults;
    }
}
