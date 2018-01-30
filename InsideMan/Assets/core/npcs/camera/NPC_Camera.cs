using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Camera : ControllableObject {
	public DetectionBehavior detectionBehavior;
	
	float angleTolerance = 0.1f;
	float rotationRate = 0.75f;

	public float cameraSweepAngle = 45f;
	public float cameraSweepTime = 8f;
	float cameraCanSweepTime;
	bool isLeft;

	// Use this for initialization
	void Start () {
		SetObjectLayer(-1f);
		startRotation = transform.rotation;
		SetRotationFromSweep();
		isLeft = Random.Range(0f, 1f) < 0.5f;
		SetCameraSweepTime(cameraSweepTime * Random.Range(0f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.instance.isGameOver)
			return;
		if (isControlled) {
			if ( Input.GetMouseButton(1) ) {
				SetTargetRotation(Game.instance.hackerCam.GetComponent<Camera>(), Input.mousePosition);
				SetCameraSweepTime();
			}
		}
		if (hasNewTargetRotation) {
			if (IsAtTargetRotation(angleTolerance)) {
				//Mathf.Abs(transform.rotation.eulerAngles.z) > targetRotation.eulerAngles.z - angleTolerance
				//&& Mathf.Abs(transform.rotation.eulerAngles.z) < targetRotation.eulerAngles.z + angleTolerance
				hasNewTargetRotation = false;
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
		SetCameraSweepTime();
	}

	void SetCameraSweepTime(float modifiedTime = 0f) {
		cameraCanSweepTime = Time.time + cameraSweepTime - modifiedTime;
	}

	void SetRotationFromSweep()
	{
		if (isLeft)
		{
			SetTargetRotation(Utilities2D.GetAngle(startRotation) + cameraSweepAngle);
		}
		else {
			SetTargetRotation(Utilities2D.GetAngle(startRotation) - cameraSweepAngle);
		}
	}

	public override void SetIsControlled(bool newIsControlled)
	{
		base.SetIsControlled(newIsControlled);
		//SetRotationFromSweep();
		hasNewTargetRotation = true;
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
		Game.instance.hacker.GetComponent<PlayerController_Hacker>().TakeControlOfObject(gameObject);
	}
}
