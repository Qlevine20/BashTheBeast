using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cards : MonoBehaviour {

	// Use this for initialization
	public bool isChoosen;
	private GameController gc;
	public Sprite[] bcards;  // [0:13] home->hospital, [14:28] Dora, [29:43] Sid, [44,58]Stan, [59:73] Ray 
	public int[] points;
	public int randomIndex;
	void Start()
	{
		setActive (false);
		isChoosen = false;
	}
	public void reset(){
		isChoosen = false; 
	}
	public void setActive(bool active){
		GameObject beastcard = GameObject.FindGameObjectWithTag ("beastCard");
		beastcard.GetComponent<Image> ().sprite = bcards [randomIndex];
		beastcard.GetComponent<Image>().enabled = active;

		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		if (active == true) {
			isChoosen = true;
		}
		if (active == false) 
		{
			//gc.player [gc.currentPlayer].beast = 1 + gc.player [gc.currentPlayer].beast;
			isChoosen = false;
		}
		//Debug.Log ("Beast Size: " + gc.player [gc.currentPlayer].beast);

	}
	public bool Status(){
		return isChoosen;
	}
}
