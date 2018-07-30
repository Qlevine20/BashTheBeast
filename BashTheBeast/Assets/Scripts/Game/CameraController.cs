using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject gco;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		GameController gc = gco.GetComponent<GameController>();
		offset = transform.position - gc.player[gc.currentPlayer].transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		GameController gc = gco.GetComponent<GameController>();
		transform.position = gc.player[gc.currentPlayer].transform.position + offset;
	}
}
