using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MultiQuesP : MonoBehaviour {
    public GameController gc;
	public bool isChoosen;
	public int choice;
    public int cardNum;
    public string rightAnswer;
    public int ButtonNum;
    public string question_text;
    public int beastSize;
    public GameObject card;
    public GameObject Awin;
    public GameObject Bwin;
    public GameObject Cwin;
    public GameObject Dwin;
    public Button A;
    public Button B;
    public Button C;
    public Button D;
    public Text text;
    public int newBeast;




	// Use this for initialization
	void Start () {
        choice = 0;
		isChoosen = false;
	}
	public void onChoose(int num)
	{
		if (!isChoosen)
		{
			choice = num;
			isChoosen = true;
		}
	}

	public void reset()
	{
		choice = 0;
		isChoosen = false;
	}

    public string[] readFile(){
        //In quetsions.text the format of the text should be: CardNum/HowManyButtons/QuestionText/CorrectAnswer/BeastSize(if right, beast becomes smaller how many beastsize)
       

        TextAsset file = (TextAsset)Resources.Load("Multiple Choice UI/Questions");
        string txt = file.text;
        string[] diff_parts = txt.Split(new string[] {Environment.NewLine},StringSplitOptions.RemoveEmptyEntries); // return a list which each element represents a line in questiosns.txt
        return diff_parts;
    }

    public void processText(string[] diff_parts,int randomNum){
        string question = diff_parts[randomNum];
        string[] question_parts = question.Split('/');
        Debug.Log("Card num"+question_parts[0]);

        cardNum = Int32.Parse(question_parts[0].Trim());
        ButtonNum = Int32.Parse(question_parts[1]);
        question_text = question_parts[2];
        rightAnswer = question_parts[3];
        string newString = question_parts[4].Replace("\r", "").Replace("\n", "");
        Debug.Log("Beast Size: " + newString);
        beastSize = Int32.Parse(newString);
    }

    public void showPanel()
    {
        showText();
        showButton();


    }
    public void showButton(){
        if (ButtonNum>2){
            A.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/A");
            B.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/B");
            C.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/C");
            D.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/D");
        }
        else{
            A.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/A");
            B.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/B");

        }
    }
    public void showText()
    {
        text.text = question_text;
    }


    public void resetImage()
    {
        Awin.GetComponent<Image>().sprite = null;
        Bwin.GetComponent<Image>().sprite = null;
        Cwin.GetComponent<Image>().sprite = null;
        Dwin.GetComponent<Image>().sprite = null;
        card.GetComponent<Image>().sprite = Resources.Load("5 type Beasts Cards/Beast Cards/Beast-Card_Back", typeof(Sprite)) as Sprite;

    }
    

    public void AOnClick()
    {
        //A.onClick.AddListener(() => { CheckAnswer(A); });

        StartCoroutine(CheckAnswer(A));
    }
    public void BOnClick()
    {
        //B.onClick.AddListener(()=>{CheckAnswer(B);});

        StartCoroutine(CheckAnswer(B));
    }
    public void COnClick()
    {
        //C.onClick.AddListener(() => { CheckAnswer(C); });
      
        StartCoroutine(CheckAnswer(C));
    }
    public void DOnClick()
    {
        //D.onClick.AddListener(() => { CheckAnswer(D); });

        StartCoroutine(CheckAnswer(D));
    }
    // two bugs. 1. after first click, only the second click will appear right/wrong sign. 
    //           2. how to deactive the panel after show the answer.
    public IEnumerator CheckAnswer(Button b)
    {
        Debug.Log(b.name);
        bool right = false;
        if (ButtonNum < 3)
        {
            D.enabled = false;
            C.enabled = false;
            if (b.name == "ButtonA" && "A" == rightAnswer)
            {
                Awin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/right", typeof(Sprite)) as Sprite;
                Debug.Log("AButtonclickright");
                right = true;

            }
            else if (b.name == "ButtonA" && "A" != rightAnswer)
            {
                Awin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/wrong", typeof(Sprite)) as Sprite;
                Debug.Log("AButtonClickwrong");
               
            }
            else if (b.name == "ButtonB" && "B" == rightAnswer)
            {
                Bwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/right", typeof(Sprite)) as Sprite;
                Debug.Log("BButtonclickright");
                right = true;
             
            }
            else if (b.name == "ButtonB" && "B" != rightAnswer)
            {
                Bwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/wrong", typeof(Sprite)) as Sprite;
                Debug.Log("AButtonClickwrong");
           
            }

        }
        else{
            C.enabled = true;
            D.enabled = true;
            if (b.name == "ButtonA" && "A" == rightAnswer)
            {
                Awin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/right", typeof(Sprite)) as Sprite;
                Debug.Log("AButtonclickright");
                right = true;

            }
            else if (b.name == "ButtonA" && "A" != rightAnswer)
            {
                Awin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/wrong", typeof(Sprite)) as Sprite;
                Debug.Log("AButtonClickwrong");
               
            }
            else if (b.name == "ButtonB" && "B" == rightAnswer)
            {
                Bwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/right", typeof(Sprite)) as Sprite;
                Debug.Log("BButtonclickright");
                right = true;
            }
            else if (b.name == "ButtonB" && "B" != rightAnswer)
            {
                Bwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/wrong", typeof(Sprite)) as Sprite;
                Debug.Log("BButtonClickwrong");
         
            }
            else if (b.name == "ButtonC" && "C" == rightAnswer)
            {
                Cwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/right", typeof(Sprite)) as Sprite;
                Debug.Log("CButtonclickright");
                right = true;
            }
            else if (b.name == "ButtonC" && "C" != rightAnswer)
            {
                Cwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/wrong", typeof(Sprite)) as Sprite;
                Debug.Log("CButtonClickwrong");
         
            }
            else if (b.name == "ButtonD" && "D" == rightAnswer)
            {
                Dwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/right", typeof(Sprite)) as Sprite;
                Debug.Log("DButtonclickright");
                right = true;
            }
            else if (b.name == "ButtonD" && "D" != rightAnswer)
            {
                Dwin.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/wrong", typeof(Sprite)) as Sprite;
                Debug.Log("DButtonClickwrong");
             
            }
        }
        if (right)
        {
            card.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/card" + cardNum.ToString() + "/right", typeof(Sprite)) as Sprite;
            //    A.image.overrideSprite = Resources.Load<Sprite>("Multiple Choice UI/card" + cardNum.ToString() + "/A");
            newBeast = gc.player[gc.currentPlayer].beast -beastSize;

        }
        else
        {
            card.GetComponent<Image>().sprite = Resources.Load("Multiple Choice UI/card" + cardNum.ToString() + "/wrong", typeof(Sprite)) as Sprite;
            newBeast = gc.player[gc.currentPlayer].beast+beastSize;
        }

        if(0 <= newBeast && newBeast < 7)
        {
            PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[gc.currentPlayer];
            pinfo.beast = newBeast;
            PersistentInfo.instance.pinfos[gc.currentPlayer] = pinfo;

            gc.player[gc.currentPlayer].beast = newBeast;
        }
        else if (newBeast < 0)
        {
            gc.player[gc.currentPlayer].beast = 0;
            PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[gc.currentPlayer];
            pinfo.beast = 0;

            PersistentInfo.instance.pinfos[gc.currentPlayer] = pinfo;
        }

        else if (newBeast > 6)
        {
            gc.player[gc.currentPlayer].beast = 6;
            PersistentInfo.Pinfo pinfo = PersistentInfo.instance.pinfos[gc.currentPlayer];
            pinfo.beast = 6;
            PersistentInfo.instance.pinfos[gc.currentPlayer] = pinfo;
        }



        yield return new WaitForSeconds(2);

        gc.multiQuestionPanel.SetActive(false);
        resetImage();
        gc.resetPlayer();
       
    }

	
		

}
