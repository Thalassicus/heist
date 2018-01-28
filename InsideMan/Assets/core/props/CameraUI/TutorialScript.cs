using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {
	public GameObject menu;
	bool isActive = true;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			isActive = !isActive;
			menu.SetActive (isActive);
		}
	}
}
