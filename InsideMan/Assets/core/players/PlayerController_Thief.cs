using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Thief : MonoBehaviour {
	float moveSpeed = 0.1f;

	void Update () { 
		var moveVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		moveVector.Normalize();
		moveVector = moveVector * moveSpeed;
		transform.
		transform.Translate(moveVector.x, moveVector.y, 0);
	}
}
