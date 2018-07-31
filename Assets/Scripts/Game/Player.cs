using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

	public GameController gc;
	public int ID;
    public int playerNum;
	public string Name;
	public bool HasFinished;
	public int CurrentFieldID = 100;
	public Sprite[] cars;
	public Sprite[] angles;
	public Vector2 track = new Vector2(0, 25);
	public ChooseCharacters cc;
	public int beast; // each player need to have a beast
	public int delay = 5;
	public bool message = false;
	public int randomIndex;
	public Sprite[] qcards;
	public questioncard qc;
	public int random = 0;
	public int loopCount = 0;
    public int loop = 0;
    public MultiQuesP mp;

	// Use this for initialization
	void Start () {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
		GetComponent<SpriteRenderer>().sprite = cars[ID];
		GetComponent<SpriteRenderer>().sortingLayerName="fore";
        if(!PersistentInfo.instance || PersistentInfo.instance.remakeScene == false) { }
		    gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();


        if(track == Vector2.zero)
        {
            track = new Vector2(0, 25);
        }
        //track = new Vector2(0, 25);



    }

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator showMessage(string msg)
	{
		gc.backForwardPanel.SetActive (true);
		gc.backForwardText.text = msg;
		yield return new WaitForSeconds(1);
		gc.backForwardPanel.SetActive (false);
	}

public void Action(){

		if (gc.inAction) {
			if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field[CurrentFieldID].Action == ActionType.GoToField)
			{
				cc = GameObject.FindGameObjectWithTag("ChoosePanel").GetComponent<ChooseCharacters>();
				cc.setActive(true);
				if (cc.isChoosen && cc.choice > 0)
				{
					track = gc.tracks[cc.choice - 1];

					CurrentFieldID = (int)track.x;
                    Debug.Log("IN ACTION FIELD ID: " + CurrentFieldID);
                    PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer];
                    pinfo.fieldID = CurrentFieldID;
                    pinfo.track = track;
                    Debug.Log("Track: " + track);
                    PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer] = pinfo;
					transform.position = gc.field[CurrentFieldID].transform.position + gc.playerSlots[ID];
					cc.reset();


                    //gc.resetPlayer();
                    if (PersistentInfo.instance.goToGame)
                    {
                        PersistentInfo.instance.goToGame = false;
                        PersistentInfo.instance.remakeScene = true;
                        gc.inAction = false;
                        int randScene = UnityEngine.Random.Range(6, 8);
                        SceneManager.LoadScene(randScene);

                    }
                    cc.setActive(false);
				}

			}
			else if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field[CurrentFieldID].Action == ActionType.GoBack
			  && gc.field[CurrentFieldID].GoBackNumSteps != 0)
			{
				string msg = "Move back " + gc.field[CurrentFieldID].GoBackNumSteps + " cells";
				StartCoroutine(showMessage(msg));
				CurrentFieldID = (int)(CurrentFieldID - gc.field[CurrentFieldID].GoBackNumSteps);
                Debug.Log("IN ACTION FIELD ID2: " + CurrentFieldID);
                PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer];
                pinfo.fieldID = CurrentFieldID;
                PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer] = pinfo;
                transform.position = gc.field[CurrentFieldID].transform.position + gc.playerSlots[ID];
				gc.resetPlayer();
			}
			else if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field[CurrentFieldID].Action == ActionType.GoAhead
			  && gc.field[CurrentFieldID].GoAheadNumSteps != 0)
			{
				string msg = "Move ahead " + gc.field[CurrentFieldID].GoAheadNumSteps + " cells";
				StartCoroutine(showMessage(msg));
				CurrentFieldID = (int)(CurrentFieldID + gc.field[CurrentFieldID].GoAheadNumSteps);
                Debug.Log("IN ACTION FIELD ID3: " + CurrentFieldID);
                PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer];
                pinfo.fieldID = CurrentFieldID;
                PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer] = pinfo;
                transform.position = gc.field[CurrentFieldID].transform.position + gc.playerSlots[ID];
				gc.resetPlayer();
			}
			else if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field[CurrentFieldID].Action == ActionType.Action)
			{
				// if the action cell is between home to hospital: appear purple card track:(0,25) ;beast card(0,13)
				if (0 <= CurrentFieldID && CurrentFieldID <= 26)
				{
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().randomIndex = UnityEngine.Random.Range(0, 13);
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().setActive(true);
				}
				// if the action cell is in the first track. This is Stan's track:(26,52); beast card(44,58)
				else if (26 <= CurrentFieldID && CurrentFieldID <= 51)
				{
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().randomIndex = UnityEngine.Random.Range(44, 58);
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().setActive(true);

				}
				// if the action cell is in the second track. This is Sugar Ray's track(53,78); beast card(59,73)
				else if (53 <= CurrentFieldID && CurrentFieldID <= 77)
				{
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().randomIndex = UnityEngine.Random.Range(59, 73);
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().setActive(true);

				}
				// if the action cell is in the third track. This is Dora's track(79,104); beast card(14,28)
				else if (79 <= CurrentFieldID && CurrentFieldID <= 103)
				{
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().randomIndex = UnityEngine.Random.Range(14, 28);
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().setActive(true);

				}
				// if the action cell is in the fourth track. This is Sid's track(105,130); beast card(29,43)
				else if (105 <= CurrentFieldID && CurrentFieldID <= 129)
				{
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().randomIndex = UnityEngine.Random.Range(29, 43);
					GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().setActive(true);

				}
				int new_beast = beast;
				new_beast += GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().points[GameObject.FindGameObjectWithTag("beastCard").GetComponent<cards>().randomIndex];

				if (0 <= new_beast && new_beast < 7)
				{
                    Debug.Log("IN ACTION FIELD ID4: " + CurrentFieldID);
                    PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[gc.currentPlayer];
                    pinfo.beast = new_beast;
                    PersistentInfo.instance.pinfos[gc.currentPlayer] = pinfo;
                    beast = new_beast;
				}
				else if (new_beast < 0)
                {
                    Debug.Log("IN ACTION FIELD ID5: " + CurrentFieldID);
                    PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[gc.currentPlayer];
                    pinfo.beast = 0;
                    PersistentInfo.instance.pinfos[gc.currentPlayer] = pinfo;
                    beast = 0;
                }

				else if (new_beast > 6)
                {
                    Debug.Log("IN ACTION FIELD ID6: " + CurrentFieldID);
                    PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[gc.currentPlayer];
                    pinfo.beast = 6;
                    PersistentInfo.instance.pinfos[gc.currentPlayer] = pinfo;
                    beast = 6;
                }


				gc.resetPlayer();
			}

			else if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field[CurrentFieldID].Action == ActionType.Question)
			{
				// move to the question cell and need the quesiton panel set active;
				if (!gc.questionPanel.activeSelf)
				{

					qc = GameObject.FindGameObjectWithTag("questionCard").GetComponent<questioncard>();
					if (random == 0)
					{
						qc.randomIndex = UnityEngine.Random.Range(0, 24);
						random = qc.randomIndex;
					}
					else
					{
						qc.randomIndex = random;
					}
					qc.setActive(true);

					gc.questionPanel.SetActive(true);

					// blocking other user from playing
				}
				if (loopCount == 0)
				{
					GameObject.FindGameObjectWithTag("Text").GetComponent<textInput>().ranInt = random + 1;
					loopCount += 1;

				}

				random = 0;
			}
			else if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field[CurrentFieldID].Action == ActionType.MultiQues)
			{
                if (!gc.multiQuestionPanel.activeSelf)
                {
                    gc.multiQuestionPanel.SetActive(true);
                    Debug.Log(gc.multiQuestionPanel.GetComponent<MultiQuesP>());
                    string[] all_questions = gc.multiQuestionPanel.GetComponent<MultiQuesP>().readFile();
                    int maxLength = all_questions.Length;
                    gc.multiQuestionPanel.GetComponent<MultiQuesP>().processText(all_questions, UnityEngine.Random.Range(0, maxLength));
                    gc.multiQuestionPanel.GetComponent<MultiQuesP>().showPanel();
                }
			}
			else
			{
       
				gc.resetPlayer();
			}
        
		}
      
	}



	public void Move(int dicedNumber){
		if (HasFinished)
			gc.NextPlayer();
		else
			StartCoroutine(moveForwards(dicedNumber));
	}

	// Move the current Player to the target Position field by field
	IEnumerator moveForwards(int dicedNumber)
	{
		gc.isGamerMoving = true;

		// get the field the player is currently on
		int currentField = CurrentFieldID;

		if (currentField + dicedNumber > track.y) {
			dicedNumber = (int)track.y - currentField;	
		}

		for (int i = 0; i < dicedNumber; i++)
		{
				float t = 0f;

				// increase the start- and endposition each iteration
				// startposition represents the field the player is on
				// endposition is always one field ahead of the players field
				// to make it look like the player moves one field at the time
				Vector3 startPosition = gc.field[(currentField + i)].transform.position+ gc.playerSlots[ID];
				Vector3 endPosition = gc.field[(currentField + i + 1)].transform.position+gc.playerSlots[ID];
				//Debug.Log("Player: "+ID+" FieldID: "+ CurrentFieldID+" StartPosition="+startPosition+" endPosition="+endPosition+" PlayerSlots: "+gc.playerSlots[ID].x+" "+gc.playerSlots[ID].y);
				float startZ = gc.field [(currentField + i)].transform.rotation.z;
				float endZ = gc.field [(currentField + i + 1)].transform.rotation.z;

				// lerp to the next field
				while(t < 1f)
				{
					t += Time.deltaTime * 4f;

					transform.position = Vector3.Lerp (startPosition, endPosition, t);
					//Debug.Log ("Rotate angle z="+(endZ-startZ));
					transform.Rotate (0,0,(endZ-startZ));
					yield return null;
				}
				yield return null;
			}
			// update the players current field
			CurrentFieldID = currentField + dicedNumber;
            Debug.Log("IN ACTION FIELD ID7: " + CurrentFieldID);
            PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer];
            pinfo.fieldID = CurrentFieldID;
            PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer] = pinfo;
            Debug.Log(pinfo.playerName + " : " + pinfo.fieldID);

        //move to the hospital and need to pop up menu

        if (gc.field[CurrentFieldID].Type == FieldType.Finish)
			{
				HasFinished = true;
                Debug.Log("IN ACTION FIELD ID8: " + CurrentFieldID);
                PersistentInfo.Pinfo pin = PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer];
                pin.hasFinished = HasFinished;
                PersistentInfo.instance.pinfos[PersistentInfo.instance.currentPlayer] = pin;
            gc.winner.Add (this);

				if(gc.stopWhenFirstPlayerHasFinished)
				{
					gc.isGameOver = false;
				}
				else
				{
					gc.isGameOver = gc.IsGameOver();
				}
				yield return new WaitForSeconds(0.15f);
			}

			// this player has finished moving

		// wait a little
		yield return new WaitForSeconds(0.1f);
		gc.isGamerMoving = false;
		gc.waitForYourTurn = false;

		yield return 0;
	}
}
