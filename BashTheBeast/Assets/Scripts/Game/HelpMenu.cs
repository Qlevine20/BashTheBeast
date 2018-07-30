using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour {
	public GameObject helpButton, helpPanel1,helpPanel2;
	// Use this for initialization
	public void onHelp(){
        if (UIController.instance.isPaused == false)
        {
            UIController.instance.isPaused = true;
            helpPanel1.SetActive(true);
            helpPanel2.SetActive(true);
        }

	}
	public void onUnHelp(){
        UIController.instance.isPaused = false;
        helpPanel1.SetActive(false);
		helpPanel2.SetActive(false);
	}
}
