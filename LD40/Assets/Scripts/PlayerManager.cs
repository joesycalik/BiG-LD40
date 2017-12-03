using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    GameManager manager;
    public Player[] players = new Player[4];

	// Use this for initialization
	void Start () {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));

        //Debuggery
        if (manager == null)
        {
            manager = new GameManager();
        }

        for (int i = 0; i < manager.playerCount; i++)
        {
            players[i].gameObject.SetActive(true);
        }
    }
	
	public void calculateResults()
    {
        manager.gameResults[0] = players[1].playerID;
        manager.gameResults[1] = players[0].playerID;
        
    }
}
