using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGround : MonoBehaviour {
	public string spriteName = "snk-sprt-ground";
	private SpriteRenderer spriteR;
	private Sprite[] sprites;

	void Start()
	{
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		sprites = Resources.LoadAll<Sprite>(spriteName);

		float r = Random.Range (0f, 1f);
		if (r < 0.7f) {
			spriteR.sprite = sprites [0];
		} else if (r < 0.85f) {
			spriteR.sprite = sprites [1];
		} else {
			spriteR.sprite = sprites [2];
		}


		spriteR.flipX = Random.Range (0f, 1f) > 0.5;
		spriteR.flipY = Random.Range (0f, 1f) > 0.5;
	}
}
