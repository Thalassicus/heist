using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Thief : MonoBehaviour {
	float moveSpeed = 0.1f;
	Rigidbody2D rigidbody2D = new Rigidbody2D();

	void Start(){
		rigidbody2D = ((Rigidbody2D)GetComponent(typeof(Rigidbody2D)));
	}

	void Update () { 
		var moveVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		moveVector.Normalize();
		moveVector = moveVector * moveSpeed;
		transform.Translate (moveVector);
		//rigidbody2D.MovePosition(rigidbody2D.position + moveVector);
	}
}
