using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public GameObject pauseButton, pausePanel;
	// Use this for initialization
	public void onPause(){
        if (UIController.instance.isPaused == false)
        {
            UIController.instance.isPaused = true;
            pausePanel.SetActive(true);
        }

	}
	public void onUnPause(){
        UIController.instance.isPaused = false;
        pausePanel.SetActive(false);

	}
}
