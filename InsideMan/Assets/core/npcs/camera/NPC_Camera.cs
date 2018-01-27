using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Camera : MonoBehaviour {

	GameInstance gameInstance;
	bool isControlled = false;

	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		Debug.Log ("Selected!");
		gameInstance.hacker.GetComponent<PlayerController_Hacker> ().TakeControlOfObject (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			(gameInstance.HackerCam.GetComponent<Camera>()).cullingMask |= 1 << LayerMask.NameToLayer("Thief");
		}
	}

	void OnTriggerStay2D(Collider2D collider){
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			(gameInstance.HackerCam.GetComponent<Camera>()).cullingMask &= ~(1 << LayerMask.NameToLayer("Thief"));
		}
	}
}
