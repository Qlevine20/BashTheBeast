using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersistentInfo : MonoBehaviour {

    public static PersistentInfo instance;
    public bool remakeScene = false;
    public GameState gstate;
    public int MoveForward;
    public bool goToGame = false;
    // Use this for initialization

    [System.Serializable]
    public struct Pinfo
    {
        public Vector2 track;
        public int playerID;
        public int playerNum;
        public string playerName;
        public bool hasFinished;
        public Vector2 playerPos;
        public int fieldID;
        public int beast;
    }

    public int NumPlayers;
    public int currentPlayer;
    public Dictionary<int,Pinfo> pinfos = new Dictionary<int, Pinfo>();

	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
