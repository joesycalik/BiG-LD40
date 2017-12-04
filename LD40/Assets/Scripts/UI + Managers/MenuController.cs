using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject credits;

    public void onClick(int menuID)
    {
        switch (menuID)
        {
            case 0:
                howToPlay.SetActive(false);
                credits.SetActive(false);
                mainMenu.SetActive(true);
                break;

            case 1:
                mainMenu.SetActive(false);
                howToPlay.SetActive(true);
                break;

            case 2:
                mainMenu.SetActive(false);
                credits.SetActive(true);
                break;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
