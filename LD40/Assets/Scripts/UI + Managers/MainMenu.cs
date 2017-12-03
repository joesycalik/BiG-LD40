using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text playerCountText;


    void Start()
    {
        GameManager.instance.playerCount = 2;
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void SetPlayerCount(float val)
    {
        GameManager.instance.playerCount = (int) val;
        playerCountText.text = "Player Count: " + val;
    }
}
