using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState: ScriptableObject {

//	public int num_players = 0;
//	public int num_ai = 0;

	// for choosing how many players
	public int num_players = 1;
	public int num_player_slider = 0;

	// for choosing interior of car
	public int num_chosen = 0;
	public int current_slider = 0;

	public int tutorial_counter = 0;

	public List<int> available_cars = new List<int>() { 0, 1, 2, 3 };
	// 0 - Red; 1 - Blue; 2 - Green; 3 - Orange

	public List<int> player_cars = new List<int> ();
	// list index is player number -1
	// each index will be filled with car index number

	public void resetInterior() {
		current_slider = 0;
		num_chosen = 0;
		available_cars = new List<int>() { 0, 1, 2, 3 };
		player_cars = new List<int> ();
	}

	public void resetGameMode() {
		num_players = 1;
		num_player_slider = 0;
	}
}
