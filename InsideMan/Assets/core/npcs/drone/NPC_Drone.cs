using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Drone : MonoBehaviour{
	GameInstance gameInstance;
	public DetectionBehavior detectionBehavior;

	public bool isControlled = false;

	Vector3 targetPosition;
	bool isMoving = true;
	float moveRate = 5f;
	float positionTolerance = 0.1f;

	public Vector2 range = new Vector2(25f,30f);

	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance>();
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		if (gameInstance.isGameOver)
			return;
		if (Input.GetMouseButton(0) && isControlled){
			SetTargetPosition(getPosition(Input.mousePosition));
		}
		if (isMoving)
		{
			var targetMoveDistance = (targetPosition - transform.position).normalized * Time.deltaTime * moveRate;
			var remainingMoveDistance = (targetPosition - transform.position);
			if (remainingMoveDistance.magnitude - targetMoveDistance.magnitude < positionTolerance)
			{
				transform.Translate(remainingMoveDistance);
				isMoving = false;
				if (!isControlled)
				{
					pickNewPosition();
				}
			}
			transform.Translate(targetMoveDistance);
		}
	}

	public void pickNewPosition()
	{
		SetTargetPosition(new Vector3(Random.Range(-1f, 1f) * range.x, Random.Range(-1f, 1f) * range.y, 0));
	}

	public Vector3 getPosition(Vector2 lookAtTarget){
		var targetPos = gameInstance.HackerCam.GetComponent<Camera>().ScreenToWorldPoint(lookAtTarget);
		return targetPos;
	}

	public void SetTargetPosition(Vector3 newTargetPosition)
	{
		targetPosition = newTargetPosition;
		isMoving = true;
	}

	public void SetIsControlled(bool thisIsControlled){
		isControlled = thisIsControlled;
		SetTargetPosition(transform.position);
		if (isControlled)
		{
			detectionBehavior.rangeRenderer.startColor = Color.blue;
			detectionBehavior.rangeRenderer.endColor = Color.blue;
		}
		else
		{
			detectionBehavior.rangeRenderer.startColor = Color.red;
			detectionBehavior.rangeRenderer.endColor = Color.red;
		}
		//targetRotation	= originalRotation;
	}

	void OnMouseDown()
	{
		gameInstance.hacker.GetComponent<PlayerController_Hacker>().TakeControlOfObject(gameObject);
	}
}
