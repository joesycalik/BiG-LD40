using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerCount;

    void Awake()
    {
        DontDestroyOnLoad(this);
    } //End Awake
} //End class

