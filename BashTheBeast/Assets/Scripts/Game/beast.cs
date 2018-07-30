using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beast : MonoBehaviour {

	// Use this for initialization

	public int beastID;
	public Sprite[] beasts;
	void Start () {
	}
	void Update () {
		GetComponent<SpriteRenderer>().sprite = beasts[beastID];
	}
} //beast


