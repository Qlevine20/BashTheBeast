using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getNumPlayer : MonoBehaviour {
	public GameState gamestate;

	public void Update() {
		gamestate.num_players = GetComponent<Dropdown> ().value + 1;
	}
}
