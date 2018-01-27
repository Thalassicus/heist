using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTrainCar : MonoBehaviour {
	private string spriteNames = "snk-sprt-traincar";
	private int spriteVersion = 0;
	private SpriteRenderer spriteR;
	private Sprite[] sprites;
	private BoxCollider2D collider;

	void Start()
	{
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		collider = gameObject.GetComponent<BoxCollider2D>();
		sprites = Resources.LoadAll<Sprite>(spriteNames);

		if (Random.Range (0f, 1f) < 0.4) {
			// boxcar
			spriteR.sprite = sprites [0];
			collider.enabled = false;
		} else {
			// tanker
			spriteR.sprite = sprites [1];
			collider.enabled = true;
		}

		spriteR.flipX = Random.Range (0f, 1f) > 0.5;
	}
}
