using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Camera : MonoBehaviour {

	GameInstance gameInstance;
	bool isControlled = false;

	Quaternion targetRotation;
	Quaternion originalRotation;
	float angleTolerance = 0.01f;
	bool isRotating = false;
	float rotationRate = 4f;

	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance> ();
		originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameInstance.isGameOver)
			return;
		if(Input.GetMouseButton(0) && isControlled){
			targetRotation = getRotation(Input.mousePosition);
			isRotating = true;
		}
		if (isRotating) {
			if (Mathf.Abs (transform.rotation.z) > targetRotation.z - angleTolerance
				&& Mathf.Abs (transform.rotation.z) < targetRotation.z + angleTolerance) {
				isRotating = false;
				transform.rotation = targetRotation;
			}
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationRate);
		}
	}

	public Quaternion getRotation(Vector2 lookAtTarget){
		var cameraPos = gameInstance.HackerCam.GetComponent<Camera> ().WorldToScreenPoint (transform.position);
		var ang = Mathf.Atan2 (lookAtTarget.y-cameraPos.y,lookAtTarget.x-cameraPos.x);
		ang = (180 / Mathf.PI) * ang;
		return Quaternion.Euler (0, 0, ang);
	}

	public void SetTargetRotation(Quaternion newRotation){
		targetRotation = newRotation;
		isRotating = true;
	}

	public void SetIsControlled(bool thisIsControlled){
		isControlled	= thisIsControlled;
		targetRotation	= originalRotation;
	}

	void OnMouseDown(){
		gameInstance.hacker.GetComponent<PlayerController_Hacker>().TakeControlOfObject(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			(gameInstance.HackerCam.GetComponent<Camera>()).cullingMask |= 1 << LayerMask.NameToLayer("Thief");
			gameInstance.ShowGameOver (false);
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
