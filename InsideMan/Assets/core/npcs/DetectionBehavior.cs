using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DetectionBehavior: MonoBehaviour {

	public GameObject alertIcon;
	Vector3 alertIconLocalScale;
	float loseTimerEnd = 0f;
	bool isLosing = false;

	public bool canMarkLoot = false;
	
	public GameObject cameraNumberText;
	public LineRenderer rangeRenderer;

	// Use this for initialization
	void Start () {
		rangeRenderer.transform.position = Utilities2D.CleanVector3(rangeRenderer.transform.position, 0f);
		cameraNumberText.GetComponent<MeshRenderer>().sortingLayerID = SortingLayer.NameToID("UI");
		alertIconLocalScale = alertIcon.transform.localScale;
	}

	// Update is called once per frame
	void Update(){
		cameraNumberText.transform.rotation = Quaternion.Euler(0,0,0);
		if (isLosing){
			var percent = 1-((loseTimerEnd - Time.time) / Game.instance.detectionTime);
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
				Game.instance.ShowGameOver(false);
			}
		}
	}

	public void SetCanMarkLoot(bool newCanMarkLoot){
		canMarkLoot = newCanMarkLoot;
	}

	public void SetCameraNumber(int number){
		cameraNumberText.GetComponent<TextMesh>().text = "" + number;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (gameObject.tag == "Camera_Controlled" && collider.gameObject.tag == "Train")
		{
			collider.gameObject.GetComponent<TrainCar>().SetRoofVisibility(false);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if (gameObject.tag == "Camera_Controlled" && collider.gameObject.tag == "Train")
		{
			collider.gameObject.GetComponent<TrainCar>().SetRoofVisibility(false);
		}
	}

	public void DoTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			Game.instance.hackerCam.cullingMask |= 1 << LayerMask.NameToLayer("Thief");
			loseTimerEnd = Time.time + Game.instance.detectionTime;
			isLosing = true;
			alertIcon.SetActive(true);
		}
	}

	public void DoTriggerStay2D(Collider2D collider){
	}

	public void DoTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			Game.instance.hackerCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Thief"));
			isLosing = false;
			alertIcon.SetActive(false);
			alertIcon.GetComponent<SpriteRenderer>().color = Color.yellow;
		}
	}
}
