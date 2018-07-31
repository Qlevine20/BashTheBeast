using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clinicCard : MonoBehaviour
{

	// Use this for initialization
	public bool isChoosen;
	private GameController gc;
	public Sprite[] ccards;  // 
	public int randomIndex;
	void Start()
	{
		setActive(false);
		isChoosen = false;
	}
	public void reset()
	{
		isChoosen = false;
	}
	public void setActive(bool active)
	{
		GameObject clinicC = GameObject.FindGameObjectWithTag("clinicCard");
		clinicC.GetComponent<Image>().sprite = ccards[randomIndex];
		clinicC.GetComponent<Image>().enabled = active;

		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		Debug.Log("CurrentPlayer: " + gc.currentPlayer);
		if (active == true)
		{
			isChoosen = true;
		}
		if (active == false)
		{
			//gc.player [gc.currentPlayer].beast = 1 + gc.player [gc.currentPlayer].beast;
			isChoosen = false;
		}
		//Debug.Log ("Beast Size: " + gc.player [gc.currentPlayer].beast);

	}
	public bool Status()
	{
		return isChoosen;
	}
}
