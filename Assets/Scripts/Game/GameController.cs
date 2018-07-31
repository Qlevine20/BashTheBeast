using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {
    private SpriteRenderer sprite;
	public bool showPlayerMsg = true;   // show current player, current field, and beast size
	public string msg = "";

	[Range(1, 4)]
	public int playerCount;
	GUIStyle currentStyle = new GUIStyle();
	// the 4 Player Positions on the Fields
	public Vector3[] playerSlots;

	// Vector3 playerBaseRotation = new Vector3(0,180,0);

	public int currentPlayer;
	// int selectedPlayer = -1;  							// for the Player selection Screen if one can choose a Player to send back to start
	// int totalDicedNumber = 0; 							// the total number of all Dices
	// int dicesFinished = 0; 								// this have to match dice.Count before the Player can Move
	public Dice dice;

	public List<Player> player = new List<Player>();			// a List holding all the Players
	public List<Interior> interiors = new List<Interior>();  	// a List holding all Car Interiors where each interior corresponds to a player in the player list
	public List<PlayerNum> playernums = new List<PlayerNum>();
	public List<Field> field = new List<Field>();				// a List holding all Fields
	public List<Player> winner = new List<Player>();
	public List<beastMeter> beastsMeters = new List<beastMeter> ();  	     // a List of BeastMeters that associate with players' id.
	public List<beast> beasts = new List<beast>();
	// a list of Vector2 ==> Vertor2(start,finish) of each track
	public Vector2[] tracks;

	public bool stopWhenFirstPlayerHasFinished = false;	// should the Game stop when the first Player reaches the finish?
	// public bool diceAgainWhenDicedSix = true;			// can the player dice again when he diced a six?

	public bool waitForYourTurn = false;						// dont allow input until its your turn
	public bool isGamerMoving = false;							// dont allow input as long as a player is moving
	public bool isSpawningPlayers = false;						// dont allow input as long as the players are spawning
	public bool isGameOver = false;							// is the Game Over yet?
	public bool inAction = false;
    public Text gameOverNumber;
    public Text gameOverPlayer;
    public Text gameOverBeastSize;
	public Text backForwardText;
	public int firstUpdate = 0;
    public float winnerTimer = 0;
    public int winnerWaitTime = 10;
	public GameObject[] Meters;
	public GameState gamestate;
	public GameObject gameOverPanel;
	public GameObject backForwardPanel;
	public GameObject questionPanel;
	public GameObject questionCard;
    public GameObject multiQuestionPanel;
  


	// Use this for initialization
	void Start () {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
		playerSlots = new Vector3[]
		{
			new Vector3(0.00f, 0.03f), // upper left
			new Vector3(0.1f, 0.06f), // upper right
			new Vector3(0.2f, 0.09f), // lower left
			new Vector3(0.3f, 0.12f)  // lower right
		};
        //gamestate = GameObject.FindGameObjectWithTag ("GameState").GetComponent<GameState> ();
        if (PersistentInfo.instance.remakeScene)
        {
            PersistentInfo pinf = PersistentInfo.instance;
            gamestate = pinf.gstate;
            currentPlayer = pinf.currentPlayer;
            playerCount = pinf.NumPlayers;
            StartCoroutine(reset(true));
        }

        else
        {
            PersistentInfo.instance.gstate = gamestate;
            playerCount = gamestate.player_cars.Count;
            PersistentInfo.instance.NumPlayers = playerCount;
            currentPlayer = UnityEngine.Random.Range(0, playerCount);
            PersistentInfo.instance.currentPlayer = currentPlayer;
            StartCoroutine(reset(false));
        }


	}

	void OnGUI(){
		currentStyle.fontSize = 22;
		GUI.backgroundColor = Color.white;
		GUI.contentColor = Color.black;
		if (showPlayerMsg) {
			//currentStyle.alignment=TextAnchor.MiddleCenter;
			GUI.Box (new Rect (10, 10, 200, 80), msg,currentStyle);
			//Debug.Log ("Label showed");
		}
	}

	// helper functions

	// reset a game
	IEnumerator reset(bool reMake)
	{

		waitForYourTurn = true;

		// search all field Objects and sort by id
		field.Clear();

		dice = GameObject.FindGameObjectWithTag ("Dice").GetComponent<Dice> ();
		yield return null;

		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Field"))
		{
			field.Add (g.GetComponent<Field>());

			yield return null;
		}
		field.Sort((a, b) => a.ID.CompareTo(b.ID));

		GameObject.FindGameObjectWithTag ("Interior").GetComponent<Interior> ().interiorID = gamestate.player_cars[currentPlayer];
		GameObject.FindGameObjectWithTag ("PlayerNum").GetComponent<PlayerNum> ().playerID = currentPlayer;
        Meters = GameObject.FindGameObjectsWithTag ("BeastMeter");
		foreach (GameObject beastMeter in Meters)
		{
			beastMeter.GetComponent<beastMeter> ().beastMeterID = currentPlayer;
		}
		GameObject.FindGameObjectWithTag ("beast").GetComponent<beast> ().beastID = 0;

        if (reMake)
        {
            SpawnPlayer(true);
            isGamerMoving = true;
            player[PersistentInfo.instance.currentPlayer].Move(PersistentInfo.instance.MoveForward);
            
        }
        else
        {
            SpawnPlayer(false);
        }

        if (!reMake)
        {
            isGamerMoving = false;
        }


		while(isSpawningPlayers)
			yield return null;

		yield return new WaitForSeconds(1f);
		waitForYourTurn = false;

		yield return 0;
	}

	// Remove old Players and Start Coroutine 'spawnPlayer'
	// normally called at the beginning of the Game or when resetting the current Scene
	void SpawnPlayer(bool reMake)
	{
        Debug.Log("spawn player: ");
		player.Clear();
        if (!reMake)
        {
            while (GameObject.FindGameObjectWithTag("Player") != null)
            {
                DestroyImmediate(GameObject.FindGameObjectWithTag("Player"));
            }
        }


        if (reMake)
        {
            for (int i = 0; i < playerCount; i++)
            {
                spawnSinglePlayer(PersistentInfo.instance.pinfos[i]);
            }
        }
        else
        {
            StartCoroutine(spawnPlayer());
        }

	}

	


    public void spawnSinglePlayer(PersistentInfo.Pinfo playerInfo)
    {
        Debug.Log("Spawn single player: ");
        isSpawningPlayers = true;
        GameObject gamer = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        sprite = gamer.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = -1;
        gamer.name = "Player_" + (playerInfo.playerNum + 1); // Gameobjects name doesn't really matter
        gamer.tag = "Player";

        gamer.GetComponent<Player>().ID = playerInfo.playerID;
        gamer.GetComponent<Player>().Name = playerInfo.playerName;
        gamer.GetComponent<Player>().HasFinished = playerInfo.hasFinished;
        Debug.Log(gamer.name + ": " + playerInfo.fieldID);
        gamer.GetComponent<Player>().CurrentFieldID = playerInfo.fieldID;
        gamer.GetComponent<Player>().beast = playerInfo.beast;
        Player p = gamer.GetComponent<Player>();
        p.playerNum = playerInfo.playerNum + 1;
        p.track = playerInfo.track;
        p.gc = this;
        player.Add(gamer.GetComponent<Player>());

        gamer.transform.position = field[(playerInfo.fieldID)].transform.position + playerSlots[playerInfo.playerID];
        isSpawningPlayers = false;
    }

    // Spawns the players and moves them to the Start field.
    IEnumerator spawnPlayer()
	{
        Debug.Log("SpawnPlayer Ienum");
		isSpawningPlayers = true;


		// spawn the players
		for (int i = 0; i < playerCount; ++i) {
			float t = 0f;

            // create a list of players in menu and don't destroy on load 
          
          
			GameObject gamer = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
            sprite = gamer.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = -1;
            gamer.name = "Player_" + (i + 1); // Gameobjects name doesn't really matter
			gamer.tag = "Player";

			gamer.GetComponent<Player> ().ID = gamestate.player_cars[i];
			gamer.GetComponent<Player> ().Name = "Player_"+(i+1);
			gamer.GetComponent<Player> ().HasFinished = false;
			gamer.GetComponent<Player> ().CurrentFieldID = 0;
			gamer.GetComponent<Player> ().beast = 0;

            PersistentInfo.Pinfo pinfo = new PersistentInfo.Pinfo();
            pinfo.playerID = gamestate.player_cars[i];
            pinfo.playerNum = i;
            pinfo.playerName = "Player_" + (i + 1);
            pinfo.hasFinished = false;
            pinfo.fieldID = 0;
            pinfo.beast = 0;

			Vector3 startPosition = gamer.transform.position;
			Vector3 endPosition = field [0].transform.position+ playerSlots[i];

			// lerp the player to the starting field
			while(t < 1f)
			{
				t += Time.deltaTime * 4f;

				gamer.transform.position = Vector3.Lerp(startPosition, endPosition, t);

				yield return null;
			}

            // add to players list
            pinfo.playerPos = gamer.transform.position;
            PersistentInfo.instance.pinfos.Add(pinfo.playerNum, pinfo);
            player.Add(gamer.GetComponent<Player>());

			yield return null;
		}

		isSpawningPlayers = false;

		//Destroy (gamestate);
		yield return 0;
	}


	
	// Update is called once per frame
	void Update () {
		if (isGameOver){
			Debug.Log ("GameOver");
            //int smallestBeast = 8;
            string winnerNumber = "";
            string winnerPlayer = "";
            string winnerBeastSize = "";
			if (firstUpdate == 0) {
				for (int i = 0; i < winner.Count; i++) {
                    winnerNumber += (i + 1).ToString() + "\n";
                    winnerPlayer += "Player " + (winner[i].playerNum).ToString() + "\n";
                    winnerBeastSize += winner[i].beast.ToString() + "\n";
				}
				gameOverPanel.SetActive (true);
                gameOverNumber.text = winnerNumber;
                gameOverPlayer.text = winnerPlayer;
                gameOverBeastSize.text = winnerBeastSize;
				firstUpdate++;
			}
            else
            {
                if(winnerTimer > winnerWaitTime)
                {
                    SceneManager.LoadScene(0);
                }
                winnerTimer += Time.deltaTime;
                return;
            }
		}	


		// dont allow any input as long as a player is moving
		if (isGamerMoving)
			return;

		// dont allow any input until its your turn
		if (waitForYourTurn)
			return;

		// don't allow anyone move when a player is in action (monster card)
		if (inAction) {
			StartCoroutine(performAction ());
			dice.isRolled = false;
			return;
		}

		msg = "Current Player: " + (currentPlayer+1).ToString() + "; Current Field: " + player [currentPlayer].CurrentFieldID + "; Beast size: " + player [currentPlayer].beast;
		GameObject.FindGameObjectWithTag ("Interior").GetComponent<Interior> ().interiorID = gamestate.player_cars[currentPlayer];
		GameObject.FindGameObjectWithTag ("PlayerNum").GetComponent<PlayerNum> ().playerID = currentPlayer;
		GameObject.FindGameObjectWithTag ("beast").GetComponent<beast> ().beastID = player [currentPlayer].beast;
		foreach (GameObject beastMeter in Meters)
		{
			beastMeter.GetComponent<beastMeter> ().beastMeterID = player[currentPlayer].beast;
		}
		if (player.Count > 0 && dice.isRolled){
			if (GameObject.FindGameObjectWithTag ("beastCard").GetComponent<cards> ().Status())
			{
				Debug.Log("Need to change the card");
				GameObject.FindGameObjectWithTag ("beastCard").GetComponent<cards> ().setActive (false);
			}
			StartCoroutine(playersTurn());
		}

	}

	IEnumerator performAction() {
		player [currentPlayer].Action ();
		yield return new WaitForSeconds(0.15f);
	}



	/// <summary>
	/// Players turn.
	/// First roll the dice, after that move the player to the target Field
	/// This Method is called e.g. after the current player pushes a Button or Presses a Key
	/// </summary>
	IEnumerator playersTurn()
	{
		waitForYourTurn = true;
        Dictionary<int, PersistentInfo.Pinfo> pdict = PersistentInfo.instance.pinfos;
        for(int i = 0; i < pdict.Count; i++)
        {
            Debug.Log("Dict Key " + i + ": " + pdict[i].playerNum);
        }
		// Move to the target field
		player[currentPlayer].Move(dice.diceNum);
		inAction = true;
		yield return new WaitForSeconds(0.15f);
	}




	public bool IsGameOver()
	{
		foreach(Player g in player)
		{
			if (!g.HasFinished)
				return false;
		}

		return true;
	}

	// Simply swaps to the next Player
	public void NextPlayer()
	{
		if (isGameOver) 
			return;
		
		currentPlayer++;
		if(currentPlayer >= playerCount)
		{
			currentPlayer = 0;
		}
        PersistentInfo.instance.currentPlayer = currentPlayer;
		CheckNextPlayerHasFinished();

	}

	public void CheckNextPlayerHasFinished()
	{
		while(player[currentPlayer].HasFinished)
		{
			currentPlayer++;
			if (currentPlayer >= player.Count)
				currentPlayer = 0;
		}

		if(currentPlayer >= playerCount)
		{
			currentPlayer = 0;
		}
        PersistentInfo.instance.currentPlayer = currentPlayer;
	}

	public void resetPlayer()
	{
		dice.isRolled = false;
		inAction = false;
        NextPlayer();

    }
}
