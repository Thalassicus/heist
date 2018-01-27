using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Thief : MonoBehaviour {
	public float moveSpeed = 1f;
	public float sprintSpeed = 2f;
	public float carryMultiplier = 0.8f;
	bool isSprinting = false;
	float moveSpeedActual = 0f;
	float sprintSpeedActual = 0f;

	bool hasObjective = false;

	//Rigidbody2D rigidbody2D = new Rigidbody2D();

	void Start(){
		//rigidbody2D = ((Rigidbody2D)GetComponent(typeof(Rigidbody2D)));
		moveSpeedActual = moveSpeed/1000;
		sprintSpeedActual = sprintSpeed/1000;
	}

	void Update () {
		var moveVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		var speed = moveSpeedActual;
		moveVector.Normalize();
		isSprinting = Input.GetAxis ("Horizontal") != 0;
		if(isSprinting){
			speed = sprintSpeedActual;
		}
		if(hasObjective){
			speed = speed * carryMultiplier;
		}
		moveVector = moveVector * speed;
		transform.Translate (moveVector);
		//rigidbody2D.MovePosition(rigidbody2D.position + moveVector);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if( collider.tag == "Objective" ){
			hasObjective = true;
			collider.gameObject.transform.SetParent(transform);
			collider.gameObject.transform.localPosition = new Vector3 (0f, 0.1f, 0f);
			Debug.Log("Picked up the objective!");
		}
		if(collider.tag == "Exit" && hasObjective){
			// End Game
			Debug.Log("WOOOOOO!!!!");
		}
	}
}
