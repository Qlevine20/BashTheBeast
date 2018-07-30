using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interiorSlider : MonoBehaviour {
	public GameState gamestate;
	public List<GameObject> interior;

	// Use this for initialization
	void Start () {
		//gamestate = GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ();
		gamestate.resetInterior ();
		//print (gamestate.current_slider);
		if (!interior [gamestate.current_slider].activeSelf)
			interior [gamestate.current_slider].SetActive (true);

	}

	public void on_click(int i) {
		if (gamestate.current_slider > gamestate.available_cars.Count)
			return;
		//print ("available: "+gamestate.available_cars.Count);
		//print ("Slider: "+gamestate.current_slider);
		interior [gamestate.available_cars [gamestate.current_slider]].SetActive (false);
		//interior [gamestate.current_slider].SetActive (false);
		gamestate.current_slider += i;
		if (gamestate.current_slider < 0)			
			gamestate.current_slider = gamestate.available_cars.Count - 1;
		else if (gamestate.current_slider == gamestate.available_cars.Count)
			gamestate.current_slider = 0;
		interior [gamestate.available_cars [gamestate.current_slider]].SetActive (true);
	}
}
