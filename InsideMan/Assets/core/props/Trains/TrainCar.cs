using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour {
	private string spriteName = "snk-sprt-traincar";
	private SpriteRenderer spriteR;
	private Sprite[] sprites;
	private BoxCollider2D thisCollider;

    public bool isRandom = true;
    public bool isBoxcar = false;

    void Start()
	{
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		thisCollider = gameObject.GetComponent<BoxCollider2D>();
		sprites = Resources.LoadAll<Sprite>(spriteName);

        if (isRandom) {
            isBoxcar = Random.Range(0f, 1f) < 0.4;
        }

        if (isBoxcar) {
            spriteR.sprite = sprites[0];
            thisCollider.isTrigger = true;
        } else {
            isBoxcar = false;
            spriteR.sprite = sprites[1];
            thisCollider.isTrigger = false;
        }

        spriteR.flipX = Random.Range (0f, 1f) > 0.5;
	}
}
