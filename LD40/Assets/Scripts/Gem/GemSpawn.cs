using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawn : MonoBehaviour {

    public bool occupied = false;
    public Gem gemPrefab;
    public float respawnTimer;
    LevelManager levelManager;

    //To do
    /// <summary>
    /// LevelManager needs to have an array of GemSpawns
    /// Upon level start, each GemSpawn will spawn a child gem
    /// Once a gem is picked up, that specific GemSpawn will begin a countdown. Once that countdown is finished, a brand new child gem will be instantiated
    /// In the gem class, respawn will likely be removed, and a gem will instead be destroyed when it hits the killzone
    /// </summary>


	// Use this for initialization
	void Start () {
        SpawnGem();
        levelManager = FindObjectOfType<LevelManager>();

    }
	
	// Update is called once per frame
	void Update () {
		if (occupied == false && !levelManager.SinglePlayerMode)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer < 0)
            {
                respawnTimer = 0;
                SpawnGem();
            }
        }
	}

    public void SpawnGem()
    {
        if (!occupied)
        {
            var gemGO = Instantiate(gemPrefab, gameObject.transform.position, Quaternion.identity);
            gemGO.GetComponent<Gem>().gemSpawn = this;
            occupied = true;
            respawnTimer = 15;
        } 
    }
}
