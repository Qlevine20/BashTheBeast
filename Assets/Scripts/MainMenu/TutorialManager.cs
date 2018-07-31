using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
	public GameState gamestate;
	public GameObject text;

	public GameObject mainmenu;
	public GameObject mainmenuR;
	public GameObject back;
	public GameObject next;
	public GameObject carslider;

	public GameObject background;
	public GameObject board;

	public List<GameObject> interior;
	public List<GameObject> beetle;

	public GameObject startArrow;
	public GameObject hospitalArrow;
	public GameObject engineArrow;
	public GameObject menuHelpArrow;

	// Use this for initialization
	void Start () {
		gamestate.tutorial_counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestate.tutorial_counter == 0) {
			mainmenu.SetActive (true);
			back.SetActive (false);
			text.GetComponent<Text> ().text = "Welcome to the tutorial!";
		} else if (gamestate.tutorial_counter == 1) {
			mainmenu.SetActive (false);
			back.SetActive (true);
			text.GetComponent<Text> ().text = "My name is ____! Let's get started!";
			background.SetActive (true);
			board.SetActive (false);
		} else if (gamestate.tutorial_counter == 2) {
			text.GetComponent<Text> ().text = "This is the game board";
			background.SetActive (false);
			board.SetActive (true);
		} else if (gamestate.tutorial_counter == 3) {
			text.GetComponent<Text> ().text = "Wait! We forgot to choose a car!";

			board.SetActive (true);
			carslider.SetActive (false);
			foreach (GameObject car in GameObject.FindGameObjectsWithTag("Interior"))
				car.SetActive (false);
			//red.SetActive (false);
		} else if (gamestate.tutorial_counter == 4) {
			text.GetComponent<Text> ().text = "Before starting the game, you get to choose between 4 different cars!";
			board.SetActive (false);
			carslider.SetActive (true);
			interior [gamestate.current_slider].SetActive (true);
			//red.SetActive (true);
		} else if (gamestate.tutorial_counter == 5) {
			var car_color = "red";
			if (gamestate.current_slider == 0)
				car_color = "red";
			else if (gamestate.current_slider == 1)
				car_color = "blue";
			else if (gamestate.current_slider == 2)
				car_color = "green";
			else if (gamestate.current_slider == 3)
				car_color = "orange";
			text.GetComponent<Text> ().text = "Nice, " + car_color + " is my favorite too!";

			carslider.SetActive (false);
			board.SetActive (false);
			beetle [gamestate.current_slider].SetActive (false);
		} else if (gamestate.tutorial_counter == 6) {
			text.GetComponent<Text> ().text = "That's better :)";
			board.SetActive (true);
			beetle [gamestate.current_slider].SetActive (true);

			startArrow.SetActive (false);
		} else if (gamestate.tutorial_counter == 7) {
			text.GetComponent<Text> ().text = "This is where you start";
			startArrow.SetActive (true);
		} else if (gamestate.tutorial_counter == 8) {
			text.GetComponent<Text> ().text = "Hey, that's you!";

			hospitalArrow.SetActive (false);
		} else if (gamestate.tutorial_counter == 9) {
			text.GetComponent<Text> ().text = "Your goal is to get to the hospital over here and then back to the start through any of the 4 paths back!";
			startArrow.SetActive (false);

			hospitalArrow.SetActive (true);
		} else if (gamestate.tutorial_counter == 10) {
			text.GetComponent<Text> ().text = "The car color and player number on the wheel shows whose turn it is";
			hospitalArrow.SetActive (false);


		} else if (gamestate.tutorial_counter == 11) {
			text.GetComponent<Text> ().text = "When it is your turn, you get to start your car engine!";
			engineArrow.SetActive (false);

		} else if (gamestate.tutorial_counter == 12) {
			text.GetComponent<Text> ().text = "Click here to start your car!\nAfter clicking, the spedometer shows how many spaces you advance";
			engineArrow.SetActive (true);

		} else if (gamestate.tutorial_counter == 13) {
			text.GetComponent<Text> ().text = "Nice, you got a 6!";
			engineArrow.SetActive (false);

			menuHelpArrow.SetActive (false);
		} else if (gamestate.tutorial_counter == 14) {
			text.GetComponent<Text> ().text = "Click here for the main menu and help";
			menuHelpArrow.SetActive (true);
			next.SetActive (true);
		} else if (gamestate.tutorial_counter == 15) {
			text.GetComponent<Text> ().text = "That's it for the tutorial!\nYou're ready to play the game now!\nGood Luck and Have Fun! :)";
			menuHelpArrow.SetActive (false);

			next.SetActive (false);
			mainmenuR.SetActive (true);
		}
	}
}
