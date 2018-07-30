using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseCharacters : MonoBehaviour {

	public int choice;
	public bool isChoosen;
    

	void Start()
	{
		choice = 0;
		setActive (false);
		isChoosen = false;
	}

	public void onChoose(int num)
	{
		if (!isChoosen) {
			choice = num;
			isChoosen = true;
            PersistentInfo.instance.goToGame = true;
		}
	}

	public void reset(){
		choice = 0;
		isChoosen = false;
	}


	public void setActive(bool active)
	{
		this.gameObject.GetComponent<Image>().enabled = active;
		GameObject[] btns = GameObject.FindGameObjectsWithTag ("ChoosePanelBtn");
		foreach (GameObject g in btns)
		{
			g.GetComponent<Image> ().enabled = active;
			g.GetComponent<Button> ().enabled = active;

		}
		GameObject[] txts = GameObject.FindGameObjectsWithTag ("ChoosePanelBtnTxt");
		foreach (GameObject g in txts)
		{
			g.GetComponent<Text> ().enabled = active;
		}
	}
		

}
