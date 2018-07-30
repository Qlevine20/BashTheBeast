using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chooseInterior : MonoBehaviour {
	public GameState gamestate;
	public GameObject text;
	public List<GameObject> interior;

//	void Start() {
//		gamestate = GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ();
//	}

	public void on_click() {
        Debug.Log("test");
//		print ("Size of Available: "+gamestate.available_cars.Count);
//		print ("Current Slider: "+gamestate.current_slider);
//		print ("Choose: "+gamestate.available_cars[gamestate.current_slider]);
		gamestate.player_cars.Add (gamestate.available_cars[gamestate.current_slider]);
        Debug.Log("Added Car ID: " + gamestate.available_cars[gamestate.current_slider]);
		if (gamestate.player_cars.Count == 1)
			text.GetComponent<Text> ().text = "Player 2: Choose your view?";
		else if (gamestate.player_cars.Count == 2)
			text.GetComponent<Text> ().text = "Player 3: Choose your view?";
		else if(gamestate.player_cars.Count == 3)
			text.GetComponent<Text> ().text = "Player 4: Choose your view?";
        Debug.Log("Player: " + gamestate.player_cars.Count);
        Debug.Log("Total: "+ gamestate.num_players);
		if (gamestate.player_cars.Count == gamestate.num_players) {
			text.GetComponent<Text> ().text = "Loading Game";
			SceneManager.LoadScene (3);
			return;
		}
			
		interior [gamestate.available_cars[gamestate.current_slider]].SetActive (false);
		gamestate.available_cars.RemoveAt (gamestate.current_slider);
		get_next ();
		interior [gamestate.available_cars[gamestate.current_slider]].SetActive (true);
	}

	void get_next() {
		if (gamestate.current_slider < (gamestate.available_cars.Count - 1))
			gamestate.current_slider += 1;
		else
			gamestate.current_slider = 0;
	}

}
