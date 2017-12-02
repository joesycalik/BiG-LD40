using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour {

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
