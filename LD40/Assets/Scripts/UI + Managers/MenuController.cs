using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject credits;
    public GameObject controls;

    public void onClick(int menuID)
    {
        switch (menuID)
        {
            case 0:
                howToPlay.SetActive(false);
                controls.SetActive(false);
                credits.SetActive(false);
                mainMenu.SetActive(true);
                break;

            case 1:
                mainMenu.SetActive(false);
                controls.SetActive(false);
                credits.SetActive(false);
                howToPlay.SetActive(true);
                break;

            case 2:
                mainMenu.SetActive(false);
                howToPlay.SetActive(false);
                controls.SetActive(false);
                credits.SetActive(true);
                break;

            case 3:
                mainMenu.SetActive(false);
                howToPlay.SetActive(false);
                controls.SetActive(true);
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
