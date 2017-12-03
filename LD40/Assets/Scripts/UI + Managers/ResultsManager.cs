using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour {

    public Text[] resultsText;
    public RectTransform[] resultsPanel;
    public GameObject gemImageTemplate;

	// Use this for initialization
	void Awake () {
        drawResults();
	}

    void drawResults()
    {
        for (int i = 0; i < 4; i++)
        {
            var isPlayerValid = i >= GameManager.instance.playerCount;
            resultsPanel[i].gameObject.SetActive(isPlayerValid);
            if (isPlayerValid) SetPlayerResults(i);
        }
    }

    void SetPlayerResults(int i)
    {
        var results = GameManager.instance.gameResults[i];
        resultsText[i].text = "Player " + results.playerID;
        for (int j = 0; j < results.gemCount; j++)
        {
            var gem = Instantiate(gemImageTemplate);
            gem.SetActive(true);
            gem.transform.SetParent(resultsPanel[i], false);
        }
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
