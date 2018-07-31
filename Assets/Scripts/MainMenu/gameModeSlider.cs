using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameModeSlider : MonoBehaviour {
	public GameState gamestate;
	public GameObject text;
	public List<GameObject> players;

	// Use this for initialization
	void Start () {
		gamestate.resetGameMode ();
		players [gamestate.num_player_slider].SetActive (true);
		text.GetComponent<Text>().text = "How many players: 1?";
	}
	
	public void on_click(int i) {
		players [gamestate.num_player_slider].SetActive (false);

		gamestate.num_player_slider += i;
		if (gamestate.num_player_slider < 0)
			gamestate.num_player_slider = players.Count - 1;
		else if (gamestate.num_player_slider == players.Count)
			gamestate.num_player_slider = 0;

		gamestate.num_players = gamestate.num_player_slider + 1;
		players [gamestate.num_player_slider].SetActive (true);
		text.GetComponent<Text>().text = "How many players: " + gamestate.num_players + "?";
	}
}
