using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour{
	GameInstance gameInstance;

	private string spriteName = "snk-sprt-traincar";
	private SpriteRenderer spriteR;
	private Sprite[] sprites;
	private BoxCollider2D thisCollider;

	public GameObject traincarCover;

	public GameObject lootPrefab;

	public Vector3[] possibleLootLocations;

    public bool isRandom = true;
    public bool isBoxcar = false;
    public bool hasEntered = true;

    void Start() {
		gameInstance = FindObjectOfType<GameInstance>();

		spriteR = gameObject.GetComponent<SpriteRenderer>();
		//traincarCover.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
		traincarCover.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
		thisCollider = gameObject.GetComponent<BoxCollider2D>();
		sprites = Resources.LoadAll<Sprite>(spriteName);

        if (isRandom) {
            isBoxcar = Random.Range(0f, 1f) < 0.5;
        }

        if (isBoxcar) {
            spriteR.sprite = sprites[2];
            thisCollider.isTrigger = true;
			//spriteR.gameObject.layer = LayerMask.NameToLayer("TrainCarRoof");
			for (int i = 0; i < possibleLootLocations.Length; i++)
			{
				if (Random.Range(0f,1f) < gameInstance.lootChance)
				{
					CreateLoot(possibleLootLocations[i]);
				}
			}
			//gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			traincarCover.SetActive(true);

		} else {
            isBoxcar = false;
            spriteR.sprite = sprites[1];
            thisCollider.isTrigger = false;
			//spriteR.gameObject.layer = LayerMask.NameToLayer("TrainCar");
			//spriteR.GetComponent<SpriteRenderer> ().enabled = false;
			traincarCover.SetActive(false);
		}

        spriteR.flipX = Random.Range (0f, 1f) > 0.5;
	}

    void OnTriggerEnter(Collider other) {
        if (!hasEntered) {
            hasEntered = true;
            Debug.Log("Entered boxcar");
        }
    }

    void CreateLoot(Vector3 location)
	{
        if (!isRandom) return;
		Debug.Log("Creating loot!");
		var pref = Instantiate(lootPrefab, transform.position, Quaternion.identity);
		pref.transform.SetParent(transform);
		pref.transform.localPosition = location;
		pref.GetComponent<Loot>().SetSortingOrder(gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1);
	}

	public void SetRoofVisibility(bool isRoofVisible) {
		if (isBoxcar) {
			traincarCover.SetActive(isRoofVisible);
		}
	}
}
