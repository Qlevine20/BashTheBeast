using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class questioncard : MonoBehaviour {

	// Use this for initialization
	public bool isChoosen;
	public Sprite[] qcards;
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


		GameObject qcard = GameObject.FindGameObjectWithTag("questionCard");

		qcard.GetComponent<Image>().sprite = qcards[randomIndex];
		qcard.GetComponent<Image>().enabled = active;

		if (active == true)
		{
			isChoosen = true;
		}
		if (active == false)
		{
			isChoosen = false;
		}
	}

	// Update is called once per frame
	public bool Status()
	{
		return isChoosen;
	}
}