using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGround : MonoBehaviour {
	void Start () {
		Object thisSprite = Resources.Load ("Ground");
		for (int x = -80; x <= 80; x++) {
			for (int y = -40; y <= 40; y++) {
				GameObject o = (GameObject)Instantiate(thisSprite);
				o.transform.position = new Vector3 (x, y, 0);
			}
		}
	}
}
