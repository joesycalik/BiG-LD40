using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{

    public Text[] GemCounts = new Text[4];

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
