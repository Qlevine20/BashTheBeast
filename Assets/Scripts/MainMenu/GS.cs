using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS : MonoBehaviour {

	public int num_players = 0;
	public int num_ai = 0;

	// for choosing interior of car
	public int num_chosen = 0;
	public int current_slider = 0;

	public List<int> available_cars = new List<int>() { 0, 1, 2, 3 };
	// 0 - Red; 1 - Blue; 2 - Green; 3 - Orange

	public List<int> player_cars = new List<int> ();
	// list index is player number -1
	// each index will be filled with car index number

	public void reset() {
		current_slider = 0;
		available_cars = new List<int>() { 0, 1, 2, 3 };
		player_cars = new List<int> ();
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
