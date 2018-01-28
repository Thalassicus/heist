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
	Vector3 originalScale;

	bool markedForDeletion = false;

	public GameObject outlineObject;
	public GameObject coinObject;
	public GameObject markerObject;

	void Start(){
		gameInstance = FindObjectOfType<GameInstance>();
		originalScale = transform.localScale;
		Random.InitState(gameObject.GetInstanceID());
		if (Random.Range(0f, 1f) < gameInstance.lootChance)
		{
			//gameObject.SetActive(true);
		}
		else
		{
			//gameObject.SetActive(false);
		}
		SetLootValue(Random.Range(0f, 1f) < gameInstance.highValueLootChance);
	}

	public void SetLootValue(bool isThisHighValue)
	{
		isHighValue = isThisHighValue;
		if (isHighValue)
		{
			value = (int)(baseValue * highValueMultiplier);
			outlineObject.SetActive(true);
			//transform.localScale = originalScale * 1.25f;
		}
		else{
			value = baseValue;
			outlineObject.SetActive(false);
			//transform.localScale = originalScale * 0.75f;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!gameInstance) { return; }
		if (markedForDeletion) return;
		if (other.gameObject.tag == "Thief")
		{
			Debug.Log("Cha-ching!");
			gameInstance.AddThiefEarnings(isMarked,value);
			markedForDeletion = true;
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Camera_Controlled")
		{
			gameInstance.AddHackerEarnings(value);
			isMarked = true;
			markerObject.SetActive(true);
		}
	}
}
