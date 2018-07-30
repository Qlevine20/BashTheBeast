using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questionPanel: MonoBehaviour
{
	// Use this for initialization
	public bool isChoosen;
	private GameController gc;
	public Sprite[] qcards;  
	public int[] points;
	public int randomIndex;
	void Start()
	{
		isChoosen = false;
	}
	public void reset()
	{
		isChoosen = false;
	}
	public void setActive(bool active)
	{
		GameObject qcard = GameObject.FindGameObjectWithTag("questionCard");
		randomIndex = Random.Range(0, 24);
		qcard.GetComponent<Image>().sprite = qcards[randomIndex];
		qcard.GetComponent<Image>().enabled = active;

		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		if (active == true)
		{
			isChoosen = true;
		}
		if (active == false)
		{
			isChoosen = false;
		}

	}
	public bool Status()
	{
		return isChoosen;
	}
}

