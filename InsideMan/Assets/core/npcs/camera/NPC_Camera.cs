using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPC_Camera : MonoBehaviour {

	GameInstance gameInstance;
	bool isControlled = false;
    public bool isRandom = true;

	Quaternion targetRotation;
	Quaternion originalRotation;
	float angleTolerance = 0.1f;
	bool isRotating = false;
	float rotationRate = 0.75f;

	public float cameraSweepAngle = 45f;
	public float cameraSweepTime = 8f;
	float cameraCanSweepTime;
	bool isLeft;

	public GameObject alertIcon;
	Vector3 alertIconLocalScale;
	float loseTimerEnd = 0f;
	float loseTimerDuration = 0.7f;
	bool isLosing = false;

	public GameObject cameraNumberText;

	// Use this for initialization
	void Start () {
		gameInstance = FindObjectOfType<GameInstance> ();
		originalRotation = transform.rotation;
		cameraNumberText.GetComponent<MeshRenderer>().sortingLayerID = SortingLayer.NameToID("UI");
		Random.InitState(gameObject.GetInstanceID());
        if (isRandom) {
            gameObject.SetActive(Random.Range(0f, 1f) < gameInstance.cameraChance);
        }
		isLeft = Random.Range(0f, 1f) < 0.5f;
		cameraCanSweepTime = Time.time + (cameraSweepTime * Random.Range(0f, 1f));
		SetRotationFromSweep();
		alertIconLocalScale = alertIcon.transform.localScale;
	}

	// Update is called once per frame
	void Update(){
		cameraNumberText.transform.rotation = Quaternion.Euler(0,0,0);
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
		if (isLosing){
			var percent = 1-((loseTimerEnd - Time.time) / gameInstance.detectionTime);
			if (percent < 0.5f)
			{
				alertIcon.transform.localScale = alertIconLocalScale * 1f;
				alertIcon.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow,Color.red,percent);
			}
			else if(percent < 1f){
				alertIcon.transform.localScale = alertIconLocalScale * 1.5f;
				alertIcon.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, percent);
			}else{
				alertIcon.transform.localScale = alertIconLocalScale * 3f;
				alertIcon.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if (loseTimerEnd < Time.time){
				gameInstance.ShowGameOver(false);
			}
		}
	}

	public void SetCameraNumber(int number){
		cameraNumberText.GetComponent<TextMesh>().text = "" + number;
	}

	void DoSweep(){
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

	public Quaternion getRotation(Vector2 lookAtTarget){
		var cameraPos = gameInstance.HackerCam.GetComponent<Camera> ().WorldToScreenPoint (transform.position);
		var ang = Mathf.Atan2 (lookAtTarget.y-cameraPos.y,lookAtTarget.x-cameraPos.x);
		ang = (180 / Mathf.PI) * ang;
		return Quaternion.Euler (0, 0, ang+90);
	}

	public void SetTargetRotation(Quaternion newRotation){
		targetRotation = newRotation;
		isRotating = true;
	}

	public void SetIsControlled(bool thisIsControlled){
		isControlled	= thisIsControlled;
		SetRotationFromSweep();

		//targetRotation	= originalRotation;
	}

	void OnMouseDown(){
		gameInstance.hacker.GetComponent<PlayerController_Hacker>().TakeControlOfObject(gameObject);
	}

	public void DoTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			(gameInstance.HackerCam.GetComponent<Camera>()).cullingMask |= 1 << LayerMask.NameToLayer("Thief");
			loseTimerEnd = Time.time + gameInstance.detectionTime;
			isLosing = true;
			alertIcon.SetActive(true);
			//gameInstance.ShowGameOver (false);
		}
	}

	public void DoTriggerStay2D(Collider2D collider){
	}

	public void DoTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			(gameInstance.HackerCam.GetComponent<Camera>()).cullingMask &= ~(1 << LayerMask.NameToLayer("Thief"));
			isLosing = false;
			alertIcon.SetActive(false);
			alertIcon.GetComponent<SpriteRenderer>().color = Color.yellow;
		}
	}
}
