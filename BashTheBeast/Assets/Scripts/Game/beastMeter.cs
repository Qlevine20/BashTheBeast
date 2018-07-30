using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beastMeter : MonoBehaviour {


	public int beastMeterID;
	public Sprite[] beastMeters;
	void Start () {

	}
	void Update () {
		GetComponent<SpriteRenderer>().sprite = beastMeters[beastMeterID];
	}
}



