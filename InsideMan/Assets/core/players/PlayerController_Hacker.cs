using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Hacker : MonoBehaviour {
	GameInstance gameInstance;
	GameObject controlledObject;
	Quaternion originalAngle = new Quaternion();

	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance> ();
	}
	
	// Update is called once per frame
	void Update () {
		if( controlledObject != null ){
			if (Input.GetMouseButton(0)) {
				var cameraPos = gameInstance.HackerCam.GetComponent<Camera> ().WorldToScreenPoint (controlledObject.transform.position);
				var ang = Mathf.Atan2 (Input.mousePosition.y-cameraPos.y,Input.mousePosition.x-cameraPos.x);
				ang = (180 / Mathf.PI) * ang;
				Debug.Log ("Moved!");
				controlledObject.transform.rotation = Quaternion.Slerp(controlledObject.transform.rotation, Quaternion.Euler(0,0,ang), Time.deltaTime * 2f);
			}
			if (Input.GetMouseButtonDown (1)) {
				transform.rotation = originalAngle;
				controlledObject = null;
			}
		}
	}

	public void TakeControlOfObject(GameObject obj){
		if(controlledObject != null){
			controlledObject.transform.rotation = originalAngle;
			controlledObject = null;
		}
		controlledObject = obj;
		originalAngle = controlledObject.transform.rotation;
	}
}
