using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour{
	GameInstance gameInstance;

	bool isMarked = false;
	int value = 0;
	int baseValue = 5000;
	float highValueMultiplier = 3f;
	public bool isHighValue = false;

	bool markedForDeletion = false;

	public GameObject markerObject;

	void Start(){
		gameInstance = FindObjectOfType<GameInstance>();
		value = baseValue;
		if (isHighValue)
		{
			value = (int)(value * highValueMultiplier);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (markedForDeletion) return;
		if (other.gameObject.tag == "Thief"){
			gameInstance.AddThiefEarnings(isMarked,value);
			markedForDeletion = true;
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Camera")
		{
			gameInstance.AddHackerEarnings(value);
			isMarked = true;
			markerObject.SetActive(true);
		}
	}
}
