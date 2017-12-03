﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Respawn();
        }
        else if (other.CompareTag("Gem"))
        {
            Destroy(other.GetComponent<Gem>().gameObject);
        }
    }
}
