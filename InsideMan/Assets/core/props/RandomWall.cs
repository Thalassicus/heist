using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWall : MonoBehaviour {
	public string spriteName = "snk-sprt-wall";
	private SpriteRenderer spriteR;
	private Sprite[] sprites;

	void Start()
	{
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		sprites = Resources.LoadAll<Sprite>(spriteName);

		spriteR.sprite = sprites [Mathf.FloorToInt(Random.Range (0f, 2.99999f))];

		spriteR.flipX = Random.Range (0f, 1f) > 0.5;
		spriteR.flipY = Random.Range (0f, 1f) > 0.5;
	}
}
