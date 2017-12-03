using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour {

    GameManager manager;
    public Text[] resultsText;

	// Use this for initialization
	void Awake () {
        manager = (GameManager)FindObjectOfType(typeof(GameManager));
        drawResults();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void drawResults()
    {
        for (int i = 0; i < manager.playerCount; i++)
        {
            switch (i)
            {
                case 0:
                    resultsText[i].text = "1st: Player " + manager.gameResults[i];
                    break;

                case 1:
                    resultsText[i].text = "2nd: Player " + manager.gameResults[i];
                    break;

                case 2:
                    resultsText[i].text = "3rd: Player " + manager.gameResults[i];
                    break;

                case 3:
                    resultsText[i].text = "4th: Player " + manager.gameResults[i];
                    break;
            }
            
        }
        
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
