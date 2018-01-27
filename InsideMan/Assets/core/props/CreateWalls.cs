using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWalls : MonoBehaviour {
	void Start () {
		Object thisSprite = Resources.Load ("Wall");
		int x = 0;
		int y = -35;
		for (x = -47; x <= 47; x++) {
			GameObject o = (GameObject)Instantiate(thisSprite);
			o.transform.position = new Vector3 (x, y, 0);
		}
		y = 35;
		for (x = -47; x <= 47; x++) {
			GameObject o = (GameObject)Instantiate(thisSprite);
			o.transform.position = new Vector3 (x, y, 0);
		}
		x = -47;
		for (y = -34; y <= 34; y++) {
			GameObject o = (GameObject)Instantiate(thisSprite);
			o.transform.position = new Vector3 (x, y, 0);
		}
		x = 47;
		for (y = -34; y <= 34; y++) {
			GameObject o = (GameObject)Instantiate(thisSprite);
			o.transform.position = new Vector3 (x, y, 0);
		}
	}
}
