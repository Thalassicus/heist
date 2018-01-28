using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Camera : MonoBehaviour {
	GameInstance gameInstance;
	public DetectionBehavior detectionBehavior;

	public bool isControlled = false;

	Quaternion targetRotation;
	Quaternion originalRotation;
	float angleTolerance = 0.1f;
	bool isRotating = false;
	float rotationRate = 0.75f;

	public float cameraSweepAngle = 45f;
	public float cameraSweepTime = 8f;
	float cameraCanSweepTime;
	bool isLeft;

	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance> ();
		originalRotation = transform.rotation;
		SetRotationFromSweep();
		isLeft = Random.Range(0f, 1f) < 0.5f;
		cameraCanSweepTime = Time.time + (cameraSweepTime * Random.Range(0f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		if (gameInstance.isGameOver)
			return;
		if (Input.GetMouseButton(0) && isControlled) {
			SetTargetRotation(getRotation(Input.mousePosition));
		}
		if (isRotating) {
			if (Mathf.Abs(transform.rotation.eulerAngles.z) > targetRotation.eulerAngles.z - angleTolerance
				&& Mathf.Abs(transform.rotation.eulerAngles.z) < targetRotation.eulerAngles.z + angleTolerance) {
				isRotating = false;
				transform.rotation = targetRotation;
			}
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationRate);
		}
		if (Time.time > cameraCanSweepTime){
			DoSweep();
		}
	}

	void DoSweep()
	{
		if (isControlled) return;

		isLeft = !isLeft;
		SetRotationFromSweep();
		cameraCanSweepTime = Time.time + cameraSweepTime;
	}

	void SetRotationFromSweep()
	{
		if (isLeft)
		{
			SetTargetRotation(Quaternion.Euler(0, 0, cameraSweepAngle) * originalRotation);
		}
		else
		{
			SetTargetRotation(Quaternion.Euler(0, 0, -cameraSweepAngle) * originalRotation);
		}
	}

	public Quaternion getRotation(Vector2 lookAtTarget)
	{
		var cameraPos = gameInstance.HackerCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
		var ang = Mathf.Atan2(lookAtTarget.y - cameraPos.y, lookAtTarget.x - cameraPos.x);
		ang = (180 / Mathf.PI) * ang;
		return Quaternion.Euler(0, 0, ang + 90);
	}

	public void SetTargetRotation(Quaternion newRotation)
	{
		targetRotation = newRotation;
		isRotating = true;
	}

	public void SetIsControlled(bool thisIsControlled)
	{
		isControlled = thisIsControlled;
		SetRotationFromSweep();
		if (isControlled)
		{
			GetComponentInChildren<TriggerZone>().gameObject.tag = "Camera_Controlled";
			detectionBehavior.rangeRenderer.startColor = Color.blue;
			detectionBehavior.rangeRenderer.endColor = Color.blue;
		}
		else
		{
			GetComponentInChildren<TriggerZone>().gameObject.tag = "Camera";
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
