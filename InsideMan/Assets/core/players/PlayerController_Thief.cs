using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController_Thief : MonoBehaviour {
	GameInstance gameInstance;

	public float moveSpeed = 1f;
	public float sprintSpeed = 2f;
	public float carryMultiplier = 0.8f;
	bool isSprinting = false;
	float moveSpeedActual = 0f;
	float sprintSpeedActual = 0f;

	bool hasObjective = false;

	Rigidbody2D rigidbody2D = new Rigidbody2D();

	//public Text debugText;

	void Start(){
		gameInstance = FindObjectOfType<GameInstance> ();
		rigidbody2D = ((Rigidbody2D)GetComponent(typeof(Rigidbody2D)));
		moveSpeedActual = moveSpeed;
		sprintSpeedActual = sprintSpeed;
	}

	void Update () {
		rigidbody2D.velocity = Vector2.zero;
		if (gameInstance.isGameOver)
			return;
		
		var speed = moveSpeedActual;
		isSprinting = Input.GetAxis ("Sprint") != 0;
		if(isSprinting){
			speed = sprintSpeedActual;
		}
		if(hasObjective){
			speed = speed * carryMultiplier;
		}
		var moveVector = (Vector2.up * Input.GetAxis("Vertical")) + (Vector2.right * Input.GetAxis("Horizontal"));
		moveVector.Normalize();
		moveVector.Scale(new Vector2(speed * Time.deltaTime, speed * Time.deltaTime));
		//debugText.text = "H: " + Input.GetAxis("Horizontal") + "\n" + "V: " + Input.GetAxis("Vertical") + "\nSpeed: "+speed+"\n" + "Vector: (" + moveVector.x + ", " + moveVector.y + ")";

		//var moveVector = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0.8f));
		//moveVector.Normalize();
		//moveVector = moveVector * speed;
		//transform.Translate (moveVector);
		rigidbody2D.velocity = moveVector;
		//rigidbody2D.MovePosition(rigidbody2D.position + moveVector);
	}

	public void LootIt(bool wasShared, int value)
	{
		if (true)
		{

		}
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
			gameInstance.ShowGameOver (true);
		}
	}
}
