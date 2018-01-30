using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Drone : ControllableObject {
	public DetectionBehavior detectionBehavior;
	
	float moveRate = 2.5f;

	public Vector2 range = new Vector2(25f,30f);
	
	// Use this for initialization
	void Start () {
		SetObjectLayer(-2f);
		SetTargetPosition(true);
	}
	
	// Update is called once per frame
	void Update (){
		if (Game.instance.isGameOver)
			return;
		if (Input.GetMouseButton(1)){
			if (isControlled) {
				SetTargetPosition(Game.instance.hackerCam.GetComponent<Camera>(), Input.mousePosition);
			}
		}
		/*
		if( Input.GetMouseButtonDown(0)){
			var dronePosition = Game.instance.hackerCam.GetComponent<Camera> ().WorldToScreenPoint (transform.position);
			if((dronePosition - Input.mousePosition).magnitude < 2f){
				Game.instance.hacker.GetComponent<PlayerController_Hacker>().TakeControlOfObject(gameObject);
			}
		}
		//*/
		if (hasNewTargetPosition){
			if ( IsAtTargetPosition(Time.deltaTime * moveRate) ) {
				transform.Translate((targetPosition - transform.position));
				hasNewTargetPosition = false;
				if (!isControlled){
					pickNewPosition();
				}
			}
			transform.Translate(Utilities2D.GetDirection3D(transform.position, targetPosition) * Time.deltaTime * moveRate);
		}
	}

	public void pickNewPosition()
	{
		SetTargetPosition(new Vector3(Random.Range(-1f, 1f) * range.x, Random.Range(-1f, 1f) * range.y, 0));
	}
	
	public override void SetIsControlled(bool newIsControlled){
		base.SetIsControlled(newIsControlled);
		//SetTargetPosition(transform.position);
		hasNewTargetPosition = true;
		if (isControlled)
		{
			gameObject.tag = "Camera_Controlled";
			detectionBehavior.rangeRenderer.startColor = Color.blue;
			detectionBehavior.rangeRenderer.endColor = Color.blue;
		}
		else
		{
			gameObject.tag = "Camera";
			detectionBehavior.rangeRenderer.startColor = Color.red;
			detectionBehavior.rangeRenderer.endColor = Color.red;
		}
		//targetRotation	= originalRotation;
	}

	void OnMouseDown()
	{
		Game.instance.hacker.GetComponent<PlayerController_Hacker>().TakeControlOfObject(gameObject);
	}
}
