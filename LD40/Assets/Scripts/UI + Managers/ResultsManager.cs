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
                    resultsText[i].text = "Player " + GameManager.instance.gameResults[i, 0] + 
                        " - Gems: " + GameManager.instance.gameResults[i, 1];
                    resultsText[i].gameObject.SetActive(true);
                    break;

                case 1:
                    resultsText[i].text = "Player " + GameManager.instance.gameResults[i, 0] +
                        " - Gems: " + GameManager.instance.gameResults[i, 1];
                    resultsText[i].gameObject.SetActive(true);
                    break;

                case 2:
                    resultsText[i].text = "Player " + GameManager.instance.gameResults[i, 0] +
                        " - Gems: " + GameManager.instance.gameResults[i, 1];
                    resultsText[i].gameObject.SetActive(true);
                    break;

                case 3:
                    resultsText[i].text = "Player " + GameManager.instance.gameResults[i, 0] +
                        " - Gems: " + GameManager.instance.gameResults[i, 1];
                    resultsText[i].gameObject.SetActive(true);
                    break;
            }
            
        }
        
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
