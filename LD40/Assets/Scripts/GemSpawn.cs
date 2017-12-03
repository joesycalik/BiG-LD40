using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawn : MonoBehaviour {

    public bool occupied = false;
    public Gem gem;
    public float respawnTimer;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (occupied == false)
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
            Instantiate(gem, gameObject.transform.position, Quaternion.identity);
            gem.gemSpawn = this;
            occupied = true;
            respawnTimer = 15;
        } 
    }
}
