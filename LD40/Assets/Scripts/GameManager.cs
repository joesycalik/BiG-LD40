using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerCount = 2;

    void Awake()
    {
        DontDestroyOnLoad(this);
    } //End Awake
} //End class

