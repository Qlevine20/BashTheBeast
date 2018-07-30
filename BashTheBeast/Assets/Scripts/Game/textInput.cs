using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class textInput : MonoBehaviour
{
	InputField input;
	InputField.SubmitEvent se;
	public Text output;
	public int ranInt;
	public string wtext;
	void Start()
	{
		Debug.Log("Start input ");
		input = gameObject.GetComponent<InputField>();
		se = new InputField.SubmitEvent();
		se.AddListener(SubmitInput);
		input.onEndEdit = se;
	}
	private void SubmitInput(string arg0)
	{
		output.text = arg0;//newText;
		input.text = "";
		input.ActivateInputField();
		Debug.Log(arg0);
		wtext = arg0;
		submit ();
	}

	public void submit()
	{
		// supposedly send sth to where ?
		Debug.Log("Submit");
		StreamWriter writer = new StreamWriter("test.txt", true);
		writer.WriteLine("CardID: "+ranInt+"\t"+wtext);
		writer.Close();
		Debug.Log("writer close");
		/*StreamWriter writer = new StreamWriter("test.txt", true);
		writer.WriteLine(output.text);
		writer.Close();*/


		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().resetPlayer();
		GameObject.FindGameObjectWithTag("QuestionCardPanel").SetActive(false);
		GameObject.FindGameObjectWithTag("questionCard").GetComponent<questioncard>().setActive(false);
	}

}