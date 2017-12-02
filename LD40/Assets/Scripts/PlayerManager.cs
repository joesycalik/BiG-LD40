using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    GameManager manager;
    public Player[] players = new Player[4];

	// Use this for initialization
	void Start () {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        for (int i = 0; i < manager.playerCount; i++)
        {
            players[i].gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
