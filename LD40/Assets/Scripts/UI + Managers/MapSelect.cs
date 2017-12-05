using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {

	public void LoadOnClick(int level)
    {
        if (level == 6 || level == 7)
        {
            GameManager.instance.playerCount = 1;
        }
        SceneManager.LoadScene(level);
    }
}
