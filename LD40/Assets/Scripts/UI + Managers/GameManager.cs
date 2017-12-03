using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var go = new GameObject("GameManager");
                m_instance = go.AddComponent<GameManager>();
            }
            return m_instance;
        }
    }
    public int playerCount = 2;
    public int[] gameResults;


    private void Update()
    {
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    } //End Awake
} //End class

