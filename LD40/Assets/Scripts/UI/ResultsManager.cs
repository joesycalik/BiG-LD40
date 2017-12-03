using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour {

    public Text[] resultsText;

	// Use this for initialization
	void Awake () {
        drawResults();
	}
	
    void drawResults()
    {
        for (int i = 0; i < GameManager.instance.playerCount; i++)
        {
            switch (i)
            {
                case 0:
                    resultsText[i].text = "1st: Player " + GameManager.instance.gameResults[i];
                    break;

                case 1:
                    resultsText[i].text = "2nd: Player " + GameManager.instance.gameResults[i];
                    break;

                case 2:
                    resultsText[i].text = "3rd: Player " + GameManager.instance.gameResults[i];
                    break;

                case 3:
                    resultsText[i].text = "4th: Player " + GameManager.instance.gameResults[i];
                    break;
            }
            
        }
        
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
