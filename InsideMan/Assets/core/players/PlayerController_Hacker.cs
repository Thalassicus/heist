using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Hacker : MonoBehaviour {
	GameInstance gameInstance;
	GameObject controlledObject;



	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameInstance.isGameOver)
			return;
		
		if( Input.GetMouseButtonDown(1) ){
			ReleaseControlOfObject ();
		}
	}

	public void TakeControlOfObject(GameObject obj){
		ReleaseControlOfObject ();
		controlledObject = obj;
		controlledObject.GetComponent<NPC_Camera> ().SetIsControlled (true);
	}

	public void ReleaseControlOfObject(){
		if(controlledObject != null){
			controlledObject.GetComponent<NPC_Camera> ().SetIsControlled (false);
			controlledObject = null;
		}
	}
}
