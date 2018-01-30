using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Hacker : MonoBehaviour {
	GameObject controlledObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.instance.isGameOver)
			return;
		
		if( Input.GetMouseButtonDown(1) ){
			//ReleaseControlOfObject ();
		}
	}

	public void TakeControlOfObject(GameObject obj){
		ReleaseControlOfObject ();
		controlledObject = obj;
		if (controlledObject.GetComponent<ControllableObject>())
		{
			controlledObject.GetComponent<ControllableObject>().SetIsControlled(true);
		}
	}

	public void ReleaseControlOfObject(){
		if(controlledObject != null)
		{
			if (controlledObject.GetComponent<ControllableObject>())
			{
				controlledObject.GetComponent<ControllableObject>().SetIsControlled(false);
			}
			controlledObject = null;
		}
	}
}
