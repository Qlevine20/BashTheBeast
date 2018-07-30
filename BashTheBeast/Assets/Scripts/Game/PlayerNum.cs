using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNum : MonoBehaviour {


	public int playerID;
	public Sprite[] playerNums;
	void Start () {
		
	}
	void Update () {
		GetComponent<SpriteRenderer>().sprite = playerNums[playerID];
	}
}



