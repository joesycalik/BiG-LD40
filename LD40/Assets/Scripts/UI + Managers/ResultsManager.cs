using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour {

    public Text[] resultsText;
    public RectTransform[] resultsPanel;
    public GameObject gemImageTemplate;
    public Text[] statsPlayerText;
    public Text[] statsGemsText;
    public Text[] statsHitsGivenText;
    public Text[] statsHitsTakenText;
    public Text[] statsDeathsText;

    // Use this for initialization
    void Awake () {
        drawResults();
	}

    void drawResults()
    {
        for (int i = 0; i < 4; i++)
        {
            var isPlayerValid = i < GameManager.instance.playerCount;
            resultsPanel[i].gameObject.SetActive(isPlayerValid);
            if (isPlayerValid) SetPlayerResults(i);
            var color = isPlayerValid ? Player.GetPlayerColor(GameManager.instance.gameResults[i].playerID) : Color.clear;
            statsPlayerText[i].color = color;
            statsGemsText[i].color = color;
            statsHitsGivenText[i].color = color;
            statsHitsTakenText[i].color = color;
            statsDeathsText[i].color = color;
        }
    }

    void SetPlayerResults(int i)
    {
        var results = GameManager.instance.gameResults[i];
        resultsText[i].text = "Player " + results.playerID;
        var color = Player.GetPlayerColor(results.playerID);
        resultsText[i].color = color;
        for (int j = 0; j < results.gemCount; j++)
        {
            var gem = Instantiate(gemImageTemplate);
            gem.SetActive(true);
            gem.transform.SetParent(resultsPanel[i], false);
        }
        statsPlayerText[i].text = "Player " + results.playerID;
        statsGemsText[i].text = results.gemsCollected.ToString();
        statsHitsGivenText[i].text = results.hitsLanded.ToString();
        statsHitsTakenText[i].text = results.hitsTaken.ToString();
        statsDeathsText[i].text = results.deaths.ToString();
    }

    public void LoadOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }
}
