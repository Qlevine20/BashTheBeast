using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialcount : MonoBehaviour {
	public GameState gamestate;

	public void on_click(int i) {
		gamestate.tutorial_counter += i;
	}
}
